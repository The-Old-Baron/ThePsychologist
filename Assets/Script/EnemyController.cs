using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class EnemyController : Entity
{
    // Public fields
    public Enemy enemy;
    public Rigidbody2D rb;
    public Light2D light;
    public GameObject flag;
    public Slider lifeBar;
    public GameObject DropPrefab;
    public float knockbackForce = 5f;

    // Private fields
    private float lastAttackTime;
    private float curRadio = 0.89f;
    private bool collidingWithPlayer;

    private void Start()
    {
        // Find flag in the scene
        flag = GameObject.Find("Flag");

        // Initialize Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Initialize attack time
        lastAttackTime = -enemy.attackCooldown;

        // Initialize light properties
        light.color = enemy.lightColor;
        light.intensity = enemy.lightIntensity;

        // Initialize life bar
        lifeBar.maxValue = enemy.maxHealth;
        Life = enemy.maxHealth;
        UpdateLifeBar();
    }

    private void Update()
    {
        // Chase player if within range
        if (Vector2.Distance(transform.position, Player.Instance.transform.position) <= enemy.chaseRange)
        {
            Vector2 direction = (Player.Instance.transform.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * enemy.speed * Time.deltaTime);
        }

        // Check for attack
        var raycast = Physics2D.Raycast(transform.position, Player.Instance.transform.position - transform.position, enemy.attackRange, LayerMask.GetMask("Player"));
        if (raycast && Time.time >= lastAttackTime + enemy.attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }

        // Check for healing
        if (enemy.heallingCooldown > 0 && Time.time >= lastAttackTime + enemy.heallingCooldown)
        {
            Healling();
            lastAttackTime = Time.time;
        }
    }

    private void Attack()
    {
        // Inflict damage to the player
        Player.Instance.TakeDamage(enemy.damage);
    }

    public void TakeDamage(float damage, Vector2 knockbackDirection)
    {
        // Reduce life
        Life -= damage;

        // Show life bar
        lifeBar.gameObject.SetActive(true);

        // Apply knockback force
        rb.AddForce(knockbackDirection.normalized * knockbackForce, ForceMode2D.Impulse);

        // Check if enemy is dead
        if (Life <= 0)
        {
            Destroy(gameObject);
        }

        // Update life bar
        UpdateLifeBar();
    }

    public void UpdateLifeBar()
    {
        // Update life bar value
        lifeBar.value = Life;

        // Update light radius based on life
        light.pointLightOuterRadius = curRadio * (Life / enemy.maxHealth) + .41f;
    }

    public void Healling()
    {
        // Heal if not at max health
        if (enemy.currentHealth < enemy.maxHealth)
        {
            Life += enemy.healling;
        }

        // Update life bar
        UpdateLifeBar();
    }

    private void OnDestroy()
    {
        // Add creature to bestiary
        Bestiary.Instance.AddCreature(enemy);

        // Try to spawn item by chance
        if (Random.Range(0f, 1f) <= enemy.chanceToDropItem)
        {
            DropPrefab.GetComponent<DroppedItem>().item = enemy.item;
            flag.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            Instantiate(DropPrefab, flag.transform.position, Quaternion.identity);
        }
        Player.Instance.PlayerStatus.AddProficience(enemy.coin);
    }
}
