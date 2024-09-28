using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEditor.Search;
using UnityEngine;

public enum MovementDirection
{
    Up,
    Down,
    Left,
    Right
}
[RequireComponent(typeof(Rigidbody2D))]
public class Player : Entity
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public MovementDirection curDirection;
    public GameObject Lantern;
    public Vector3 lanternRotationOffset;
    public TMP_Text interactText;
    public PlayerStatus PlayerStatus;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            // print all Player Status on Unity Console
            Debug.Log("Strength: " + PlayerStatus.Strength);
            Debug.Log("Dexterity: " + PlayerStatus.Dexterity);
            Debug.Log("Constitution: " + PlayerStatus.Constitution);
            Debug.Log("Intelligence: " + PlayerStatus.Intelligence);
            Debug.Log("Wisdom: " + PlayerStatus.Wisdom);
            Debug.Log("Charisma: " + PlayerStatus.Charisma);
        }
        Move();
         CheckForNearbyItems(); // Check for nearby items in each frame
        // Obtém a posição do mouse na tela
        Vector3 mousePosition = Input.mousePosition;

        // Converte a posição do mouse para coordenadas do mundo
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = Lantern.transform.position.z; // Mantém a mesma profundidade da lanterna

        // Calcula a direção entre a lanterna e a posição do mouse
        Vector3 direction = mousePosition - Lantern.transform.position;

        // Calcula o ângulo em que a lanterna deve rotacionar
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Ajusta a rotação da lanterna para apontar na direção do mouse com o offset
        Lantern.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle) + lanternRotationOffset);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (NierItem != null)
            {
                PlayerStatus.AddItem(NierItem.GetComponent<DroppedItem>().item);
                Destroy(NierItem);
                interactText.text = "";
            }
        }
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized; // Normalize the direction to ensure consistent speed
        rb.velocity = direction * speed;

        if (x > 0)
        {
            curDirection = MovementDirection.Right;
        }
        else if (x < 0)
        {
            curDirection = MovementDirection.Left;
        }
        else if (y > 0)
        {
            curDirection = MovementDirection.Up;
        }
        else if (y < 0)
        {
            curDirection = MovementDirection.Down;
        }
    }

    public GameObject NierItem;
    public float getterRadius;

    private void CheckForNearbyItems()
    {
        float radius = 1.5f; // Define the radius to check for nearby items
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, getterRadius, LayerMask.GetMask("Items")); // Find all colliders within the radius

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Item"))
            {
                ShowCollectItem(hitCollider.GetComponent<DroppedItem>().item);
                NierItem = hitCollider.gameObject;
                return; // Exit after finding the first item
            }
        }

        // If no items are found, clear the interact text and NierItem reference
        interactText.text = "";
        NierItem = null;
    }

  

    private void ShowCollectItem(Item item)
    {
        interactText.text = "Pressione E para pegar " + item.Name;
    }
}

