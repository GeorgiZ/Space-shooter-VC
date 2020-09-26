using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightAttack : MonoBehaviour
{
    private float _speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        transform.Translate(Vector3.right * Time.deltaTime * _speed);
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
