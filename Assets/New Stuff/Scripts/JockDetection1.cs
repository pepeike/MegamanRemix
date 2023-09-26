using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JockDetection1 : MonoBehaviour
{

    public GameObject target2;

    private void FixedUpdate()
    {
        if (transform.childCount == 0) {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            target2 = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            target2 = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            target2 = null;
        }
    }


}
