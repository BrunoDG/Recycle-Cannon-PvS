using UnityEngine;

using System.Collections;
using System.Threading;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    private GameObject[] waveSpawners;
    public Material Organic, Plastic, Metal;

    // Waves countdown 
    public float waveCountdown = 10f;
    private float countdown = 5f;
    private int waveIndex = 1;

    public static int spawnCount = 0;

    private void Start()
    {
        waveSpawners = GameObject.FindGameObjectsWithTag("WaveSpawner");
    }

    public static void SpawnOver()
    {
        WaveSpawner wave = new WaveSpawner();
        wave.countdown = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0f && spawnCount == 0)
        {
            StartCoroutine(SpawnWave());
            countdown = waveCountdown;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        spawnCount = waveIndex * 5;
        foreach (GameObject waver in waveSpawners)
        {
            for (int i = 0; i < waveIndex; i++)
            {
                int enemyType = GetRandomInt(0, 1000) % 5;
                SpawnEnemy(enemyType, waver.transform);
                yield return new WaitForSeconds(0.5f);
            }
        }
        waveIndex++;
    }

    void SpawnEnemy(int enemyType, Transform spawnPoint)
    {
        GameObject alien = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation).gameObject;

        if (enemyType == 0 || enemyType == 2 || enemyType == 4) { 
            alien.GetComponent<Renderer>().material = (Material)Instantiate(Organic);
            alien.tag = "Organic";
        } else if (enemyType == 1)
        {
            alien.GetComponent<Renderer>().material = (Material)Instantiate(Plastic);
            alien.tag = "Plastic";
        } else
        {
            alien.GetComponent<Renderer>().material = (Material)Instantiate(Metal);
            alien.tag = "Metal";
        }

    }

    public int GetRandomInt(int min, int max)
    {
        System.Random rand = new System.Random();
        int index = rand.Next(min, max);
        return index;
    }
}
