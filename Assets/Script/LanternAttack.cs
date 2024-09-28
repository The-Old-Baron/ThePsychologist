using System.Collections.Generic;
using UnityEngine;

public class LanternAttack : MonoBehaviour
{
    // Collider do objeto LanternAttack
    private Collider2D collider2D;

    // Lista de inimigos dentro do alcance do ataque
    private List<GameObject> enemiesInRange = new List<GameObject>();

    // Método chamado ao iniciar o script
    private void Start()
    {
        // Obtém o componente Collider2D anexado ao objeto
        collider2D = GetComponent<Collider2D>();
    }

    // Método para realizar o ataque
    public void Attack()
    {
        // Itera sobre todos os inimigos dentro do alcance
        foreach (var enemy in enemiesInRange)
        {
            // Aplica dano ao inimigo
            enemy.GetComponent<EnemyController>().TakeDamage(Player.Instance.PlayerStatus.Damage, new Vector2(1f, 1f));
            Debug.Log("Hit: " + enemy.name);
        }
    }

    // Método chamado quando outro collider entra no trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que entrou no trigger é um inimigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Adiciona o inimigo à lista de inimigos dentro do alcance
            enemiesInRange.Add(collision.gameObject);
            Debug.Log("Enemy entered: " + collision.gameObject.name);
        }
        // Verifica se o objeto que entrou no trigger é interativo com luz
        if (collision.gameObject.CompareTag("InteractiveWithLight"))
        {
            // Executa a ação específica do objeto interativo
            var interactiveComponent = collision.gameObject.GetComponent<IInteractiveWithLight>();
            if (interactiveComponent != null)
            {
                interactiveComponent.InteractWithLight();
                Debug.Log("Interactive action executed on: " + collision.gameObject.name);
            }
        }
    }

    // Método chamado quando outro collider sai do trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Verifica se o objeto que saiu do trigger é um inimigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Remove o inimigo da lista de inimigos dentro do alcance
            enemiesInRange.Remove(collision.gameObject);
            Debug.Log("Enemy exited: " + collision.gameObject.name);
        }
    }
}
