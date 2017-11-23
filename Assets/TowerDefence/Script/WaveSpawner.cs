using System.Collections;

using UnityEngine;

using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {


    public static int EnemiesAlive = 0;
    public int Enemis;
    public Wave[] waves;
    public Transform enemyPrefab;
    public Transform spawnPoint;

    public GameMenager gameMenager;

    public float timeBetweenWaves = 5f;

    private float countDown = 2f;

    private int waveIndex = 0;

    public Text waveContdownText;

    void Update()
    {
        Enemis = EnemiesAlive;
        if(EnemiesAlive>0)
        {
            return;
        }
        if (waveIndex == waves.Length)
        {
            gameMenager.WinLevel();

            this.enabled = false;
        }

        if (countDown <=0f)
        {
            StartCoroutine(SpawnWave());

            countDown = timeBetweenWaves;
            return;

        }


        countDown -= Time.deltaTime;

        countDown = Mathf.Clamp(countDown, 0, Mathf.Infinity);
        waveContdownText.text = Mathf.Floor(countDown).ToString();

        waveContdownText.text = string.Format("{0:0.00}",countDown);
    }

    IEnumerator SpawnWave()
    {
        
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];
        EnemiesAlive = wave.count;
            for(int i=0; i <wave.count; i++)
            {

                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        
         waveIndex++;

       
       
    }



    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy,spawnPoint.position,spawnPoint.rotation);
        
    }





}
