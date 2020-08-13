using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField] private int _lives = 3;
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private bool _isRocketActive = false;
    [SerializeField] private bool _isTripleShotActive = false;
    [SerializeField] private bool _isSpeedActive = false;
    [SerializeField] private bool _isDebuffActive = false;
    [SerializeField] private bool _isShieldActive = false;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] public int _score;
    [SerializeField] private int _shieldCharges;
    [SerializeField] private GameObject TripleShot;
    [SerializeField] private GameObject LeftWingFire;
    [SerializeField] private GameObject RightWingFire;
    [SerializeField] private AudioClip _laser;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject Rocket;
    [SerializeField] private CameraShake _Camera;

    private SpawnManager _spawnManager;
    public GameObject laser;
    private Ui_Manager _uiManager;
    private int _dmgAmaunt = 1;
    private float _canFire = 0f;
    private float _bar;
    private int _ammunition;

    void Start()
    {
        transform.position = new Vector3(0f, -4.8f, 0f);
        _Camera = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<Ui_Manager>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _laser;
        _ammunition = 30;
    }

    void Update()
    {
        Movement();
        Thrust();
        SpaceToShoot();
        DestroyTripleShot();
        Shield();
        SetMaxLives();
        replenishShield(); // shield cannot be below 0 and above 3 ( replenish to 3 if 0 < shield < 3 )
        shieldcolour();
        PlayerOnFire();
        _uiManager.ChangeLives(_lives);
       _ammunition = Mathf.Clamp(_ammunition, 0, 30);
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

        if (_isSpeedActive == true)
        {
            _speed = 15.0f;
        }
        else
        {
            _speed = 10.0f;
        }

        if (_isDebuffActive == true)
        {
            _speed = 5.0f;
        }
    }

    private void Thrust()
    {
        ChangeThrusterBar();
        if (Input.GetKey(KeyCode.LeftShift) && _bar != 0)
        {
            _speed = _speed * 1.4f;
        }
    }

    private void ChangeThrusterBar()
    {
        _bar = GameObject.Find("Canvas").GetComponent<Ui_Manager>()._barX; //depleting the truster bar by moving the the thruster's sprite bar pivot point
    }

    private void Shoot()
    {
        _canFire = Time.time + _fireRate;

        if ( _isTripleShotActive == true)
        {
            Instantiate(TripleShot, transform.position, Quaternion.identity); 
        }
        else if (_isRocketActive == true)
        {
            Instantiate(Rocket, transform.position, Quaternion.identity);              
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
        _uiManager.UpdateAmmo(_ammunition);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            //player cannot shoot if ammo is 0
            if (_ammunition == 0)
            {
                return;
            }
            _ammunition -= 1;
   
            Shoot();           
        }
    }

    public void Damage()
    {
        //when shield is on one charge and player gets hit disable the shield and dont take dmg on that hit
        if (_isShieldActive == true && _shieldCharges == 1)
        {
            _isShieldActive = false;
            shield.SetActive(false);
            return;
        }

        _lives -= _dmgAmaunt;
        _shieldCharges -= 1;
        _uiManager.ChangeLives(_lives);
        _Camera.TriggerShake();

        if ( _lives <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);           
        }
    }

    public void Shield()
    {
        if (_isShieldActive == true)
        {
            _dmgAmaunt = 0;
        }
        else if (_isShieldActive == false)
        {
            _dmgAmaunt = 1;          
        }
    }

    public void setShield()
    {
        _shieldCharges = 3;
    }

    private void replenishShield()
    {
        if (_shieldCharges < 0)
        {
            _shieldCharges = 0;
        }
        else if (_shieldCharges > 3)
        {
            _shieldCharges = 3;
        }
    }

    private void shieldcolour()
    {
        switch (_shieldCharges)
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

    public void DestroyTripleShot()
    {
        if (TripleShot.transform.position.y >= 3.2f)
        {
            Destroy(TripleShot);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        if(_isTripleShotActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _isTripleShotActive = false;
        }
    }

    public void RocketsActive()
    {
        _isRocketActive = true;
        StartCoroutine(RocketDownRoutine());
    }

    IEnumerator RocketDownRoutine()
    {
        if (_isRocketActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _isRocketActive = false;
        }
    }

    public void SpeedActive()
    { 
        _isSpeedActive = true;
        StartCoroutine(SpeedPowerupDownRoutine());     
    }

    IEnumerator SpeedPowerupDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedActive = false;
    }

    public void DebuffActive()
    {
        _isDebuffActive = true;

        StartCoroutine(DebuffPowerDownRoutine());
    }

    IEnumerator DebuffPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isDebuffActive = false;
    }

    public void ShieldActive()
    {       
        _isShieldActive = true;
        shield.SetActive(true);        
    }

    public void AddScore()
    {
        _score += 10;
        _uiManager.UpdateScore(_score);
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
        if (_lives < 3 && _lives > 1)
        {            
            LeftWingFire.SetActive(true);
            RightWingFire.SetActive(false);
        }
        else if (_lives < 2)
        {           
            RightWingFire.SetActive(true);
            LeftWingFire.SetActive(true);
        }
        else
        {
            RightWingFire.SetActive(false);
            LeftWingFire.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
 
        if (collision.tag == "Elaser")
        {
            Damage();
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "ammo")
        {
            _ammunition += 15;
        }
        else if(collision.tag == "heal")
        {
            _lives += 2;
        }

    }

    private void SetMaxLives()
    {
        if (_lives > 3)
        {
            _lives = 3;
        }
    }

}
 