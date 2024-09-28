using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class EnemyController : Entity
{
    public Enemy enemy;
    public Rigidbody2D rb;
    private float lastAttackTime;
    private float curRadio = 0.89f;
    public Light2D light;
    public bool collidingWithPlayer;
    public GameObject flag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collidingWithPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collidingWithPlayer = false;
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastAttackTime = -enemy.attackCooldown;
        light.color = enemy.lightColor;
        light.intensity = enemy.lightIntensity;
        lifeBar.maxValue = enemy.maxHealth;
        Life = enemy.maxHealth;
        UpdateLifeBar();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, Player.Instance.transform.position) <= enemy.chaseRange)
        {
            Vector2 direction = (Player.Instance.transform.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * enemy.speed * Time.deltaTime);
        }

        if(collidingWithPlayer && Time.time >= lastAttackTime + enemy.attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }

        if (enemy.heallingCooldown > 0 && Time.time >= lastAttackTime + enemy.heallingCooldown)
        {
            Healling();
            lastAttackTime = Time.time;
        }
    }

    private void Attack()
    {
        Player.Instance.TakeDamage(enemy.damage);
    }

    public void TakeDamage(float damage)
    {
        Life -= damage;
        lifeBar.gameObject.active = true;

        if (Life <= 0)
        {
            Destroy(gameObject);
        }
        UpdateLifeBar();
    }
    public Slider lifeBar;
    public void UpdateLifeBar()
    {
        lifeBar.value = Life;
        light.pointLightOuterRadius = curRadio * (Life / enemy.maxHealth) +.41f;
    }

    public void Healling()
    {
        if (enemy.currentHealth < enemy.maxHealth)
        {
            enemy.currentHealth += enemy.healling;
        }
        UpdateLifeBar();
    }
    public GameObject DropPrefab;
    private void OnDestroy()
    {
        Bestiary.Instance.AddCreature(enemy);
        // Try Spawn item by chance
        if (Random.Range(0f, 0f) <= enemy.chanceToDropItem)
        {
            DropPrefab.GetComponent<DroppedItem>().item = enemy.item;
            flag.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
            Instantiate(DropPrefab, flag.transform.position, Quaternion.identity);
        }
    }

    
}
