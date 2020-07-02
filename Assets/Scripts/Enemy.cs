using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField]
    private int _enemySpeed = 10;

    [SerializeField]
    private GameObject EnemyLaser;

    Vector3 enemyDirection = new Vector3(0, -1, 0);

    public int damage = 10;

    private Player _player;

    private float _FireSpeed;

    private float _canFire = -1f;



    

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

    }

    void Update()
    {
        EnemyBehaviour();
        EnemyShoot();      
    }

    private void EnemyBehaviour()
    {
        transform.Translate(enemyDirection * _enemySpeed * Time.deltaTime);
        if(transform.position.y <= -5.35f)
        {
            float randomX = Random.Range(-9.4f, 9.4f);
            transform.position = new Vector3(randomX, 6.9f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
            other.GetComponent<Player>().Damage();
            Destroy(gameObject);
            

        }
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore();
            }

            Destroy(gameObject);

        }
    }

    private void EnemyShoot()
    {
        if (Time.time > _canFire)
        {
            _FireSpeed = Random.Range(2f, 4f);
            _canFire = Time.time + _FireSpeed;
            GameObject clone = Instantiate(EnemyLaser, transform.position, Quaternion.identity);
            
        }
        

    }


}
