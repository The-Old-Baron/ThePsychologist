using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public int StrengthModifier;
    public int DexterityModifier;
    public int ConstitutionModifier;
    public int IntelligenceModifier;
    public int WisdomModifier;
    public int CharismaModifier;
    public ItemType Type;
}
public enum ItemType{
    Consumable,
    Equipment,
    Quest,
    Passive,
    Key
}