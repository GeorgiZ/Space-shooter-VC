using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootIfPlayerBehind : MonoBehaviour
{
    [SerializeField] private GameObject EnemyMissile;
    [SerializeField] private GameObject Parent;
  
    private float _canFire = -1.0f;
    private float _fireSpeed = 5.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the player is behind the enemy can shoot with a fire rate
        if (collision.tag == "Player" && Time.time > _canFire)
        {
            _canFire = Time.time + _fireSpeed;
            Instantiate(EnemyMissile, Parent.transform.position, Quaternion.identity);
            
        }
    }

}
