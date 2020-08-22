using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    [SerializeField] private int speed = 1;
    [SerializeField] private int PowerupID;
    [SerializeField] private AudioSource soundsource;
    [SerializeField] private float _collectSpeed;

    BoxCollider2D PowerupCollider;
    private Renderer turnoffrender;
    Player player;
    Vector3 powerupDirection = new Vector3(0, -1, 0);

    private void Start()
    {
        soundsource = GetComponent<AudioSource>();
        turnoffrender = GetComponent<Renderer>();
        PowerupCollider = gameObject.GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        //moves all the pickups to the player with the set speed
        PowerupMovement();
        if(Input.GetKey(KeyCode.C))
        {
            float step = _collectSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
           
        }
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
                    player.TripleShotActive();
                    PowerupCollider.enabled = false;
                    break;
                case 1:
                    player.SpeedActive();
                    PowerupCollider.enabled = false;
                    break;
                case 2:
                    player.setShield(); //set shield charges to 3
                    player.ShieldActive();
                    PowerupCollider.enabled = false;
                    break;
                case 3:
                    PowerupCollider.enabled = false;
                    break;
                case 4:
                    PowerupCollider.enabled = false;
                    break;
                case 5:
                    player.RocketsActive();
                    PowerupCollider.enabled = false;
                    break;
                case 6:
                    player.DebuffActive();
                    PowerupCollider.enabled = false;
                    break;
                case 7:
                    player.NuclearChargeActive();
                    PowerupCollider.enabled = false;
                    break;
            }
            
            //the sound needs to finish playing
            turnoffrender.enabled = false;
            Destroy(gameObject, 0.8f);           
        }
        else if(collision.tag =="SingleLaser" || collision.tag== "Elaser")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
