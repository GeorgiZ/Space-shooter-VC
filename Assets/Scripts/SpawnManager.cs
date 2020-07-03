using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    
    [SerializeField]
    private GameObject _enemyCointainer;

    public GameObject Enemy;

    [SerializeField]
    private GameObject[] Powerup;

    private bool _stopSpawning = false;
    
    void Start()
    {
        

    }


    IEnumerator SpawningEnemies()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            float randomX = Random.Range(-9.4f, 9.4f);
            Vector3 random = new Vector3(randomX, 5.9f, 0);
            GameObject newEnemy = Instantiate(Enemy, random, Quaternion.identity);
            newEnemy.transform.parent = _enemyCointainer.transform;
            yield return new WaitForSeconds(5.0f);
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

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawningEnemies());
        StartCoroutine(SpawnPowerup());
    }

    

}
