using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileR : MonoBehaviour
{
    private float _speed = 3;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.Translate(Vector3.right * Time.deltaTime * _speed);
        if (transform.position.x > 10.5f)
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
