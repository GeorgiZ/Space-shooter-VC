using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    [SerializeField] private int _speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MissileBehaviour();
    }

    private void MissileBehaviour()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);

        if (gameObject.transform.position.y >= 6.9f || transform.position.y <= -6.0f)
        {
            
            Destroy(gameObject);
        }
    }
}
