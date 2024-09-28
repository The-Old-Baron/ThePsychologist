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
    public float projectileSpeed;
    public Color AttackColor;
    private void Start()
    {
        // Initialize player and lantern light
        player = GetComponent<Player>();
        lastAttackTime = -attackCooldown; // Allows immediate attack at the start
        lanternLight = player.Lantern.GetComponentInChildren<Light2D>();
        
        switch(player.playerEquippment.Weapon)
        {
            case WeaponType.Sword:
                AttackColor = Color.red;
                break;
            case WeaponType.Shield:
                AttackColor = Color.blue;
                break;
            case WeaponType.Spear:
                AttackColor = Color.green;
                break;
            case WeaponType.Staff:
                AttackColor = Color.yellow;
                break;
            default:
                AttackColor = Color.white;
                break;
        }
    }

    private void Update()
    {
        // Check for attack input and cooldown
        if (Input.GetMouseButton(0) && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    private void Attack()
    {
        // Handle lantern light effect during attack
        StartCoroutine(HandleLanternLight());

        // Determine attack type based on equipped weapon
        switch (player.playerEquippment.Weapon)
        {
            case WeaponType.Sword:
            case WeaponType.Shield:
            case WeaponType.Spear:
                PerformShortRangeAttack();
                break;
            case WeaponType.Staff:
                PerformLongRangeAttack();
                break;
            default:
                Debug.LogWarning("Unsupported weapon type.");
                break;
        }
    }

    private void PerformShortRangeAttack()
    {
        // Perform short-range attack using the lantern
        player.Lantern.GetComponentInChildren<LanternAttack>().Attack();
    }

    private void PerformLongRangeAttack()
    {
        // Perform long-range attack by shooting a projectile towards the mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;
        GameObject projectile = Instantiate(player.playerEquippment.projetilPrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Bullet>().damage = player.PlayerStatus.Damage;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

        Debug.Log("Long-range attack performed.");
    }

    private IEnumerator HandleLanternLight()
    {
        // Temporarily increase lantern light intensity during attack and then fade it back
        float initialIntensity = lanternLight.intensity;
        lanternLight.intensity = lightIntensityDuringAttack;
        lanternLight.color = Color.red;
        lanternLight.volumeIntensity = 0.09f;
        float elapsedTime = 0f;
        while (elapsedTime < lightFadeDuration)
        {
            lanternLight.intensity = Mathf.Lerp(lightIntensityDuringAttack, initialIntensity, elapsedTime / lightFadeDuration);
            lanternLight.color = Color.Lerp(Color.red, Color.white, elapsedTime / lightFadeDuration);
            lanternLight.volumeIntensity = Mathf.Lerp(0.09f, 0f, elapsedTime / lightFadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        lanternLight.intensity = initialIntensity;
    }
}
