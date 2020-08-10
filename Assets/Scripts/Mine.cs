using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] GameObject MineExplosion;
    [SerializeField] private CameraShake _Camera;
    [SerializeField] private int _speed;
    [SerializeField] GameObject Projectile;
    [SerializeField] GameObject ExplodingRocket;
    [SerializeField] GameObject MineShield;

    private GameObject clone;
    private Player _player;
    private float _canShoot = -1.0f;
    private float _fireSpeed;
    float xRandom;


    void Start()
    {
        _Camera = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        xRandom = Random.Range(-1, 1);

    }
    void Update()
    {
        MineBehaviour();
        MineAttack();
    }

    private void MineBehaviour()
    {        
        //setting up the x movement for the mine enemy to left or right to a whole randomized float
        if (xRandom <0)
        {
            xRandom = -1.0f;
        }
        else
        {
            xRandom = 1;
        }
        Vector3 random = new Vector3(xRandom, 0, 0);
        transform.Translate((Vector3.down + random) * Time.deltaTime * _speed);

        if (transform.position.y <= -5.35f)
        {
            float randomX = Random.Range(-8.0f, 9.0f);
            transform.position = new Vector3(randomX, 6.9f, 0);
        }
    }

    private void MineAttack()
    {
        if (Time.time > _canShoot)
        {
            _fireSpeed = 9.0f;
            _canShoot = Time.time + _fireSpeed;
            Instantiate(Projectile, transform.position, Quaternion.identity);
        }

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _Camera.TriggerShake();
            collision.GetComponent<Player>().Damage();

            if(MineShield.activeSelf == true)//Removes the shield if it is active and protects the enemy for one hit
            {
                MineShield.SetActive(false);
                return;
            }
            clone = Instantiate(MineExplosion, transform.position, Quaternion.identity);
            DestroyObjectsAddScore();
            
        }
        
        if(collision.tag == "Laser" )
        {
            Destroy(collision.gameObject);

            if (MineShield.activeSelf == true)
            {
                MineShield.SetActive(false);
                return;
            }
            clone = Instantiate(MineExplosion, transform.position, Quaternion.identity);
            DestroyObjectsAddScore();
        }
        
        if (collision.tag == "rocket")
        {
            clone = Instantiate(MineExplosion, transform.position, Quaternion.identity);
            GameObject rocketExplosion =  Instantiate(ExplodingRocket, collision.transform.position, Quaternion.identity);
            Destroy(rocketExplosion, 1.47f);
            DestroyObjectsAddScore();
            Destroy(collision.gameObject);
        }
    }

    private void DestroyObjectsAddScore()
    {
     
        Destroy(gameObject);
        Destroy(clone, 1.0f);
        _player.AddScore();
    }
}
