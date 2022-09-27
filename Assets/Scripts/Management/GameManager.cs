using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool gameEnded = false;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Mais de um GameManager na cena.");
            return;
        }
        instance = this;
    }

    private void Update()
    {
        if (gameEnded)
            return;

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        gameEnded = true;
        Debug.Log("Game Over");
        return;
    }
}
