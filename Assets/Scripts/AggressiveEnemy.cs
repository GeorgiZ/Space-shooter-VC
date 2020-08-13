using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveEnemy : MonoBehaviour
{

    [SerializeField] private int _speed;
    [SerializeField] private GameObject AggressiveEnemyExplosion;
    [SerializeField] private GameObject ExplodingRocket;
    [SerializeField] private GameObject Canvas;

    private Ui_Manager UiManager;
    private Player player;
    public int _turnPoint;
    private float _currentPosition;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        UiManager = GameObject.Find("Canvas").GetComponent<Ui_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentPosition = transform.position.x;
        AggressiveEnemyBehaviour();
    }

    private void AggressiveEnemyBehaviour()
    {
        if (_currentPosition >= 11.0f)
        {
            _turnPoint = 1;
        }
        else if (_currentPosition <= -11.0f)
        {
            _turnPoint = 0;
        }

        if(_turnPoint == 1)
        {
            transform.Translate((Vector3.left) * Time.deltaTime * _speed);
        }
        else if (_turnPoint == 0)
        {
            transform.Translate((Vector3.right) * Time.deltaTime * _speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject clone = Instantiate(AggressiveEnemyExplosion, transform.position, Quaternion.identity);
            Destroy(clone, 1.0f);
            collision.GetComponent<Player>().Damage();
            Destroy(gameObject);
            player.AddScore();
        }
        else if( collision.tag == "Laser")
        {
            player.AddScore();
            GameObject clone = Instantiate(AggressiveEnemyExplosion, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(clone, 1.0f);
            Destroy(gameObject);
        }
        else if(collision.tag == "rocket")
        {
            player.AddScore();
            GameObject clone = Instantiate(AggressiveEnemyExplosion, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(clone, 1.0f);
            GameObject RocketExplosion = Instantiate(ExplodingRocket, transform.position, Quaternion.identity);
            Destroy(RocketExplosion, 0.8f);
            Destroy(gameObject);
        }
        else if(collision.tag == "ExplosionRadius")
        {
            //adds 10 points to the score for each enemy hit and upfates it

            player._score += 10;
            UiManager.UpdateScore(player._score);
        }
    }


}
