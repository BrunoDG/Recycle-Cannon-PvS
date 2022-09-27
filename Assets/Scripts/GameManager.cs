using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void GameOver(int whoDied)
    {
        GameManager manager = new GameManager();
        switch (whoDied)
        {
            case 0:
                Debug.Log("O coletor morreu. Fim de Jogo.");
                break;
            case 1:
                Debug.Log("A Muralha foi destruída. Fim de jogo.");
                break;
            case 2:
                Debug.Log("Você conseguiu! Parou a invasão!");
                break;
        }
    }
}
