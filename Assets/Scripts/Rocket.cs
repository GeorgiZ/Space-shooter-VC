using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private GameObject _explodingEnemy;
    [SerializeField] private float _rSpeed = 3f;
    [SerializeField] private GameObject ExplodingRocket;

    private Player _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();      
    }

    void Update()
    {
        RocketBehaviour();
    }

    private void RocketBehaviour()
    {
        transform.Translate(Vector3.up * _rSpeed * Time.deltaTime);
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
            //destroys the rocket object on collision with the enemy and starts its animation
            //the object "ExplodingRocket" with the animation mentioned has a child game object with a colliding radius to kill multiple enemies
            GameObject deadEnemyAnim = Instantiate(_explodingEnemy, collision.transform.position, Quaternion.identity);
            Destroy(deadEnemyAnim, 1.47f);
            _player.AddScore();
            GameObject clone = Instantiate(ExplodingRocket, transform.position, Quaternion.identity);          
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Destroy(clone, 0.8f);
        }
    }
}
