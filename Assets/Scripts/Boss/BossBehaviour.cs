using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] GameObject FrontShot;
    [SerializeField] GameObject LeftShot;
    [SerializeField] GameObject RightShot;
    [SerializeField] GameObject BossHp;
    [SerializeField] GameObject ExplosionOnDeath;
    // L - LEFT , D - DOWN , R - RIGHT
    [SerializeField] GameObject LFireProjectile;
    [SerializeField] GameObject LDFireProjectile;
    [SerializeField] GameObject DFireProjectile;
    [SerializeField] GameObject DRFireProjectile;
    [SerializeField] GameObject RFireProjectile;

    private float _spreadFireRate;
    private float _SpreadCanFire = -1.0f;
    private float _frontFireRate;
    private float _frontCanFire = -1.0f;
    private float _leftFireRate;
    private float _leftCanFire = -1.0f;
    private float _rightFireRate;
    private float _rightCanFire = -1.0f;
    public int _speed = 2;
    private Vector3 _offset;
    public bool isBossAlive = false;
    // Start is called before the first frame update
    void Start()
    {
        _offset = new Vector3(0.15f, -0.7f, 0.0f);
        isBossAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        ShootStright();
        ShootLeft();
        ShootRight();
        ShootSpread();
        
    }
   
    private void Movement()
    {
        //stops the boss at the centre of the screen
        if(transform.position.y <= 0)
        {
            _speed = 0;
            BossHp.SetActive(true);
        }
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().Damage();
            ReduceHp();
        }
        else if (collision.tag == "Laser")
        {
            ReduceHp();
        }
        else if (collision.tag == "rocket")
        {
            ReduceHp();
        }
        else if (collision.tag == "NuclearBomb")
        {
            ReduceHp();
        }
    }

    private void ShootSpread()
    {
        if(Time.time > _SpreadCanFire)
        {
            _spreadFireRate = 5.0f;
            _SpreadCanFire = Time.time + _spreadFireRate;
            Instantiate(LFireProjectile, transform.position, Quaternion.identity);
            Instantiate(LDFireProjectile, transform.position, Quaternion.identity);
            Instantiate(DFireProjectile, transform.position, Quaternion.identity);
            Instantiate(DRFireProjectile, transform.position, Quaternion.identity);
            Instantiate(RFireProjectile, transform.position, Quaternion.identity);

        }
    }
    private void ShootStright()
    {
        //boss shoots his front projectile with a random fire speed
        if(Time.time > _frontCanFire)
        {
            _frontFireRate = 3.0f;
            _frontCanFire = Time.time + _frontFireRate;
            Instantiate(FrontShot, transform.position + _offset, Quaternion.identity);
        }
    }

    private void ShootLeft()
    {
        if(Time.time > _leftCanFire)
        {
            _leftFireRate = 3.0f;
            _leftCanFire = Time.time + _leftFireRate;
            Instantiate(LeftShot, transform.position, Quaternion.identity);
        }
    }

    private void ShootRight()
    {
        if(Time.time > _rightCanFire)
        {
            _rightFireRate = 3.0f;
            _rightCanFire = Time.time + _rightFireRate;
            Instantiate(RightShot, transform.position, Quaternion.identity);
        }
    }

    private void ReduceHp()
    {
        BossHp.transform.localScale -= new Vector3( 4, 0, 0);
        if (BossHp.transform.localScale.x < 0)
        {
            Destroy(this.gameObject);
            GameObject clone1 =  Instantiate(ExplosionOnDeath, transform.position, Quaternion.identity);
            GameObject clone2 = Instantiate(ExplosionOnDeath, new Vector3(transform.position.x, transform.position.y -1, transform.position.z), Quaternion.identity);
            Destroy(clone1, 1.47f);
            Destroy(clone2, 1.47f);
        }
    }

}
