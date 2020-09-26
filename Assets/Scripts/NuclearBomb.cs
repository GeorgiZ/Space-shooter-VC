using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuclearBomb : MonoBehaviour
{
    private float _speed = 12.0f;
    private float _rotationSpeed = 150.0f;
    private 

    void Update()
    {
        RotateBomb();
        FindClosestEnemy();
    }

    private void RotateBomb()
    {
        transform.Rotate(Vector3.back * Time.deltaTime * _rotationSpeed);
    }
    
    void FindClosestEnemy() 
    {
        float step = _speed * Time.deltaTime;
        float distanceToClosestEnemy = Mathf.Infinity;
        AllEnemies closestEnemy = null;
        AllEnemies[] allEnemies = GameObject.FindObjectsOfType<AllEnemies>();
        // finds the nearest gameobject from a type 'AllEnemies' and it sets the closestEnemy to it
        //If the GameObject is farther than the nearest distance encountered so far, it won't take it into account.
        foreach (AllEnemies currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, step);
    }

}
