using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttributsHud : MonoBehaviour
{
    public static AttributsHud Instance;
    public TMP_Text Strength;
    public TMP_Text Dexterity;
    public TMP_Text Constitution;
    public TMP_Text Intelligence;
    public TMP_Text Wisdom;
    public TMP_Text Charisma;

    public TMP_Text Health;
    public TMP_Text Damage;
    public TMP_Text Speed;
    public TMP_Text AttackCooldown;
    public TMP_Text Defence;
    public TMP_Text Proficience;

    public GameObject PlayerStatus;
    public void Start()
    {
        Instance = this;
    }
    public void UpdatePlayerStatus(PlayerStatus playerStatus)
    {
        Strength.text = "Strength: " + playerStatus.Strength;
        Dexterity.text = "Dexterity: " + playerStatus.Dexterity;
        Constitution.text = "Constitution: " + playerStatus.Constitution;
        Intelligence.text = "Intelligence: " + playerStatus.Intelligence;
        Wisdom.text = "Wisdom: " + playerStatus.Wisdom;
        Charisma.text = "Charisma: " + playerStatus.Charisma;

        Health.text = "Health: " + playerStatus.Health;
        Damage.text = "Damage: " + playerStatus.Damage;
        Speed.text = "Speed: " + playerStatus.Speed;
        AttackCooldown.text = "Attack Cooldown: " + playerStatus.AttackCooldown;
        Defence.text = "Defence: " + playerStatus.Defence;
        Proficience.text = "Proficience: " + playerStatus.Proficience;

        PlayerStatus.SetActive(true);
    }
}
