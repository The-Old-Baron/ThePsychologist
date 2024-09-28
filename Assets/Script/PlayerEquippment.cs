using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Sword,
    Shield,
    Staff,
    Spear
}
public class PlayerEquippment : MonoBehaviour
{
    public Item Consumable;
    public Item Equipment;
    public WeaponType Weapon;

    public GameObject projetilPrefab;

    public Dictionary<string, Item> Items; //Name | Object

}
