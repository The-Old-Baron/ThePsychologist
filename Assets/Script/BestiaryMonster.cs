using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestiaryMonster : MonoBehaviour
{
    // Public fields for UI elements
    public TMP_Text Name;
    public TMP_Text Description;
    public TMP_Text Count;
    public SpriteRenderer Image;

    // Method to set the monster details in the UI
    public void SetMonster(Enemy enemy, int count)
    {
        // Set the text fields with the enemy's details
        Name.text = enemy.name;
        Description.text = enemy.description;
        Count.text = count.ToString();

        // Reset the image (assuming you will set it later)
        Image.sprite = null;

        // Change the color of the sprite based on the enemy type
        switch (enemy.enemyType)
        {
            case EnemyType.EmocaoDestrutiva:
                // Uncomment and modify the line below if you want to change the color of the sprite
                // Image.color = Color.red;
                break;
            // Add other cases as needed
        }
    }
}
