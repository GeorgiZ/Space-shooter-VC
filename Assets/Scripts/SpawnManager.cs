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
    [SerializeField] private int enemyCount;
    [SerializeField] private GameObject _waves;
    [SerializeField] private GameObject _mine;
    [SerializeField] private GameObject _ammoSpawn;
    private Text waveText;
    public GameObject Enemy;
    private bool _stopSpawning = false;
    private float enemySpawnRate = 4.0f;

    private void Start()
    {       
        waveText = _waves.GetComponent<Text>();        
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
           
            //Spawn position for mines
            float mineRandomX = Random.Range(-4.0f, 4.0f);
            Vector3 mineRandom = new Vector3(mineRandomX, 5.9f, 0);
            //Position for normal enemies
            float randomX = Random.Range(-7.0f, 9.0f);
            Vector3 random = new Vector3(randomX, 5.9f, 0);


            yield return new WaitForSeconds(enemySpawnRate);
            Instantiate(Enemy, random, Quaternion.identity);
            enemyCount += 1;
            yield return new WaitForSeconds(enemySpawnRate);
            Instantiate(_mine, mineRandom, Quaternion.identity);
            enemyCount += 1;
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
            if (enemyCount ==45)
            {
                _stopSpawning = true;
            }
            
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
            yield return new WaitForSeconds(5.0f);
            float randomX = Random.Range(-9.4f, 9.4f);
            Vector3 random = new Vector3(randomX, 6.9f, 0f);
            Instantiate(Debuff, random, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
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
    }
}
