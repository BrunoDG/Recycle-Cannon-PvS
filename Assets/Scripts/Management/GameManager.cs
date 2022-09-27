using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool gameEnded = false;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Mais de um GameManager na cena.");
            return;
        }
        instance = this;
    }

    void Update()
    {
        if (gameEnded)
            return;

        if (PlayerStats.Lives <= 0 || PlayerStats.WallHealth <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        Debug.Log("Game Over");
        //SceneManager.LoadScene("MainMenu");
        return;
    }
}
