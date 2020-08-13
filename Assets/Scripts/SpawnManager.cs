using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject Debuff;
    [SerializeField] private GameObject[] Powerup;
    [SerializeField] private GameObject[] MediumRatePowerup;
    [SerializeField] private GameObject[] RarePowerup;
    [SerializeField] private GameObject _waves;
    [SerializeField] private GameObject _mine;
    [SerializeField] private GameObject _ammoSpawn;
    [SerializeField] private GameObject _agressiveEnemy;

    private Text waveText;
    public GameObject Enemy;
    private bool _stopSpawning = false;
    private float enemySpawnRate = 4.0f;
    public int enemyCount;

    private void Start()
    {       
        waveText = _waves.GetComponent<Text>();
    }

    private void SpawnsNormalEnemy()
    {
        //Spawn position
        float randomX = Random.Range(-7.0f, 9.0f);
        Vector3 random = new Vector3(randomX, 5.9f, 0);

        ShieldedEnemyChance(Enemy);
        Instantiate(Enemy, random, Quaternion.identity);
        
        enemyCount += 1;
    }

    private void SpawnMineEnemy()
    {
        //Spawn position for mines
        float mineRandomX = Random.Range(-4.0f, 4.0f);
        Vector3 mineRandom = new Vector3(mineRandomX, 5.9f, 0);

        ShieldedEnemyChance(_mine);
        Instantiate(_mine, mineRandom, Quaternion.identity);
        enemyCount += 1;
    }

    private void SpawnAggressiveEnemy()
    {
        //spawns the aggressive enemy on the left or right side
        int randomX = Random.Range(0, 2);
        int random = 0;
        if (randomX == 1)
        {
            random = -11;
        }
        else if(randomX == 0)
        {
            random = 11;
        }
        Vector3 randomPosition = new Vector3(random, 3.0f, 0.0f);

        Instantiate(_agressiveEnemy, randomPosition, Quaternion.identity);
        
    }
    private void ShieldedEnemyChance(GameObject enemy)
    {
        //25% chance that the enemy will have a shield
        int randomChance = Random.Range(1, 5);

        if (randomChance == 2)
        {
            enemy.transform.GetChild(0).gameObject.SetActive(true);

        }
        else
        {
            enemy.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    IEnumerator SpawningEnemies()
    {        
        yield return new WaitForSeconds(1.0f);
        waveText.text = "Wave 1";
        _waves.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        _waves.SetActive(false);

        while (_stopSpawning == false)
        {
           
            yield return new WaitForSeconds(enemySpawnRate);
            SpawnsNormalEnemy();
            yield return new WaitForSeconds(enemySpawnRate);
            SpawnMineEnemy();

            if (enemyCount == 10)
            {
                //initializes text for waves
                yield return new WaitForSeconds(1.0f);
                waveText.text = "Wave 2";
                _waves.SetActive(true);
                enemySpawnRate = 2.0f;
                yield return new WaitForSeconds(2.0f);
                _waves.SetActive(false);


            }
            if (enemyCount == 30)
            {
                yield return new WaitForSeconds(1.0f);
                waveText.text = "Wave 3";
                _waves.SetActive(true);
                enemySpawnRate = 1.0f;
                yield return new WaitForSeconds(2.0f);
                _waves.SetActive(false);
            }
            if (enemyCount == 45)
            {
                _stopSpawning = true;
            }
            
        }    
    }

    IEnumerator SpawningAggressiveEnemy()
    {
        while(_stopSpawning == false)
        {
            yield return new WaitForSeconds(17.0f);
            SpawnAggressiveEnemy();
        }
    }

    IEnumerator SpawnAmmo()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(15.0f);

            float randomX = Random.Range(-9.4f, 9.4f);
            Vector3 random = new Vector3(randomX, 6.9f, 0.0f);

            Instantiate(_ammoSpawn, random, Quaternion.identity);
        }
        
    }

    IEnumerator SpawnPowerup()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(20.0f);

            float randomX = Random.Range(-9.4f, 9.4f);
            Vector3 random = new Vector3(randomX, 6.9f, 0.0f);
            int randomPowerup = Random.Range(0, 2);
            
            Instantiate(Powerup[randomPowerup], random, Quaternion.identity);
        }
    }

    IEnumerator SpawnMediumPowerup()
    {
        while(_stopSpawning == false)
        {
            yield return new WaitForSeconds(35.0f);

            float randomX = Random.Range(-9.4f, 9.4f);
            Vector3 random = new Vector3(randomX, 6.9f, 0.0f);
            int randomPowerup = Random.Range(0, 2);

            Instantiate(MediumRatePowerup[randomPowerup], random, Quaternion.identity);
        }
    }

    IEnumerator SpawnRarePowerup()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(45.0f);

            float randomX = Random.Range(-9.4f, 9.4f);
            Vector3 random = new Vector3(randomX, 6.9f, 0f);

            Instantiate(RarePowerup[0], random, Quaternion.identity);
        }
        
    }

    IEnumerator SpawnDebuff()
    {
        while(_stopSpawning == false)
        {
            yield return new WaitForSeconds(10.0f);

            float randomX = Random.Range(-9.4f, 9.4f);
            Vector3 random = new Vector3(randomX, 6.9f, 0f);

            Instantiate(Debuff, random, Quaternion.identity);

        }
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnDebuff());
        StartCoroutine(SpawningEnemies());
        StartCoroutine(SpawnPowerup());
        StartCoroutine(SpawnRarePowerup());
        StartCoroutine(SpawnMediumPowerup());
        StartCoroutine(SpawnAmmo());
        StartCoroutine(SpawningAggressiveEnemy());
    }
}
