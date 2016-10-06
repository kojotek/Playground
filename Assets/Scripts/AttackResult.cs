public enum AttackType{
    SingleShot,
    SpinShot,
    Boomerang
}


public class AttackResult{

    public AttackResult(ActorController act, DamageTable tab, bool crit, AttackType atkType){
        actor = act;
        table = tab;
        criticalSpot = crit;
        type = atkType;
    }
    public ActorController actor;
    public DamageTable table;
    public bool criticalSpot;
    public AttackType type;
}