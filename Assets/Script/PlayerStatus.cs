using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int Strength { get; private set; }
    public int Dexterity { get; private set; }
    public int Constitution { get; private set; }
    public int Intelligence { get; private set; }
    public int Wisdom { get; private set; }
    public int Charisma { get; private set; }

    public int Health { get; private set; }
    public int Damage { get; private set; }
    public int Speed { get; private set; }
    public int AttackCooldown { get; private set; }
    public int Defence { get; private set; }
    public int Proficience { get; private set; }

    private Dictionary<string, Item> inventory = new Dictionary<string, Item>();

    void Start()
    {
        // Inicializa os status do jogador
        Strength = 10;
        Dexterity = 10;
        Constitution = 10;
        Intelligence = 10;
        Wisdom = 10;
        Charisma = 10;
        Health = 10;
        Damage = 10;
        Speed = 10;
        AttackCooldown = 10;
        Defence = 10;
        Proficience = 10;
    }
   
    public void AddItem(Item item)
    {
        if (!inventory.ContainsKey(item.Name))
        {
            inventory.Add(item.Name, item);
            ModifyStats(item, true);
        }
    }

    public void RemoveItem(Item item)
    {
        if (inventory.ContainsKey(item.Name))
        {
            inventory.Remove(item.Name);
            ModifyStats(item, false);
        }
    }

    private void ModifyStats(Item item, bool isAdding)
    {
        int modifier = isAdding ? 1 : -1;
        Strength += item.StrengthModifier * modifier;
        Dexterity += item.DexterityModifier * modifier;
        Constitution += item.ConstitutionModifier * modifier;
        Intelligence += item.IntelligenceModifier * modifier;
        Wisdom += item.WisdomModifier * modifier;
        Charisma += item.CharismaModifier * modifier;
    }

    
}

