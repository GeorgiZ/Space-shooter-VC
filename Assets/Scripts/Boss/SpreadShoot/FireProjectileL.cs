using UnityEngine;

public class FireProjectileL : MonoBehaviour
{
    private float _speed = 3;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        //the projectile moves to the left
        transform.Translate(Vector3.left * Time.deltaTime * _speed);
        if (transform.position.x < -10.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().Damage();
        }
    }
}
