using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopyProjectile : MonoBehaviour
{
    [SerializeField]
    public Vector2 poopySpeed;

    private float virginTime = 5f;
    private float timer;

    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        timer = virginTime;
    }

    private void Start() {


        rb.velocity = Vector2.one * poopySpeed;
    }


    void Update() {

        timer -= Time.deltaTime;

        if (timer < 0) {
            Destroy(gameObject);
        }




        //transform.position += new Vector3(virginSpeed, 0, 0);
    }




    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Terrain")) {

            Destroy(gameObject);

        }
    }
}
