using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileD : MonoBehaviour
{
    private float _speed = 3;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        //the projectile moves down
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -6.6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().Damage();
        }
    }
}

