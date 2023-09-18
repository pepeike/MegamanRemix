using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shockwave : MonoBehaviour
{

    [HideInInspector]
    public float shockSpeed;
    [SerializeField]
    private float shockForce = 10;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(shockwaveTimer());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            float dif = shockSpeed/2;
            collision.attachedRigidbody.velocity = new Vector2(dif * shockForce, shockForce);

            collision.GetComponent<PlayerManager>().inControl = false;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.right * shockSpeed;
    }

    IEnumerator shockwaveTimer()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }

}
