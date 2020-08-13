using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private int laserSpeed = 8;

    void Update()
    {
        LaserBehaviour();
    }

    private void LaserBehaviour()
    {
        transform.Translate(Vector3.down * laserSpeed * Time.deltaTime);
        if(transform.position.y <= -6.0f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }     
    }

    

}
