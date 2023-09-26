using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbarrier : MonoBehaviour
{

    public Killbarrier killbarrier;

    //private Vector3 defaultKillpos = new Vector3(33, 2, -66);

    //[SerializeField]
    //private bool modKillPos = false;
    //[SerializeField]
    //private Vector3 newKillPos;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }

}
