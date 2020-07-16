using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    [SerializeField] private int speed = 3;
    [SerializeField] private int PowerupID;
    [SerializeField] private AudioSource soundsource;

    BoxCollider2D PowerupCollider;
    public Renderer turnoffrender;
    Vector3 powerupDirection = new Vector3(0, -1, 0);

    private void Start()
    {
        soundsource = GetComponent<AudioSource>();
        turnoffrender = GetComponent<Renderer>();
        PowerupCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        PowerupMovement();
    }

    private void PowerupMovement()
    {
        transform.Translate(powerupDirection * speed * Time.deltaTime);
        if (transform.position.y <= -7.34f)
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
                    PowerupCollider.enabled = false;
                    break;
                case 1:
                    collision.GetComponent<Player>().SpeedActive();
                    PowerupCollider.enabled = false;
                    break;
                case 2:
                    collision.GetComponent<Player>().setShield(); //set shield charges to 3
                    collision.GetComponent<Player>().ShieldActive();
                    PowerupCollider.enabled = false;
                    break;
                case 3:
                    PowerupCollider.enabled = false;
                    break;
                case 4:
                    PowerupCollider.enabled = false;
                    break;
                case 5:
                    collision.GetComponent<Player>().RocketsActive();
                    PowerupCollider.enabled = false;
                    break;              
            }
            
            //the sound needs to finish playing
            turnoffrender.enabled = false;
            Destroy(gameObject, 0.8f);           
        }
    }
}
