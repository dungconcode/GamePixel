public enum StartType { Health, Armor, Damage, Mana }
public enum ModifierType { Flat, Percent }

[System.Serializable]
public class StartModifier
{
    public StartType startType;
    public ModifierType modifierType;
    public float value;

    public StartModifier(StartType type, ModifierType modType, float val)
    {
        this.startType = type;
        this.modifierType = modType;
        this.value = val;
    }
}