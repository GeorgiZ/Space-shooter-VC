using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attacked to a child of the ExplodingRocket Object
public class ExplosionRadius : MonoBehaviour
{
    [SerializeField] private GameObject _explodingEnemy;
    [SerializeField] GameObject MineExplosion;
    [SerializeField] private GameObject AggressiveExplosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            GameObject clone = Instantiate(_explodingEnemy, collision.transform.position, Quaternion.identity);
            Destroy(clone, 1.47f);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if(collision.tag == "Mine")
        {
            GameObject clone = Instantiate(MineExplosion, collision.transform.position, Quaternion.identity);
            Destroy(clone, 1.47f);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if(collision.tag == "Aggressive")
        {
            GameObject clone = Instantiate(AggressiveExplosion, collision.transform.position, Quaternion.identity);
            Destroy(clone, 1.47f);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}