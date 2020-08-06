using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject ExplodingEnemy;
    [SerializeField] private int _enemySpeed = 10;
    [SerializeField] private GameObject EnemyLaser;
    [SerializeField] private CameraShake _Camera;
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;

    Vector3 enemyPosition;
    private Player _player;
    private Ui_Manager _uiManager;
    private float _FireSpeed;
    private float _canFire = -1f;

    private void Start()
    {
        _Camera = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _uiManager = GameObject.Find("Canvas").GetComponent<Ui_Manager>();
        enemyPosition = transform.position;
    }

    void Update()
    { 
        EnemyBehaviour();
        EnemyShoot();
        enemyPosition = transform.position;
    }

    private void EnemyBehaviour()
    {
        enemyPosition -= transform.up * Time.deltaTime * _enemySpeed;
        transform.position = enemyPosition - transform.right * Mathf.Sin(Time.time * _frequency) * _amplitude; //side movement

        if (transform.position.y <= -5.35f)
        {
            float randomX = Random.Range(-8.0f, 9.0f);
            transform.position = new Vector3(randomX, 6.9f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameObject clone = Instantiate(ExplodingEnemy, transform.position, Quaternion.identity);
            Destroy(clone, 1.47f);
            _Camera.TriggerShake();
            other.GetComponent<Player>().Damage();
            Destroy(gameObject);
            _player.AddScore();
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
        else if(other.tag == "ExplosionRadius")
        {
            _player._score += 10;
            _uiManager.UpdateScore(_player._score);
            
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
