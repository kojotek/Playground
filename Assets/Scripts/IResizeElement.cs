public interface IResizeElement {
    float size { get; }
    void AddSizeInPixels(int pixels);
    void SetSizeInPixels(int pixels);
}