using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGamer : MonoBehaviour
{
    public GameObject target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.gameObject;        
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        target = null;
    }


}
