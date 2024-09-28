using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternAttack : MonoBehaviour
{
    public Collider2D colldier;
    private List<GameObject> enemiesInRange = new List<GameObject>();

    public void Start()
    {
        colldier = GetComponent<Collider2D>();
    }

    public void Attack()
    {
        foreach (var enemy in enemiesInRange)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(Player.Instance.PlayerStatus.Damage);
            Debug.Log("Hit: " + enemy.name);
        }
    }

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemiesInRange.Add(collision.gameObject);
            Debug.Log("Enemy entered: " + collision.gameObject.name);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(collision.gameObject);
            Debug.Log("Enemy exited: " + collision.gameObject.name);
        }
    }

   
}
