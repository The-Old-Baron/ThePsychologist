using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage=1;
    public Rigidbody2D rb;

    

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.gameObject.tag == "Player")
        {
            return;
        }

        // Randomiza a chance de critico
        if (Random.Range(0f, 1f) <= 0.1f)
        {
            damage = damage * 2;
        }
        EnemyController enemy = hitInfo.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}