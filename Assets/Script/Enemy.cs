using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Default")]
public class Enemy : ScriptableObject
{
    public float attackCooldown;
    public float speed;
    public Color lightColor;
    public float lightIntensity;
    public float attackRange;
    public float damage;
    public float chaseRange;
    public float chanceToDropItem;
    public float maxHealth;
    public float currentHealth;
    public Item item;
    public float healling;
    public float heallingCooldown;
    public int ID;
    public string description;
    public string name;
    public Sprite sprite;
    public EnemyType enemyType;
}

public enum EnemyType
{
    EmocaoDestrutiva,
    DisturbioCognitivo,
    TranstonoDePersonalidade,
    DisturbioDeIdentidade
}