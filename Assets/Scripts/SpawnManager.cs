﻿using System.Collections;
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
    [SerializeField] private GameObject _teleportingEnemy;
    [SerializeField] private GameObject _teleportLight;
    [SerializeField] private GameObject _homingNuclearPowerup;
    [SerializeField] GameObject Boss;

    private Text waveText;
    public GameObject Enemy;
    public bool _stopSpawning = false;
    private float enemySpawnRate = 4.0f;
    public int enemyCount;
    public bool _bossSpawned = false;

    private void Start()
    {       
        waveText = _waves.GetComponent<Text>();
    }

    private void Update()
    {
        InvokeBoss();
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
            else if (randomX == 0)
            {
                random = 11;
            }
            Vector3 randomPosition = new Vector3(random, 3.0f, 0.0f);

            Instantiate(_agressiveEnemy, randomPosition, Quaternion.identity);
            enemyCount += 1;
    }

    private void SpawnTeleportEnemy()
    {
            float _randomX = Random.Range(-8.8f, 8.8f);
            float _randomY = Random.Range(-4.5f, 4.5f);
            Vector3 _random = new Vector3(_randomX, _randomY, 0);
            GameObject enemy = Instantiate(_teleportingEnemy, _random, Quaternion.identity);
            GameObject light = Instantiate(_teleportLight, enemy.transform.position, Quaternion.identity);
            Destroy(light, 0.57f);
            enemyCount += 1;      
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

    private void SpawnBoss()
    {
        Vector3 BossSpawnPosition = new Vector3(0f, 6.2f, 0f);
        Instantiate(Boss, BossSpawnPosition, Quaternion.identity);
    }

    private void InvokeBoss()
    {
        //spawns the boss 9 seconds after the last wave has finished
            if (enemyCount == 45 && _bossSpawned == false)
            {               
                Invoke("SpawnBoss", 9.0f);
                _bossSpawned = true;
            }
    }

    IEnumerator SpawningEnemies()
    {
        while (_stopSpawning == false)
        {
            if (enemyCount == 0)
            {
                yield return new WaitForSeconds(1.0f);
                waveText.text = "Wave 1";
                _waves.SetActive(true);
                yield return new WaitForSeconds(1.0f);
                _waves.SetActive(false);
            }
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
            else if (enemyCount == 30)
            {
                yield return new WaitForSeconds(3.0f);
                waveText.text = "FINAL WAVE ";
                _waves.SetActive(true);
                enemySpawnRate = 1.0f;
                yield return new WaitForSeconds(2.0f);
                _waves.SetActive(false);
            }
            else if (enemyCount == 45)
            {
                _stopSpawning = true;
                yield return new WaitForSeconds(5.0f);
                waveText.text = "BOSS INCOMING ";
                _waves.SetActive(true);
                yield return new WaitForSeconds(3.0f);
                _waves.SetActive(false);
             
            }
            
        }    
    }

    IEnumerator SpawningTeleportingEnemy()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(40.0f);
            SpawnTeleportEnemy();
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

    IEnumerator SpawnHomingNuclear()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(60.0f);

            float randomX = Random.Range(-9.4f, 9.4f);
            Vector3 random = new Vector3(randomX, 6.9f, 0f);

            Instantiate(_homingNuclearPowerup, random, Quaternion.identity);
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
        if(_stopSpawning == false)
        {
            StartCoroutine(SpawnDebuff());
            StartCoroutine(SpawningEnemies());
            StartCoroutine(SpawnPowerup());
            StartCoroutine(SpawnRarePowerup());
            StartCoroutine(SpawnMediumPowerup());
            StartCoroutine(SpawnAmmo());
            StartCoroutine(SpawningAggressiveEnemy());
            StartCoroutine(SpawningTeleportingEnemy());
            StartCoroutine(SpawnHomingNuclear());
        }
        else
        {
            return;
        }
        
    }

}
