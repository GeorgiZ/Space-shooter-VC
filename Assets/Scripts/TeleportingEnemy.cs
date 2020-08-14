using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TeleportingEnemy : MonoBehaviour
{
    private Ui_Manager UiManager;
    private Player player;
    private float _randomX;
    private float _randomY;
    private Vector3 _random;
    [SerializeField] private GameObject Teleport;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        UiManager = GameObject.Find("Canvas").GetComponent<Ui_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            Destroy(collision.gameObject);
            //random coordinates for teleportation
            _randomX = Random.Range(-9.8f, 9.8f);
            _randomY = Random.Range(-5.0f, 5.0f);
            _random = new Vector3(_randomX, _randomY, 0);
            
            GameObject entry = Instantiate(Teleport, transform.position, Quaternion.identity);//teleport animation
            Destroy(entry, 0.57f);
            transform.position = _random;
            GameObject arrival = Instantiate(Teleport, transform.position, Quaternion.identity);
            Destroy(arrival, 0.57f);

        }
        else if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().AddScore();
            UiManager.UpdateScore(player._score);
        }
    }
}
