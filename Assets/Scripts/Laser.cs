﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float laserSpeed;


    void Update()
    {
        LaserBehaviour();
        
    }

    public void LaserBehaviour()
    {
        transform.Translate(Vector3.up * laserSpeed * Time.deltaTime);
        if (gameObject.transform.position.y >= 6.9f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            GameObject clone  = Instantiate(ExplodingEnamy, collision.transform.position, Quaternion.identity);
            Destroy(clone, 1.47f);

        }
    }
}