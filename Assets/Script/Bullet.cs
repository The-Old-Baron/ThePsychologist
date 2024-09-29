using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1; // Dano padrão da bala
    public Rigidbody2D rb; // Referência ao Rigidbody2D da bala

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Ignora colisão com o jogador
        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        if (hitInfo.gameObject.GetComponent<LanternAttack>() != null)
        {
            return;
        }
        // Randomiza a chance de crítico (10% de chance)
        if (Random.value <= 0.1f)
        {
            damage *= 2; // Dano crítico é o dobro do dano normal
        }

        // Tenta obter o componente EnemyController do objeto colidido
        Vector2 knockbackDirection = (hitInfo.transform.position - transform.position).normalized;
        Player.Instance.TakeDamage(damage, knockbackDirection, 2);

        // Destroi a bala após a colisão
        Destroy(gameObject);
    }
}