using TMPro;
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
    public static Player Instance;
    // Public fields
    public PlayerStatus PlayerStatus;
    public PlayerEquippment playerEquippment;
    public PlayerAttackSystem playerAttackSystem;
    public float speed = 5f;
    public GameObject Lantern;
    public Vector3 lanternRotationOffset;
    public TMP_Text interactText;
    public GameObject NierItem;
    public float getterRadius;

    // Private fields
    private Rigidbody2D rb;
    public MovementDirection curDirection;

    private void Start()
    {
        // Initialize Rigidbody2D component

        rb = GetComponent<Rigidbody2D>();
        PlayerStatus = GetComponent<PlayerStatus>();
        playerEquippment = GetComponent<PlayerEquippment>();
        playerAttackSystem = GetComponent<PlayerAttackSystem>();

        Instance = this;
    }

    private void Update()
    {
        // Check for player status display input
        if (Input.GetKeyDown(KeyCode.L))
        {
            DisplayPlayerStatus();
        }

        // Handle player movement
        Move();

        // Check for nearby items
        CheckForNearbyItems();

        // Handle lantern rotation towards mouse position
        RotateLanternTowardsMouse();

        // Check for item interaction input
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithItem();
        }
    }

    private void DisplayPlayerStatus()
    {
        Debug.Log("Strength: " + PlayerStatus.Strength);
        Debug.Log("Dexterity: " + PlayerStatus.Dexterity);
        Debug.Log("Constitution: " + PlayerStatus.Constitution);
        Debug.Log("Intelligence: " + PlayerStatus.Intelligence);
        Debug.Log("Wisdom: " + PlayerStatus.Wisdom);
        Debug.Log("Charisma: " + PlayerStatus.Charisma);
    }

    private void Move()
    {
        // Get input axes
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Calculate movement direction and apply velocity
        Vector2 direction = new Vector2(x, y).normalized;
        rb.velocity = direction * speed;

        // Update current movement direction
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

    private void RotateLanternTowardsMouse()
    {
        // Get mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Lantern.transform.position.z;

        // Calculate direction and angle to rotate lantern
        Vector3 direction = mousePosition - Lantern.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation with offset
        Lantern.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle) + lanternRotationOffset);
    }

    private void InteractWithItem()
    {
        if (NierItem != null)
        {
            PlayerStatus.AddItem(NierItem.GetComponent<DroppedItem>().item);
            Destroy(NierItem);
            interactText.text = "";
        }
    }

    private void CheckForNearbyItems()
    {
        // Define the radius to check for nearby items
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, getterRadius, LayerMask.GetMask("Items"));

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

    private void OnDestroy()
    {
        Instance = null;
    }
    public void TakeDamage(float damage)
    {
        Life -= damage;
        if (Life <= 0)
        {
            Debug.Log("Player died!");
        }
    }
}
