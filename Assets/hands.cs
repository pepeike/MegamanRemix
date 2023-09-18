using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hands : MonoBehaviour
{

    public GameObject shockwave;
    



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

    

}
