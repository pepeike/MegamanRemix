using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class righthand : MonoBehaviour
{
    public GameObject shockwave;
    public lefthand left;
    public bool turn = false;
    public Rigidbody2D rb;
    public Vector2 FollowTarget;
    public Vector2 righthandtarget = new Vector2 (68.77f, 13.25f);
    public float followspeed = 0.03f;
    public CamerShake cam;
    public Collider2D thiscollider;
    public Animator anim;

    public enum MonsterStates
    {
        Neutral,
        Charging,
        Grounded,
        Returning
    }

    public MonsterStates state = MonsterStates.Neutral;

    public void Update()
    {

        FollowTarget = new Vector2(left.player.transform.position.x, left.lefthandtarget.y);

        switch (state)
        {

            case MonsterStates.Neutral:
               if (turn == true)
                {
                    
                    state = MonsterStates.Charging;
                    StartCoroutine(Charging());

                }
            break;

            case MonsterStates.Charging:
                transform.position = Vector2.MoveTowards(transform.position, FollowTarget, followspeed);

            break;

            case MonsterStates.Grounded:

                StartCoroutine(GettingUp());

           break;


            
             

           






        }
        
        

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collided");
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Debug.Log("SEXO");
            SpawnShockwave();
            cam.ScreenShake();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(thiscollider, collision.collider);
        }
    }



    private void SpawnShockwave()
    {
        GameObject go1 = (GameObject)Instantiate(shockwave, transform.position, Quaternion.identity);
        GameObject go2 = (GameObject)Instantiate(shockwave, transform.position, Quaternion.identity);
        go1.GetComponent<shockwave>().shockSpeed = 2.0f;
        go2.GetComponent<shockwave>().shockSpeed = -2.0f;
    }


    #region Coroutines & Functions
    IEnumerator Charging()
    {
        yield return new WaitForSeconds(10);
        fall();
    }

    IEnumerator GettingUp()
    {
        yield return new WaitForSeconds(5);
        GetUp();
    }

    private void fall()
    {
        anim.SetBool("Falling", true);
        state = MonsterStates.Grounded;
        rb.gravityScale = 1f;
    }

    private void GetUp()
    {
        anim.SetBool("Falling", false);
        state = MonsterStates.Returning;
        rb.gravityScale = 0f;
        transform.position = Vector2.MoveTowards(transform.position, righthandtarget, 0.01f);
        if (transform.position.x == 68.77f && transform.position.y == 13.25f)
        {
            state = MonsterStates.Neutral;
            turn = false;
            left.turn = true;

        }
    }



    #endregion
}
