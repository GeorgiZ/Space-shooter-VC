using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftAttack : MonoBehaviour
{
    private float _speed = 3;

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        transform.Translate(Vector3.left * Time.deltaTime * _speed);
        if (transform.position.y < -7.0f)
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
