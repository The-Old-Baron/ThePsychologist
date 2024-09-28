using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    private Player player;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    private void Start()
    {
        player = GetComponent<Player>();
        lastAttackTime = -attackCooldown; // Permite atacar imediatamente no início
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= lastAttackTime + attackCooldown) // MouseButton1
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        switch (player.playerEquippment.Weapon)
        {
            case WeaponType.Sword:
                PerformShortRangeAttack();
                break;
            case WeaponType.Staff:
                PerformLongRangeAttack();
                break;
            // Adicione outros tipos de armas aqui
            default:
                Debug.LogWarning("Tipo de arma não suportado.");
                break;
        }
    }

    void PerformShortRangeAttack()
    {
        // Lógica para ataque de curta distância
        Vector2 direction = Vector2.zero;

        switch (player.curDirection)
        {
            case MovementDirection.Up:
                direction = Vector2.up;
                break;
            case MovementDirection.Down:
                direction = Vector2.down;
                break;
            case MovementDirection.Left:
                direction = Vector2.left;
                break;
            case MovementDirection.Right:
                direction = Vector2.right;
                break;
            default:
                Debug.LogWarning("Direção não suportada.");
                return;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f);
        if (hit.collider != null)
        {
            // Lógica para quando o ataque acerta algo
            Debug.Log("Ataque de curta distância realizado e acertou: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Ataque de curta distância realizado, mas não acertou nada.");
        }

        // Desenhar o gizmo da área de alcance
        Debug.DrawLine(transform.position, transform.position + (Vector3)direction, Color.red, 1f);
    }

    public float VelocidadeDeProjetil;
    void PerformLongRangeAttack()
    {
        // Dispara um projetil virado para o sentido do mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;
        GameObject projectile = Instantiate(player.playerEquippment.projetilPrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * VelocidadeDeProjetil;
        Debug.Log("Ataque de longa distância realizado.");
    }
}
