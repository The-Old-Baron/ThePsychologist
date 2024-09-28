using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttributsHud : MonoBehaviour
{
    // Singleton instance
    public static AttributsHud Instance;

    // Text fields for player attributes
    [Header("Attributes")]
    public TMP_Text Strength;
    public TMP_Text Dexterity;
    public TMP_Text Constitution;
    public TMP_Text Intelligence;
    public TMP_Text Wisdom;
    public TMP_Text Charisma;

    // Text fields for player status
    [Header("Status")]
    public TMP_Text Health;
    public TMP_Text Damage;
    public TMP_Text Speed;
    public TMP_Text AttackCooldown;
    public TMP_Text Defence;
    public TMP_Text Proficience;

    // GameObject to display player status
    public GameObject PlayerStatus;

    // Initialize the singleton instance
    private void Start()
    {
        Instance = this;
    }

    // Update the player status HUD with the given player status
    public void UpdatePlayerStatus(PlayerStatus playerStatus)
    {
        // Update attribute texts
        Strength.text = $"Strength: {playerStatus.Strength}";
        Dexterity.text = $"Dexterity: {playerStatus.Dexterity}";
        Constitution.text = $"Constitution: {playerStatus.Constitution}";
        Intelligence.text = $"Intelligence: {playerStatus.Intelligence}";
        Wisdom.text = $"Wisdom: {playerStatus.Wisdom}";
        Charisma.text = $"Charisma: {playerStatus.Charisma}";

        // Update status texts
        Health.text = $"Health: {playerStatus.Health}";
        Damage.text = $"Damage: {playerStatus.Damage}";
        Speed.text = $"Speed: {playerStatus.Speed}";
        AttackCooldown.text = $"Attack Cooldown: {playerStatus.AttackCooldown}";
        Defence.text = $"Defence: {playerStatus.Defence}";
        Proficience.text = $"Proficience: {playerStatus.Proficience}";

        // Activate the player status GameObject
        PlayerStatus.SetActive(true);
    }
}
