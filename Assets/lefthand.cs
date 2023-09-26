using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class lefthand : MonoBehaviour
{

    public GameObject shockwave;
    public righthand right;
    public Vector2 lefthandtarget =  new Vector2(61.8f, 13.25f);
    public Vector2 FollowTarget;
    public Rigidbody2D rb;
    public bool turn = true;
    public float followspeed = 0.03f;
    public LevelManager manager;
    public GameObject player;
    public CamerShake cam;
    public Collider2D thiscollider;
    public DetectGamer detect;
    public Animator anim;

    public Vector2 startingpos;

    private void Start()
    {
        startingpos = transform.position;
    }





    public enum MonsterStates
    {
        Neutral,
        Charging,
        Grounded,
        Returning


    }
    public MonsterStates state = MonsterStates.Neutral;

    #region State Machine
    private void Update()
    {

        
        player = detect.target;
        FollowTarget = new Vector2(player.transform.position.x, lefthandtarget.y);
        
        if (state == MonsterStates.Neutral && turn == true)

        {

            state = MonsterStates.Charging;
            StartCoroutine(Charging());
            Debug.Log("Charging");
        }

        if (state == MonsterStates.Grounded)
        {
            StartCoroutine(GettingUp());
        }

        switch (state)
        {
            case MonsterStates.Charging:
               transform.position =  Vector2.MoveTowards(transform.position, FollowTarget, followspeed);

           break;
        }

    }


    #endregion




    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collided");
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Debug.Log("SEXO");
            SpawnShockwave();
            cam.ScreenShake();
        }

        if (collision.gameObject.CompareTag("Damage"))
        {
            Physics2D.IgnoreCollision(thiscollider, collision.collider);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(thiscollider, collision.collider);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("Ass");
    }


    private void SpawnShockwave()
    {
        GameObject go1 = (GameObject)Instantiate(shockwave, transform.position, Quaternion.identity);
        GameObject go2 = (GameObject)Instantiate(shockwave, transform.position, Quaternion.identity);
        go1.GetComponent<shockwave>().shockSpeed = 2.0f;
        go2.GetComponent<shockwave>().shockSpeed = -2.0f;
    }

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
        transform.position = Vector2.MoveTowards(transform.position, lefthandtarget, 0.01f);
        if (transform.position.x == lefthandtarget.x && transform.position.y == lefthandtarget.y)
        {
            state = MonsterStates.Neutral;
            turn = false;
            right.turn = true;

        }
    }

    
        
































    

}
