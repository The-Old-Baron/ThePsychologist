using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerAttackSystem : MonoBehaviour
{
    private Player player;
    public float attackCooldown = 1f;
    private float lastAttackTime;
    private Light2D lanternLight;
    public float lightIntensityDuringAttack = 3f;
    public float lightFadeDuration = 1f;

    private void Start()
    {
        player = GetComponent<Player>();
        lastAttackTime = -attackCooldown; // Permite atacar imediatamente no início
        lanternLight = player.Lantern.GetComponentInChildren<Light2D>();
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
        StartCoroutine(HandleLanternLight());
        switch (player.playerEquippment.Weapon)
        {
            case WeaponType.Sword:
                PerformShortRangeAttack();
                break;
            case WeaponType.Shield:
                PerformShortRangeAttack();
                break;
            case WeaponType.Spear:
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
        Player.Instance.Lantern.GetComponentInChildren<LanternAttack>().Attack();
    }

    public float VelocidadeDeProjetil;
    void PerformLongRangeAttack()
    {
        // Dispara um projetil virado para o sentido do mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;
        GameObject projectile = Instantiate(player.playerEquippment.projetilPrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Bullet>().damage = player.PlayerStatus.Damage;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * VelocidadeDeProjetil;

        Debug.Log("Ataque de longa distância realizado.");
    }

    private IEnumerator HandleLanternLight()
    {
        float initialIntensity = lanternLight.intensity;
        lanternLight.intensity = lightIntensityDuringAttack;

        float elapsedTime = 0f;
        while (elapsedTime < lightFadeDuration)
        {
            lanternLight.intensity = Mathf.Lerp(lightIntensityDuringAttack, initialIntensity, elapsedTime / lightFadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        lanternLight.intensity = initialIntensity;
    }
}
