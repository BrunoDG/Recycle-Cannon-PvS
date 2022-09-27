using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    private float fullWallHP = 20f;

    [Header("City Wall Life Bar")]
    public float wallHP;
    public Image wallHealth;

    [Header("Player Lifes Count")]
    public int playerLives;
    
    public List<GameObject> playerHealth;

    void Start()
    {
        wallHP = fullWallHP;
        wallHealth.fillAmount = wallHP / fullWallHP;
        playerHealth.AddRange(GameObject.FindGameObjectsWithTag("LifeScavenger"));
    }

    public void DamageWall(float amount)
    {
        wallHP -= amount;
        wallHealth.fillAmount = wallHP / fullWallHP;
    }

    public void DamagePlayer(int amount)
    {
        playerLives--;
        for (int i = 0; i < playerHealth.Count; i++)
        {
            if (i > playerLives-1)
            {
                playerHealth[i].SetActive(false);
            }
        }
    }
}
