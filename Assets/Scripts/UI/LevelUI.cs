using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public static LevelUI instance;

    private float fullWallHP = 20f;

    [Header("City Wall Life Bar")]
    public float wallHP;
    public Image wallHealth;

    [Header("Player Lifes Count")]
    public int playerLives;   
    public List<GameObject> playerHealth;

    [Header("Cannon Ammo manager")]
    public int ammo;
    public string ammoType;

    [Header("Wave Spawner Properties")]
    public int level;
    public int waveSpawnCount;

    void Start()
    {
        if (instance != null)
        {
            Debug.LogError("There can't be more than one LevelUI instance.");
            return;
        }

        instance = this;
        wallHP = fullWallHP;
        wallHealth.fillAmount = wallHP / fullWallHP;
        playerHealth.AddRange(GameObject.FindGameObjectsWithTag("LifeScavenger"));
        level = 1;
    }

    public void DrawCannonAmmo()
    {

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

    public void SelectLevel(int lvl)
    {
        level = lvl;
    }
}
