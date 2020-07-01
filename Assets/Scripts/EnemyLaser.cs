using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private int laserSpeed = 8;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /* if (gameObject.transform.position.y <= -6.8f)
         {
             Destroy(gameObject);
         }*/
        LaserBehaviour();
    }

    private void LaserBehaviour()
    {
        transform.Translate(Vector3.down * laserSpeed * Time.deltaTime);
        if(transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
       
    }


}
