using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private float _speed = 10.0f;

    [SerializeField]
    private bool isTripleShotActive = false;

    [SerializeField]
    private bool isSpeedActive = false;

    [SerializeField]
    private bool isShieldActive = false;

    [SerializeField]
    private GameObject TripleShot;

    [SerializeField]
    private float _fireRate = 0.5f;

    [SerializeField]
    private int score;

    [SerializeField]
    private GameObject LeftWingFire;

    [SerializeField]
    private GameObject RightWingFire;

    [SerializeField]
    private AudioClip _laser;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private GameObject ExplodingEnamy;

    [SerializeField]
    private GameObject shield;

    [SerializeField]
    private int shieldCharges;


    //private Shield_Strenght _shieldRemaining;
    private Ui_Manager _uiManager;
    private int DmgAmaunt = 1;
    private float _canFire = 0f;
    private SpawnManager _spawnManager;
    public GameObject laser;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, -4.8f, 0f);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<Ui_Manager>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _laser;


    }

    void Update()
    {

        Movement();
        Thrust();
        SpaceToShoot();
        DestroyTripleShot();
        Shield();
        replenishShield(); // shield cannot be below 0 and above 3 ( replenish to 3 if 0 < shield < 3 )
        shieldcolour();
        PlayerOnFire();
        Debug.Log(shieldCharges);
  
        

    }

    public void Movement()
    {
       
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 axisMovement = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(axisMovement * _speed * Time.deltaTime);


        if (transform.position.x >= 11.1f)
        {
            transform.position = new Vector3(-11.25f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.25f)
        {
            transform.position = new Vector3(11.1f, transform.position.y, 0);
        }
        else if (transform.position.y >= 6.96f)
        {
            transform.position = new Vector3(transform.position.x, -5.5f, 0);
        }
        else if (transform.position.y <= -5.5f)
        {
            transform.position = new Vector3(transform.position.x, 6.96f, 0);
        }

        if (isSpeedActive == true)
        {
            _speed = 15.0f;
        }
        else if (isSpeedActive == false)
        {
            _speed = 10.0f;
        }
    }

    private void Thrust()
    {
        if ( Input.GetKey(KeyCode.LeftShift))
        {
            _speed = _speed * 1.3f;
        }
    }

    private void Shoot()
    {
        _canFire = Time.time + _fireRate;

        if ( isTripleShotActive == true)
        {
            Instantiate(TripleShot, transform.position, Quaternion.identity);
            
        }
        else
        {
            
            Instantiate(laser, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        _audioSource.Play();
            
    }

    //moved Shoot() in a separate method, not to have nested if statements in Shoot().
    private void SpaceToShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            Shoot();
        }
    }

    public void Damage()
    {
        
        if (isShieldActive == true && shieldCharges == 0)
        {
 
            isShieldActive = false;
            shield.SetActive(false);
            return;
        }
        _lives -= DmgAmaunt;
        shieldCharges -= 1;

        _uiManager.ChangeLives(_lives);

        if ( _lives <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            
            
        }
    }

    public void Shield()
    {
        if (isShieldActive == true)
        {
            DmgAmaunt = 0;
        }
        else if (isShieldActive == false)
        {
            DmgAmaunt = 1;
        }

    }   

    public void DestroyTripleShot()
    {
        if (TripleShot.transform.position.y >= 3.2f)
        {
            Destroy(TripleShot);
        }
    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        if(isTripleShotActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            isTripleShotActive = false;
        }
    }

    public void SpeedActive()
    {
  
        isSpeedActive = true;
        StartCoroutine(SpeedPowerupDownRoutine());     
    }

    IEnumerator SpeedPowerupDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedActive = false;
    }

    public void ShieldActive()
    {
        
        isShieldActive = true;
        shield.SetActive(true);
        

    }

    public void AddScore()
    {
        score += 10;
        _uiManager.UpdateScore(score);
    }


     public void CheckLives()
    {

            if (Input.GetKeyDown(KeyCode.R) && _lives <= 0)
            {
            SceneManager.LoadScene(1);
            }
    }

    private void PlayerOnFire()
    {
        if (_lives == 2)
        {
            LeftWingFire.SetActive(true);
        }
        else if (_lives == 1)
        {
            RightWingFire.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            GameObject clone = Instantiate(ExplodingEnamy, collision.transform.position, Quaternion.identity);
            Destroy(clone, 1.47f);

        }
        if (collision.tag == "Elaser")
        {
            Damage();
        }

    }

    public void setShield()
    {
        shieldCharges = 3;
    }

    private void replenishShield()
    {
        if (shieldCharges < 0)
        {
            shieldCharges = 0;
        }
        else if (shieldCharges > 3)
        {
            shieldCharges = 3;
        }
    }

    private void shieldcolour()
    {
        switch (shieldCharges)
        {
            case 0:
                shield.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                break;
            case 1:
                shield.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
                break;
            case 2:
                shield.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
                break;
            case 3:
                shield.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                break;

        }
    }


}
 