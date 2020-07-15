using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0f;
    [SerializeField] private float shakeMagnitude = 1.0f;
    [SerializeField] private float dampingSpeed = 1.0f;
    private Transform transform;
    Vector3 initialPosition;

    private void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent<Transform>();
        }
    }

    private void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }

        
    }

    public void TriggerShake()
    {
        shakeDuration = 2.0f;
    }
}
