using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int Strength;
    public int Dexterity;
    public int Constitution;
    public int Intelligence;
    public int Wisdom;
    public int Charisma;

    public int Health;
    public int Damage;
    public int Speed;
    public int AttackCooldown;
    public int Defence;
    public int Proficience; 

    private Dictionary<string, Item> inventory = new Dictionary<string, Item>();

    public TMP_Text CollectedItemsText;

    public int GetHealth()
    {
        return Health * Constitution;
    }
    public int GetDamage()
    {
        return Damage * Strength;
    }
    public int GetSpeed()
    {
        return Speed * Dexterity;
    }
    public int GetAttackCooldown()
    {
        return AttackCooldown * Dexterity;
    }
    public int GetDefence()
    {
        return Defence * Constitution;
    }
    
    public void AddProficience(int value)
    {
        Proficience += value;
    }
    void Start()
    {
        // Inicializa os status do jogador
        Strength = 1;
        Dexterity = 1;
        Constitution = 1;
        Intelligence = 1;
        Wisdom = 1;
        Charisma = 1;
        Health = 1;
        Damage = 10;
        Speed = 1;
        AttackCooldown = 1;
        Defence = 1;
        Proficience = 1;
        CollectedItemsText.text = "";
    }
    
    public void AddItem(Item item)
    {
        if (!inventory.ContainsKey(item.Name))
        {
            inventory.Add(item.Name, item);
            ModifyStats(item, true);
            StartCoroutine(ShowCollectedItem(item.Name));
        }
    }

    private IEnumerator ShowCollectedItem(string itemName)
    {
        CollectedItemsText.text = $"Collected: {itemName}";
        CollectedItemsText.color = new Color(CollectedItemsText.color.r, CollectedItemsText.color.g, CollectedItemsText.color.b, 1);

        yield return new WaitForSeconds(2); // Wait for 2 seconds before starting the fade out

        while (CollectedItemsText.color.a > 0)
        {
            Color color = CollectedItemsText.color;
            color.a -= Time.deltaTime / 2; // Fade out over 2 seconds
            CollectedItemsText.color = color;
            yield return null;
        }

        CollectedItemsText.text = "";
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

