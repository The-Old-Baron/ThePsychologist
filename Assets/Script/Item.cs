using UnityEngine;

// Define a ScriptableObject to represent an item in the inventory
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    // Basic properties of the item
    [Header("Basic Info")]
    public string Name; // Name of the item

    // Attribute modifiers provided by the item
    [Header("Attribute Modifiers")]
    public int StrengthModifier; // Modifier for Strength attribute
    public int DexterityModifier; // Modifier for Dexterity attribute
    public int ConstitutionModifier; // Modifier for Constitution attribute
    public int IntelligenceModifier; // Modifier for Intelligence attribute
    public int WisdomModifier; // Modifier for Wisdom attribute
    public int CharismaModifier; // Modifier for Charisma attribute

    // Type of the item
    [Header("Item Type")]
    public ItemType Type; // Type of the item (e.g., Consumable, Equipment, etc.)
}

// Enum to define different types of items
public enum ItemType
{
    Consumable, // Items that can be consumed (e.g., potions)
    Equipment,  // Items that can be equipped (e.g., weapons, armor)
    Quest,      // Items related to quests
    Passive,    // Items that provide passive benefits
    Key         // Key items that are essential for progression
}