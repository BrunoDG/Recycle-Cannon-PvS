using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class WallResistance : MonoBehaviour
{
    private float startHealth = 20f;

    private LevelUI life;

    [SerializeField]
    public float health;

    void Start()
    {
        life = GetComponent<LevelUI>();
        health = startHealth;
    }
    public void TakeDamage(float amount)
    {
        life.DamageWall(amount);
        health -= amount;
        
        if (health <= 0)
        {
            Explode();
        }
    }

    void EndTurn()
    {
        health += 10f;
        life.DamageWall(-10f);
    }

    void Explode()
    {
        Destroy(gameObject);
    }
}
