using UnityEngine;
using UnityEngine.UI;

public class Alien : MonoBehaviour
{

    [HideInInspector]
    public float speed;

    [Header("Enemy Properties")]
    public float startSpeed = 10f;
    public float startBossHealth = 20f;
    public float startHealth = 3f;
    public float enemyWorth;
    public float health;
    public string enemyType;

    [Header("Droppables")]
    public GameObject[] trashObject;

    private float countdown;
    private bool hitWall = false;
    private bool isBoss = false;


    [Header("Unity Stuff")]
    public Image healthBar;


    public void SetStageBoss(bool boss)
    {
        isBoss = boss;
    }

    void Start()
    {
        speed = startSpeed;
        countdown = 1f;

        if (isBoss) {
            health = startBossHealth;
        } else
        {
            health = startHealth;
        }
        enemyWorth = health;
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
        LevelUI lvl = GetComponent<LevelUI>();
        if (enemyType == "Boss")
        {
            lvl.level++;
        } else
        {
            WaveSpawner.spawnCount--;
            lvl.waveSpawnCount = WaveSpawner.spawnCount;
        }

        DropItems();
        Destroy(transform.gameObject);
        return;
    }

    void DropItems()
    {
        
    }

}
