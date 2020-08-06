using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MineProjectile : MonoBehaviour
{

	[SerializeField] public float speed = 2;
	[SerializeField] private CameraShake _Camera;

	private SpriteRenderer _spriteRenderer;
	private CircleCollider2D _thisCollider;
	private AudioSource _explosion;
	public float rotatingSpeed = 200;
    private GameObject target;
	Rigidbody2D rb;

	void Start()
	{
		_Camera = GameObject.Find("Main Camera").GetComponent<CameraShake>();
		target = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(MineProjectileLifespan());
		_explosion = GetComponent<AudioSource>();
		_thisCollider = gameObject.GetComponent<CircleCollider2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void FixedUpdate()
	{

			Vector2 point2Target = (Vector2)transform.position - (Vector2)target.transform.position;
			point2Target.Normalize();
			float value = Vector3.Cross(point2Target, transform.right).z;
			rb.angularVelocity = rotatingSpeed * value;
			rb.velocity = transform.right * speed;
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
			collision.GetComponent<Player>().Damage();
			_Camera.TriggerShake();
			_explosion.Play();
			_spriteRenderer.enabled = false;
			_thisCollider.enabled = false; // so the player wont get get hit while the sound finishes
			Destroy(gameObject, 1.0f);
        }
    }

	IEnumerator MineProjectileLifespan()
    {
		yield return new WaitForSeconds(4.0f);
		Destroy(gameObject);
    }
}
