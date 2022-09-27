using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class WallResistance : MonoBehaviour
{
    private float startHealth = 20f;

    [SerializeField]
    public float health;

    [Header("Unity Stuff")]
    public Image healthBar;

    void Start()
    {
        health = startHealth;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Explode();
        }
    }

    void EndTurn()
    {
        health += 10f;
    }

    void Explode()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
