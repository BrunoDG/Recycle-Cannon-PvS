using UnityEngine;

using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    [Header("Enemy spawner properties")]
    public Transform enemyPrefab;
    public Transform bossPrefab;
    public Material Organic, Plastic, Metal, Boss;

    private GameObject[] waveSpawners;

    [Header("Waves countdown")]
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
        if (waveIndex < 5)
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
        } else
        {
            SpawnBoss(waveSpawners[0].transform);
            waveIndex = 0;
        }
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

    public void SpawnBoss(Transform spawnPoint)
    {
        GameObject boss = Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation).gameObject;
        boss.tag = "Boss";
    }

    public int GetRandomInt(int min, int max)
    {
        System.Random rand = new System.Random();
        int index = rand.Next(min, max);
        return index;
    }
}
