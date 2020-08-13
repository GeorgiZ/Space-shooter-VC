using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RammingBehaviour : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private GameObject Parent;

    public float rotatingSpeed = 200;
    Rigidbody2D rb;
    private GameObject Player;
    public float seconds = 0.0f;
    public bool playerInRange = false;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        rb = Parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if(playerInRange == true)
        {
            seconds += Time.deltaTime;
            rb.AddForce(-transform.up * _speed);
        }
        // if the player is not in range - the enemy goes back up if it tried to ram the player without going back futher than its starting Y position
        else if (playerInRange == false && Parent.transform.position.y < 3) 
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(transform.up * 15);

        }
        else if(Parent.transform.position.y > 3)
        {
            rb.velocity = Vector2.zero;
        }
        if (seconds >= 0.4f)
        {
            playerInRange = false;
            seconds = 0.0f;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("adasf");
            playerInRange = true;
            
        }
    }
}
