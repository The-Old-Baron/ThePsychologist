using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    public EnemyController enemyController;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with enemy");
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            Player.Instance.TakeDamage(enemyController.enemy.damage, knockbackDirection, 2);
        }
    }
}
