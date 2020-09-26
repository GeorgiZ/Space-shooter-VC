using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float laserSpeed;

    void Update()
    {
        LaserBehaviour();
    }

    public void LaserBehaviour()
    {
        transform.Translate(Vector3.up * laserSpeed * Time.deltaTime);
        if (gameObject.transform.position.y >= 6.9f || transform.position.y <= -6.0f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
