using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProficienceControler : MonoBehaviour
{
    public SaveData playerStatus;

    public int CustoDoLevel;
    public TMP_Text Cost;
    public TMP_Text proficienceText;
    public TMP_Text StrengthText;
    public TMP_Text DexterityText;
    public TMP_Text ConstitutionText;
    public TMP_Text IntelligenceText;
    public TMP_Text WisdomText;
    public TMP_Text CharismaText;
    public void Start()
    {
        // Load PlayerStatus
        LoadPlayerStatus();
        UpdateText();
    }

    private void LoadPlayerStatus()
    {
        // Assuming you have a method to load the player status from a file
        string path = Path.Combine(Application.persistentDataPath, "savefile.json"); 
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            playerStatus = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            Debug.LogError("Save file not found");
        }
    }
    public void AddForce()
    {
        if (playerStatus.proficiencyPoints >= CustoDoLevel)
        {
            playerStatus.proficiencyPoints  -= CustoDoLevel;
            playerStatus.Strength += 1;
            UpdateText();
        }
    }
    public void AddDexterity()
    {
        if (playerStatus.proficiencyPoints  >= CustoDoLevel)
        {
            playerStatus.proficiencyPoints  -= CustoDoLevel;
            playerStatus.Dexterity += 1;
            UpdateText();
        }
    }
    public void AddConstitution()
    {
        if (playerStatus.proficiencyPoints  >= CustoDoLevel)
        {
            playerStatus.proficiencyPoints  -= CustoDoLevel;
            playerStatus.Constitution += 1;
            UpdateText();
        }
    }
    public void AddIntelligence()
    {
        if (playerStatus.proficiencyPoints  >= CustoDoLevel)
        {
            playerStatus.proficiencyPoints  -= CustoDoLevel;
            playerStatus.Intelligence += 1;
            UpdateText();
        }
    }
    public void AddWisdom()
    {
        if (playerStatus.proficiencyPoints  >= CustoDoLevel)
        {
            playerStatus.proficiencyPoints  -= CustoDoLevel;
            playerStatus.Wisdom += 1;
            UpdateText();
        }
    }
    public void AddCharisma()
    {
        if (playerStatus.proficiencyPoints  >= CustoDoLevel)
        {
            playerStatus.proficiencyPoints  -= CustoDoLevel;
            playerStatus.Charisma += 1;
            UpdateText();
        }
    }
    public void RetornToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void UpdateText()
    {
        proficienceText.text = "Proficience: " + playerStatus.proficiencyPoints ;
        StrengthText.text = "Strength: " + playerStatus.Strength;
        DexterityText.text = "Dexterity: " + playerStatus.Dexterity;
        ConstitutionText.text = "Constitution: " + playerStatus.Constitution;
        IntelligenceText.text = "Intelligence: " + playerStatus.Intelligence;
        WisdomText.text = "Wisdom: " + playerStatus.Wisdom;
        CharismaText.text = "Charisma: " + playerStatus.Charisma;
        Cost.text = "Cost: " + CustoDoLevel;
        UpdateCusto();
    }

    public void UpdateCusto()
    {
        int level = playerStatus.Strength + playerStatus.Dexterity + playerStatus.Constitution + playerStatus.Intelligence + playerStatus.Wisdom + playerStatus.Charisma;
        CustoDoLevel = level * 10;
        Cost.text = "Cost: " + CustoDoLevel;
    }
}
