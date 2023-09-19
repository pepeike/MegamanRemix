using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class lefthand : MonoBehaviour
{

    public GameObject shockwave;
    public righthand right;
    public Vector2 lefthandtarget = new Vector2(61.54f, 10.44f);
    public Rigidbody2D rb;

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
        if (state == MonsterStates.Neutral)
        {
            state = MonsterStates.Charging;
            StartCoroutine(Charging());
            Debug.Log("Charging");
        }

        if (state == MonsterStates.Grounded)
        {
            StartCoroutine(GettingUp());
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

        state = MonsterStates.Grounded;
        rb.gravityScale = 1f;
    }

    
    private void GetUp()
    {
        state = MonsterStates.Returning;
        rb.gravityScale = 0f;
        transform.position = Vector3.MoveTowards(transform.position, lefthandtarget, 3f);
    }
        
































    

}
