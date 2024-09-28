using UnityEngine;

public class BestiaryController : MonoBehaviour
{
    // Referência para a UI do Bestiário
    public GameObject BestiaryUI;
    
    // Prefab do monstro do bestiário
    public GameObject BestiaryMonsterPrefab;
    
    // Contêiner onde os monstros do bestiário serão instanciados
    public Transform BestiaryMonsterContainer;

    // Método para abrir o bestiário
    public void OpenBestiary()
    {
        // Ativa a UI do bestiário
        BestiaryUI.SetActive(true);
        
        // Remove todos os filhos do contêiner de monstros do bestiário
        foreach (Transform child in BestiaryMonsterContainer)
        {
            Destroy(child.gameObject);
        }
        
        // Instancia e configura cada entrada do bestiário
        foreach (BestiaryEntry entry in Bestiary.Instance.creatures)
        {
            GameObject go = Instantiate(BestiaryMonsterPrefab, BestiaryMonsterContainer);
            BestiaryMonster monster = go.GetComponent<BestiaryMonster>();
            monster.SetMonster(entry.enemy, entry.count);
        }
    }

    // Método para fechar o bestiário
    public void CloseBestiary()
    {
        // Desativa a UI do bestiário
        BestiaryUI.SetActive(false);
    }
}