using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JockDetection : MonoBehaviour
{

    public GameObject target;

    private void FixedUpdate()
    {
        if (transform.childCount == 0) {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            target = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            target = null;
        }
    }


}
