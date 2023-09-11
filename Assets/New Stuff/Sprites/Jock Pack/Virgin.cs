using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virgin : MonoBehaviour
{

    [SerializeField]
    public float virginSpeed = 1f;

    private float virginTime = 5f;
    private float timer;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = virginTime;
    }

    private void Start()
    {
        

        rb.velocity = new Vector2(virginSpeed, 0);
    }


    void Update()
    {

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            Destroy(gameObject);
        }

        


        //transform.position += new Vector3(virginSpeed, 0, 0);
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain")) {
            
            Destroy(gameObject);

        }
    }

    

}
