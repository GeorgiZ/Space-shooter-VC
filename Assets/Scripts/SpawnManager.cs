using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Powerup;
    [SerializeField] private GameObject[] RarePowerup;
    [SerializeField] private int enemyCount;
    [SerializeField] private GameObject _waves;
    [SerializeField]
    private int _score;
    private Player player;
    private Text waveText;
    public GameObject Enemy;
    private bool _stopSpawning = false;

    private void Start()
    {       
        waveText = _waves.GetComponent<Text>();        
    }

    private void Update()
    {
        _score = player._score;
    }

    IEnumerator SpawningEnemies()
    {        
        yield return new WaitForSeconds(1.0f);
        waveText.text = "Wave 1";
        _waves.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        _waves.SetActive(false);

        while (_stopSpawning == false)
        {     
            float randomX = Random.Range(-8.6f, 9.0f);
            Vector3 random = new Vector3(randomX, 5.9f, 0);
            GameObject newEnemy = Instantiate(Enemy, random, Quaternion.identity);
            enemyCount += 1;
            yield return new WaitForSeconds(2.0f);
            if (enemyCount == 10)
            {
                yield return new WaitForSeconds(5);
                waveText.text = "Wave 2";
                _waves.SetActive(true);
                yield return new WaitForSeconds(3.0f);
                _waves.SetActive(false);
            }
            if (enemyCount == 30)
            {
                yield return new WaitForSeconds(5);
                waveText.text = "Wave 3";
                _waves.SetActive(true);
                yield return new WaitForSeconds(3.0f);
                _waves.SetActive(false);
            }
            if (enemyCount == 60)
            {
                _stopSpawning = true;
            }
            
        }    
    }

    IEnumerator SpawnPowerup()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(3.0f);
            float randomX = Random.Range(-9.4f, 9.4f);
            Vector3 random = new Vector3(randomX, 6.9f, 0.0f);

            int randomPowerup = Random.Range(0, 5);
            
            Instantiate(Powerup[randomPowerup], random, Quaternion.identity);
            yield return new WaitForSeconds(5); 
        }
    }

    IEnumerator SpawnRarePowerup()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(10.0f);
            float randomX = Random.Range(-9.4f, 9.4f);
            Vector3 random = new Vector3(randomX, 6.9f, 0f);

            Instantiate(RarePowerup[0], random, Quaternion.identity);
            yield return new WaitForSeconds(15);
        }
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawningEnemies());
        StartCoroutine(SpawnPowerup());
        StartCoroutine(SpawnRarePowerup());
    }
}
