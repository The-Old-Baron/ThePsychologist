using UnityEngine;

// Define a ScriptableObject for Enemy with a menu option in Unity's asset creation menu
[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Default")]
public class Enemy : ScriptableObject
{
    // Basic enemy attributes
    [Header("Attributes")]
    public float maxHealth;          // Maximum health of the enemy
    public float currentHealth;      // Current health of the enemy
    public float speed;              // Movement speed of the enemy
    public float attackRange;        // Range within which the enemy can attack
    public float damage;             // Damage dealt by the enemy
    public float chaseRange;         // Range within which the enemy will chase the player

    // Attack-related attributes
    [Header("Attack Attributes")]
    public float attackCooldown;     // Cooldown time between attacks

    // Healing-related attributes
    [Header("Healing Attributes")]
    public float healling;           // Amount of health the enemy can heal
    public float heallingCooldown;   // Cooldown time between healing actions

    // Drop-related attributes
    [Header("Drop Attributes")]
    public int coin;               // Amount of coins the enemy can drop
    public float chanceToDropItem;   // Chance to drop an item upon defeat
    public Item item;                // Item that can be dropped

    // Visual and descriptive attributes
    [Header("Visual & Description")]
    public Color lightColor;         // Color of the enemy's light
    public float lightIntensity;     // Intensity of the enemy's light
    public string description;       // Description of the enemy
    public string name;              // Name of the enemy
    public Sprite sprite;            // Sprite representing the enemy

    // Identification and type
    [Header("Identification")]
    public int ID;                   // Unique identifier for the enemy
    public EnemyType enemyType;      // Type of the enemy
}

// Enum to define different types of enemies
public enum EnemyType
{
    EmocaoDestrutiva,                // Destructive Emotion
    DisturbioCognitivo,              // Cognitive Disturbance
    TranstonoDePersonalidade,        // Personality Disorder
    DisturbioDeIdentidade            // Identity Disturbance
}