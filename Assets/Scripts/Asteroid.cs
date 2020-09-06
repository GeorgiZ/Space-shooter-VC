using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Player _player;
    private float rotationSpeed = 1;
    private Animator asteroid;
    private SpawnManager _spawn;
    private AudioSource boom;
    private BoxCollider2D ThisCollider;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        asteroid = GetComponent<Animator>();
        _spawn = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        boom = GetComponent<AudioSource>();
        ThisCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        AsteroidRotation();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            ThisCollider.enabled = false; // not to spawns multiple enemies
            asteroid.SetTrigger("AsteroidExplosion");
            boom.Play();
            _spawn.StartSpawning();
            Destroy(gameObject, 2.6f);
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "Player")
        {
            ThisCollider.enabled = false;
            asteroid.SetTrigger("AsteroidExplosion");
            boom.Play();
            _spawn.StartSpawning();
            _player.Damage();
            
            Destroy(gameObject, 2.6f);
        }
    }

    private void AsteroidRotation()
    {
        transform.Rotate(0, 0, 50 * rotationSpeed * Time.deltaTime);
    }

}
