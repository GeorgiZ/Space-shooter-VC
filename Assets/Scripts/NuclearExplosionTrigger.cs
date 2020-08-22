using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuclearExplosionTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Parent;
    [SerializeField] GameObject NuclearExplosion;

    public bool _collided = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _collided = true;
        if (collision.tag == "AnyEnemy")
        {
            KillAll();           
            GameObject clone = Instantiate(NuclearExplosion, transform.position, Quaternion.identity);
            Destroy(Parent);
            Destroy(clone, 1.12f);
        }
    }

    private void KillAll()
    {
        if(_collided == true)
        {
            GameObject[] Enemies = GameObject.FindGameObjectsWithTag("AnyEnemy");

            foreach (GameObject i in Enemies)
            {
                Destroy(i.transform.parent.gameObject);
            }
        }
        _collided = false; 
    }
}
