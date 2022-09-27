using UnityEngine;
using UnityEngine.UI;

public class Alien : MonoBehaviour
{

    [HideInInspector]
    public float speed;

    [Header("Enemy Properties")]
    public float startSpeed = 10f;
    public float startHealth = 100f;
    public int worth = 20;

    private float health;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {

    }

    void Die()
    {
        Destroy(transform.gameObject);
        WaveSpawner.spawnCount--;
        return;
    }
}
