using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Player _player;
    private float rotationSpeed = 1;
    private Animator asteroid;

    private SpawnManager _spawn;

    public AudioSource boom;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        asteroid = GetComponent<Animator>();
        _spawn = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        boom = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidRotation();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            asteroid.SetTrigger("AsteroidExplosion");
            boom.Play();
            _spawn.StartSpawning();
            Destroy(gameObject, 2.6f);
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "Player")
        {
            asteroid.SetTrigger("AsteroidExplosion");
            boom.Play();
            _spawn.StartSpawning();
            _player.Damage();
            
            Destroy(gameObject, 2.6f);
        }
        else if(collision.tag == "rocket")
        {
            asteroid.SetTrigger("AsteroidExplosion");
            boom.Play();
            _spawn.StartSpawning();
            Destroy(gameObject, 2.6f);
            Destroy(collision.gameObject);
        }
    }

    private void AsteroidRotation()
    {
        transform.Rotate(0, 0, 50 * rotationSpeed * Time.deltaTime);
    }

}
