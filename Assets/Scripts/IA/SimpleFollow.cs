using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    public GameObject target;
    Rigidbody2D rdb;

    [SerializeField]
    private float minDistance = 5f;
    

    [SerializeField]
    LayerMask ground;

    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rdb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            
            if (Vector3.Distance(transform.position, target.transform.position) > minDistance)
            {
                float dif = target.transform.position.x - transform.position.x;

                if (dif > 0)
                {
                    rdb.AddForce(new Vector2(dif + 7f, target.transform.position.y - transform.position.y + 3f));
                } else if (dif < 0)
                {
                    rdb.AddForce(new Vector2(dif - 7f, target.transform.position.y - transform.position.y + 3f));
                }

                

                /*shootingTimer -= Time.deltaTime;
                if (shootingTimer < .1f)
                {
                    Debug.Log("Shooting");
                    shootingTimer = shootTime;

                    
                }*/
            }



        }

        RaycastHit2D groundHit;
        RaycastHit2D ceilHit;

        groundHit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y, 0), Vector2.down, ground);
        //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - .8f, 0), Vector2.down, Color.red);
        ceilHit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y, 0), Vector2.up, ground);
        float dist;
        float ceilDist;

        dist = groundHit.distance - .2f;
        ceilDist = ceilHit.distance + .2f;

        //Debug.Log(dist);
        Debug.Log(ceilDist);

        if (dist < 3f)
        {
            rdb.velocity = new Vector2(rdb.velocity.x, 1f);
        }

        if (ceilDist < 2f)
        {
            rdb.velocity = new Vector2(rdb.velocity.x, -1f);
        } else if (ceilDist - .2f == 0)
        {
            rdb.velocity = new Vector2(rdb.velocity.x, 0);
        }


        if (rdb.velocity.x > 0 && !facingRight)
        {
            Flip();
        } else if (rdb.velocity.x < 0 && facingRight)
        {
            Flip();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Detecting Player");
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player Projectile"))
        {
            Destroy(gameObject);
        }
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }




    /*private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            target = null;
        }
    }*/
}
