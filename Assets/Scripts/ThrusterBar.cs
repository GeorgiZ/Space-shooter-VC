using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterBar : MonoBehaviour
{

    private Transform bar;
    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("Bar");
        bar.localScale = new Vector3(.4f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
