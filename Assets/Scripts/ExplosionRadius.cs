using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attacked to a child of the ExplodingRocket Object
public class ExplosionRadius : MonoBehaviour
{
    [SerializeField] private GameObject _explodingEnemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            GameObject clone = Instantiate(_explodingEnemy, collision.transform.position, Quaternion.identity);
            Destroy(clone, 1.47f);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}