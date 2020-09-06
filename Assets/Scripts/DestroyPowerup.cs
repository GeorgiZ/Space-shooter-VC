
using UnityEngine;

public class DestroyPowerup : MonoBehaviour
{

    [SerializeField] GameObject Laser;
    [SerializeField] GameObject Parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -7.34f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Powerup")
        {
            Instantiate(Laser, Parent.transform.position, Quaternion.identity);
        }
    }

}
