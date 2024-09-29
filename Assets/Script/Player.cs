using System.IO;
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
    private string saveFilePath;

    // Private fields
    private Rigidbody2D rb;
    private MovementDirection curDirection;

    private void Start()
    {
        // Initialize components
        rb = GetComponent<Rigidbody2D>();
        PlayerStatus = GetComponent<PlayerStatus>();
        playerEquippment = GetComponent<PlayerEquippment>();
        playerAttackSystem = GetComponent<PlayerAttackSystem>();
        saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");


        HandlerLoadPlayerAtr();
        Instance = this;
    }

    public void HandlerLoadPlayerAtr()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            // Carrega os pontos de proficiência
            PlayerStatus.Proficience = saveData.proficiencyPoints;

            // Carrega os atributos do jogador
            PlayerStatus.Strength = saveData.Strength;
            PlayerStatus.Dexterity = saveData.Dexterity;
            PlayerStatus.Constitution = saveData.Constitution;
            PlayerStatus.Intelligence = saveData.Intelligence;
            PlayerStatus.Wisdom = saveData.Wisdom;
            PlayerStatus.Charisma = saveData.Charisma;

            Debug.Log("Player attributes loaded from save file.");
        }
        else
        {
            Debug.LogWarning("Save file not found.");
        }
    }
    private void Update()
    {
        HandleInput();
        Move();
        CheckForNearbyItems();
        CheckForNearbyInteractive();
        RotateLanternTowardsMouse();
        UpdatePlayerStatus();

        if (NierInteract == null && NierItem == null)
        {
            interactText.text = "";
        }
        
    }

    private void HandleInput()
    {
        // Toggle player status display
        if (Input.GetKey(KeyCode.Tab))
        {
            AttributsHud.Instance.UpdatePlayerStatus(PlayerStatus);
        }
        else
        {
            AttributsHud.Instance.PlayerStatus.SetActive(false);
        }

        // Pause game on Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // Interact with item
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithItem();
        }
    }

    private void TogglePause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            PlayerHud.Instance.PauseMenu.Pause();
        }
        else
        {
            Time.timeScale = 1;
            PlayerHud.Instance.PauseMenu.Resume();
        }
    }

    private void Move()
    {
        // Get input axes
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Calculate movement direction and apply velocity
        Vector2 direction = new Vector2(x, y).normalized;
        rb.velocity = direction * speed * Player.Instance.PlayerStatus.GetSpeed();

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
    
    public GameObject NierInteract;

    private void InteractWithScene()
    {
        if (NierInteract != null)
        {
            NierInteract.GetComponent<IInteractive>().Interact();
            interactText.text = "";
        }
    }
    private void CheckForNearbyInteractive()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, getterRadius, LayerMask.GetMask("Interactive"));
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log(hitCollider.name);
            if (hitCollider.CompareTag("Interactive"))
            {
                ShowInteract(hitCollider.GetComponent<IInteractive>());
                NierInteract = hitCollider.gameObject;
                Debug.Log("Interact with " + hitCollider.name);
                return;
            }
        }
        interactText.text = "";
        NierInteract = null;
    }

    private void ShowInteract(IInteractive interactive)
    {
        interactText.text = "Pressione E para interagir";
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
                Debug.Log("Collect " + hitCollider.name);
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

    public void TakeDamage(float damage, Vector2 knockbackDirection, float knockbackForce)
    {
        Life -= damage;
        if (Life <= 0)
        {
            Debug.Log("Player died!");
            HandlePlayerDeath();
            // Retorna para o menu porem na tela de proficiencia
            PlayerHud.Instance.PauseMenu.GameOver();
        }
        else
        {
            ApplyKnockback(knockbackDirection, knockbackForce);
        }
    }

    private void ApplyKnockback(Vector2 direction, float force)
    {
        rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }

    public void UpdatePlayerStatus()
    {
        // Placeholder for updating player status
    }
     private void HandlePlayerDeath()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            // Adiciona pontos de proficiência
            saveData.proficiencyPoints += PlayerStatus.Proficience;

            // Salva os dados de volta no arquivo de save
            json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(saveFilePath, json);

            Debug.Log("Proficiency points added to save file.");
        }
        else
        {
            Debug.LogWarning("Save file not found.");
        }
    }
}
[System.Serializable]
public class SaveData
{
    public int proficiencyPoints;
    public int Strength;
    public int Dexterity;
    public int Constitution;
    public int Intelligence;
    public int Wisdom;
    public int Charisma;
}