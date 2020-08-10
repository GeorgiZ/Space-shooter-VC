using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject ExplodingEnemy;
    [SerializeField] private int _enemySpeed = 10;
    [SerializeField] private GameObject EnemyLaser;
    [SerializeField] private CameraShake _Camera;
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;
    [SerializeField] GameObject Shield;

    private Player _player;
    private Ui_Manager _uiManager;
    private float _FireSpeed;
    private float _canFire = -1f;

    private void Start()
    {
        _Camera = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _uiManager = GameObject.Find("Canvas").GetComponent<Ui_Manager>();
    }

    void Update()
    {;
        EnemyBehaviour();
        EnemyShoot();
    }

    private void EnemyBehaviour()
    {
        transform.Translate((Vector3.down) * Time.deltaTime * _enemySpeed);
        //enemyPosition -= transform.up * Time.deltaTime * _enemySpeed;
        transform.Translate((Vector3.right) * Mathf.Sin(Time.time * _frequency) * _amplitude); //side movement

        if (transform.position.y <= -5.35f)
        {
            float randomX = Random.Range(-8.0f, 9.0f);
            transform.position = new Vector3(randomX, 6.8f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().Damage();
            _Camera.TriggerShake();
            if (Shield.activeSelf == true) //Removes the shield if it is active and protects the enemy for one hit
            {
                Shield.SetActive(false);
                return;
            }
            GameObject clone = Instantiate(ExplodingEnemy, transform.position, Quaternion.identity);
            Destroy(clone, 1.47f);
            Destroy(gameObject);
            _player.AddScore();
        }
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (Shield.activeSelf == true) //Removes the shield if it is active and protects the enemy for one hit
            {
                Shield.SetActive(false);
                return;
            }
            // RemoveShield();
            GameObject clone = Instantiate(ExplodingEnemy, gameObject.transform.position, Quaternion.identity);
            Destroy(clone, 1.47f);
            if(_player != null)
            {
                _player.AddScore();
            }
            Destroy(gameObject);
        }
        else if(other.tag == "ExplosionRadius")
        {
            if (Shield.activeSelf == true) //Removes the shield if it is active and protects the enemy for one hit
            {
                Shield.SetActive(false);
                return;
            }
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
