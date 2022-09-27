using UnityEngine;
using UnityEngine.UI;

public class Alien : MonoBehaviour
{

    [HideInInspector]
    public float speed;

    [Header("Enemy Properties")]
    public float startSpeed = 10f;
    public float startHealth = 3f;
    public int worth = 5;
    public float health;

    [Header("Droppables")]
    public GameObject[] trashObject;


    private float countdown;
    private bool hitWall = false;


    [Header("Unity Stuff")]
    public Image healthBar;

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
        countdown = 1f;
    }

    void Update()
    {
        if (hitWall)
        {
            countdown -= Time.deltaTime;
        } 
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            DamagePlayer();
        }
        else if (other.gameObject.tag == "CityWall")
        {
            DamageWall(1f);
            hitWall = true;

        }
        else if (other.gameObject.tag == "Bullet")
        {
            TakeDamage(1f);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "CityWall" && countdown <= 0)
        {
            DamageWall(1f);
            countdown = 1f;
        }
    }

    void DamageWall(float damage)
    {
        
        PlayerStats.WallHealth -= damage;
    }

    void DamagePlayer()
    {
        PlayerStats.Lives--;
    }

    void Die()
    {
        Destroy(transform.gameObject);

        WaveSpawner.spawnCount--;
        return;
    }

    void DropItems()
    {
        
    }

}
