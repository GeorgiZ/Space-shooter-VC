using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    private float _rSpeed = 3f;

    [SerializeField]
    private GameObject ExplodingRocket;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
            GameObject clone = Instantiate(ExplodingRocket, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Destroy(clone, 0.8f);

        }
    }
}
