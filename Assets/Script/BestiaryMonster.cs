using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestiaryMonster : MonoBehaviour
{
    public TMP_Text Name;
    public TMP_Text Description;
    public TMP_Text Count;
    public SpriteRenderer Image;

    public void SetMonster(Enemy enemy, int count)
    {
        Name.text = enemy.name;
        Description.text = enemy.description;
        Count.text = count.ToString();
        Image = null;

        switch (enemy.enemyType)
        {
            case EnemyType.EmocaoDestrutiva:
                //gameObject.GetComponent<SpriteRenderer>().color = Color.red; // Change Image to SpriteRenderer
                break;
        }
    }
}
