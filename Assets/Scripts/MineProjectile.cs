using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MineProjectile : MonoBehaviour
{

	[SerializeField] public float speed = 2;

	private bool _canMove = true;
	public float rotatingSpeed = 200;
    private GameObject target;
	Rigidbody2D rb;

	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(MineProjectileLifespan());
	}

	void FixedUpdate()
	{
		if(_canMove == true)
        {
			Vector2 point2Target = (Vector2)transform.position - (Vector2)target.transform.position;
			point2Target.Normalize();
			float value = Vector3.Cross(point2Target, transform.right).z;
			rb.angularVelocity = rotatingSpeed * value;
			rb.velocity = transform.right * speed;
		}
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
			collision.GetComponent<Player>().Damage();
        }
    }

	IEnumerator MineProjectileLifespan()
    {
		yield return new WaitForSeconds(6.0f);
		_canMove = false;
	
		Destroy(gameObject);
    }
}
