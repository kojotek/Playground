using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Xft {
    public class XWeaponTrail : MonoBehaviour {
        public class Element {
            public Vector3 PointStart;

            public Vector3 PointEnd;

            public Vector3 Pos {
                get {
                    return (PointStart + PointEnd) / 2f;
                }
            }


            public Element(Vector3 start, Vector3 end) {
                PointStart = start;
                PointEnd = end;
            }

            public Element() {

            }
        }



        public class ElementPool {
            private readonly Stack<Element> _stack = new Stack<Element>();

            public int CountAll { get; private set; }
            public int CountActive { get { return CountAll - CountInactive; } }
            public int CountInactive { get { return _stack.Count; } }
            public ElementPool(int preCount) {
                for (int i = 0; i < preCount; i++) {
                    Element element = new Element();
                    _stack.Push(element);
                    CountAll++;
                }
            }

            public Element Get() {
                Element element;
                if (_stack.Count == 0) {
                    element = new Element();
                    CountAll++;
                }
                else {
                    element = _stack.Pop();
                }

                return element;
            }

            public void Release(Element element) {
                if (_stack.Count > 0 && ReferenceEquals(_stack.Peek(), element)) {
                    Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
                }
                _stack.Push(element);
            }
        }



        #region public members

        public static string Version = "1.1.1";


        public bool UseWith2D = false;
        public string SortingLayerName;
        public int SortingOrder;
        public Transform PointStart;
        public Transform PointEnd;
        public Transform Relative;

        public int MaxFrame = 14;
        public int Granularity = 60;
        public float Fps = 60f;

        public Color MyColor = Color.white;
        public Material MyMaterial;
        #endregion



        #region protected members
        protected float mTrailWidth = 0f;
        protected Element mHeadElem = new Element();
        protected List<Element> mSnapshotList = new List<Element>();
        protected ElementPool mElemPool;
        protected Spline mSpline = new Spline();
        protected float mFadeT = 1f;
        protected bool mIsFading = false;
        protected float mFadeTime = 1f;
        protected float mElapsedTime = 0f;
        protected float mFadeElapsedime = 0f;
        protected GameObject mMeshObj;
        protected VertexPool mVertexPool;
        protected VertexPool.VertexSegment mVertexSegment;
        protected bool mInited = false;

        #endregion

        #region property
        public float UpdateInterval {
            get {
                return 1f / Fps;
            }
        }
        public Vector3 CurHeadPos {
            get { return (Quaternion.Inverse(Relative.rotation) * (PointStart.position - Relative.position) + Quaternion.Inverse(Relative.rotation) * (PointEnd.position - Relative.position)) / 2f; }
        }
        public float TrailWidth {
            get {
                return mTrailWidth;
            }
        }
        #endregion

        #region API
        //you may pre-init the trail to save some performance.
        public void Init() {
            if (mInited)
                return;

            mElemPool = new ElementPool(MaxFrame);

            mTrailWidth = (PointStart.localPosition - PointEnd.localPosition).magnitude;

            InitMeshObj();

            InitOriginalElements();

            InitSpline();

            mInited = true;
        }


        public void Activate() {
            mInited = false;
            Init();

            gameObject.SetActive(true);

            mFadeT = 1f;
            mIsFading = false;
            mFadeTime = 1f;
            mFadeElapsedime = 0f;
            mElapsedTime = 0f;

            //reset all elemts to head pos.
            for (int i = 0; i < mSnapshotList.Count; i++) {
                mSnapshotList[i].PointStart = Quaternion.Inverse(Relative.rotation) * (PointStart.position - Relative.position);
                mSnapshotList[i].PointEnd = Quaternion.Inverse(Relative.rotation) * (PointEnd.position - Relative.position);

                mSpline.ControlPoints[i].Position = mSnapshotList[i].Pos;
                mSpline.ControlPoints[i].Normal = mSnapshotList[i].PointEnd - mSnapshotList[i].PointStart;
            }

            //reset vertex too.
            RefreshSpline();
            UpdateVertex();
        }

        public void Deactivate() {
            gameObject.SetActive(false);
        }

        public void StopSmoothly(float fadeTime) {
            mIsFading = true;
            mFadeTime = fadeTime;
        }

        #endregion

        #region unity methods
        void Update() {

            if (!mInited)
                return;


            UpdateHeadElem();


            mElapsedTime += Time.deltaTime;
            if (mElapsedTime < UpdateInterval) {
                return;
            }
            mElapsedTime -= UpdateInterval;



            RecordCurElem();

            RefreshSpline();

            UpdateFade();

            UpdateVertex();

        }


        void LateUpdate() {
            if (!mInited)
                return;


            mVertexPool.LateUpdate(Relative);
        }

        void OnLevelWasLoaded(int level) {
            mInited = false;
        }

        void OnDestroy() {
            if (!mInited || mVertexPool == null) {
                return;
            }
            mVertexPool.Destroy();
        }


        void Start() {
            mInited = false;
            Init();
        }

       // void OnEnable() {
       //     mInited = false;
       //     Init();
       // }

        void OnDrawGizmos() {
            if (PointEnd == null || PointStart == null) {
                return;
            }


            float dist = (PointStart.localPosition - PointEnd.localPosition).magnitude;

            if (dist < Mathf.Epsilon)
                return;


            Gizmos.color = Color.red;

            Gizmos.DrawSphere(Quaternion.Inverse(Relative.rotation) * (PointStart.position - Relative.position), dist * 0.04f);


            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(Quaternion.Inverse(Relative.rotation) * (PointEnd.position - Relative.position), dist * 0.04f);

        }

        #endregion

        #region local methods

        void InitSpline() {
            mSpline.Granularity = Granularity;

            mSpline.Clear();

            for (int i = 0; i < MaxFrame; i++) {
                mSpline.AddControlPoint(CurHeadPos, PointStart.localPosition - PointEnd.localPosition);
            }
        }

        void RefreshSpline() {
            for (int i = 0; i < mSnapshotList.Count; i++) {
                mSpline.ControlPoints[i].Position = mSnapshotList[i].Pos;
                mSpline.ControlPoints[i].Normal = mSnapshotList[i].PointEnd - mSnapshotList[i].PointStart;
            }

            mSpline.RefreshSpline();
        }

        void UpdateVertex() {

            VertexPool pool = mVertexSegment.Pool;


            for (int i = 0; i < Granularity; i++) {
                int baseIdx = mVertexSegment.VertStart + i * 3;

                float uvSegment = (float)i / Granularity;


                float fadeT = uvSegment * mFadeT;

                Vector2 uvCoord = Vector2.zero;

                Vector3 pos = mSpline.InterpolateByLen(fadeT);

                //Debug.DrawRay(pos, Vector3.up, Color.red);

                Vector3 up = mSpline.InterpolateNormalByLen(fadeT);
                Vector3 pos0 = pos + (up.normalized * mTrailWidth * 0.5f);
                Vector3 pos1 = pos - (up.normalized * mTrailWidth * 0.5f);


                // pos0
                pool.Vertices[baseIdx] = pos0;
                pool.Colors[baseIdx] = MyColor;
                uvCoord.x = 0f;
                uvCoord.y = uvSegment;
                pool.UVs[baseIdx] = uvCoord;

                //pos
                pool.Vertices[baseIdx + 1] = pos;
                pool.Colors[baseIdx + 1] = MyColor;
                uvCoord.x = 0.5f;
                uvCoord.y = uvSegment;
                pool.UVs[baseIdx + 1] = uvCoord;

                //pos1
                pool.Vertices[baseIdx + 2] = pos1;
                pool.Colors[baseIdx + 2] = MyColor;
                uvCoord.x = 1f;
                uvCoord.y = uvSegment;
                pool.UVs[baseIdx + 2] = uvCoord;
            }

            mVertexSegment.Pool.UVChanged = true;
            mVertexSegment.Pool.VertChanged = true;
            mVertexSegment.Pool.ColorChanged = true;

        }

        void UpdateIndices() {

            VertexPool pool = mVertexSegment.Pool;

            for (int i = 0; i < Granularity - 1; i++) {
                int baseIdx = mVertexSegment.VertStart + i * 3;
                int nextBaseIdx = mVertexSegment.VertStart + (i + 1) * 3;

                int iidx = mVertexSegment.IndexStart + i * 12;

                //triangle left
                pool.Indices[iidx + 0] = nextBaseIdx;
                pool.Indices[iidx + 1] = nextBaseIdx + 1;
                pool.Indices[iidx + 2] = baseIdx;
                pool.Indices[iidx + 3] = nextBaseIdx + 1;
                pool.Indices[iidx + 4] = baseIdx + 1;
                pool.Indices[iidx + 5] = baseIdx;


                //triangle right
                pool.Indices[iidx + 6] = nextBaseIdx + 1;
                pool.Indices[iidx + 7] = nextBaseIdx + 2;
                pool.Indices[iidx + 8] = baseIdx + 1;
                pool.Indices[iidx + 9] = nextBaseIdx + 2;
                pool.Indices[iidx + 10] = baseIdx + 2;
                pool.Indices[iidx + 11] = baseIdx + 1;

            }

            pool.IndiceChanged = true;
        }

        void UpdateHeadElem() {
            mSnapshotList[0].PointStart = Quaternion.Inverse(Relative.rotation) * (PointStart.position - Relative.position);
            mSnapshotList[0].PointEnd = Quaternion.Inverse(Relative.rotation) * (PointEnd.position - Relative.position);
        }


        void UpdateFade() {
            if (!mIsFading)
                return;

            mFadeElapsedime += Time.deltaTime;

            float t = mFadeElapsedime / mFadeTime;

            mFadeT = 1f - t;

            if (mFadeT < 0f) {
                Deactivate();
            }
        }

        void RecordCurElem() {
            //TODO: use element pool to avoid gc alloc.
            //Element elem = new Element(PointStart.localPosition, PointEnd.localPosition);

            Element elem = mElemPool.Get();
            elem.PointStart = Quaternion.Inverse(Relative.rotation) * (PointStart.position - Relative.position);
            elem.PointEnd = Quaternion.Inverse(Relative.rotation) * (PointEnd.position - Relative.position);

            if (mSnapshotList.Count < MaxFrame) {
                mSnapshotList.Insert(1, elem);
            }
            else {
                mElemPool.Release(mSnapshotList[mSnapshotList.Count - 1]);
                mSnapshotList.RemoveAt(mSnapshotList.Count - 1);
                mSnapshotList.Insert(1, elem);
            }

        }

        void InitOriginalElements() {
            mSnapshotList.Clear();
            //at least add 2 original elements
            mSnapshotList.Add(new Element(Quaternion.Inverse(Relative.rotation) * (PointStart.position - Relative.position), Quaternion.Inverse(Relative.rotation) * (PointEnd.position - Relative.position)));
            mSnapshotList.Add(new Element(Quaternion.Inverse(Relative.rotation) * (PointStart.position - Relative.position), Quaternion.Inverse(Relative.rotation) * (PointEnd.position - Relative.position)));
        }



        void InitMeshObj() {
            //init vertexpool
            mVertexPool = new VertexPool(MyMaterial, this);
            mVertexSegment = mVertexPool.GetVertices(Granularity * 3, (Granularity - 1) * 12);
            UpdateIndices();
        }

        #endregion


    }

}


