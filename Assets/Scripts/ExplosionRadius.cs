using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attacked to a child of the ExplodingRocket Object
public class ExplosionRadius : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}