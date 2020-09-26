using UnityEngine;

public class FireProjectileLD : MonoBehaviour
{
    private float _speed = 3;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        // the projectile moves diagonally
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        transform.Translate(Vector3.left * Time.deltaTime * _speed);
        if (transform.position.x < -6.6f)
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
