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
        Debug.Log("Ataque de curta distância realizado.");
    }

    void PerformLongRangeAttack()
    {
        // Dispara um projetil virado para o sentido do mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;
        GameObject projectile = Instantiate(player.playerEquippment.projetilPrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * 15f;
        Debug.Log("Ataque de longa distância realizado.");
    }
}
