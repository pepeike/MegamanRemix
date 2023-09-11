using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField]
    public float projectileSpeed = 1f;

    public Rigidbody2D rdb;

    private float virginTime = 5f;
    private float timer;

    private void Awake()
    {
        timer = virginTime;
    }


    void Update()
    {

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            Destroy(gameObject);
        }

        rdb.velocity = new Vector3(projectileSpeed, 0, 0);



        //transform.position += new Vector3(projectileSpeed, 0, 0);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Terrain"))
        {
            Destroy(gameObject);
        }
        

    }
}
