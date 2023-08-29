using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jock : MonoBehaviour
{
    public GameObject Output;
    public GameObject Virgin;
    public Animator anim;
    float Shootime = 5;
    float shootimer = 5;

    //alterações da pep
    public GameObject target;
    private bool facingLeft = false;


    private void Start()
    {
        
    }
    private void Update()
    {
        
        if (target)
        {
            Vector3 dif = target.transform.position - transform.position;

            if (dif.x > 0 && !facingLeft)
            {
                Flip();
            } else if (dif.x < 0 && facingLeft)
            {
                Flip();
            }

        }

        
    }

    private void FixedUpdate()
    {
        if (target)
        {
            Shootime -= Time.deltaTime;

            if (Shootime < 0)
            {
                anim.SetTrigger("ToShoot");

                if (!facingLeft)
                {
                    GameObject go = (GameObject)Instantiate(Virgin, Output.transform.position, Quaternion.identity);
                    go.GetComponent<Virgin>().virginSpeed *= -1;
                } else
                {
                    GameObject go = (GameObject)Instantiate(Virgin, Output.transform.position, Quaternion.identity);
                    go.GetComponent<Virgin>().virginSpeed *= 1;
                }

                
                Shootime = shootimer;
            }
        }

        



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = null;
        }
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingLeft = !facingLeft;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player Projectile"))
        {
            Destroy(gameObject);
        }
    }


}
