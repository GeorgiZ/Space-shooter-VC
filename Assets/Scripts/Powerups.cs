using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    [SerializeField]
    private int speed = 3;

    [SerializeField]
    private int PowerupID;

    [SerializeField]
    private AudioSource soundsource;

    public Renderer turnoffrender;

    Vector3 powerupDirection = new Vector3(0, -1, 0);

    private void Start()
    {
        soundsource = GetComponent<AudioSource>();
        turnoffrender = GetComponent<Renderer>();
    }

    void Update()
    {
        PowerupMovement();
    }


    private void PowerupMovement()
    {
        transform.Translate(powerupDirection * speed * Time.deltaTime);
        if (transform.position.y <= -5.35f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            soundsource.Play();
            switch(PowerupID)
            {
                case 0:
                    collision.GetComponent<Player>().TripleShotActive();
                    break;
                case 1:
                    collision.GetComponent<Player>().SpeedActive();
                    break;
                case 2:
                    collision.GetComponent<Player>().ShieldActive();
                    break;
            }

            turnoffrender.enabled = false;
            Destroy(gameObject, 0.8f);
            
        }

    }



}
