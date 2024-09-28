using UnityEngine;

public class BestiaryController : MonoBehaviour
{
    public GameObject BestiaryUI;
    public GameObject BestiaryMonsterPrefab;
    public Transform BestiaryMonsterContainer;

    public void OpenBestiary()
    {
        BestiaryUI.SetActive(true);
        foreach (Transform child in BestiaryMonsterContainer)
        {
            Destroy(child.gameObject);
        }
        foreach (BestiaryEntry entry in Bestiary.Instance.creatures)
        {
            GameObject go = Instantiate(BestiaryMonsterPrefab, BestiaryMonsterContainer);
            BestiaryMonster monster = go.GetComponent<BestiaryMonster>();
            monster.SetMonster(entry.enemy, entry.count);
        }
    }

    public void CloseBestiary()
    {
        BestiaryUI.SetActive(false);
    }
}