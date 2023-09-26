using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopyGuy : MonoBehaviour
{
    public GameObject Output1;
    public GameObject Output2;
    public GameObject Output3;
    public GameObject Virgin;
    public Animator anim;
    float Shootime = 3;
    float shootimer = 3;

    //alterações da pep
    public GameObject target;
    [SerializeField]
    private JockDetection detection;
    private bool facingLeft = false;


    private void Awake() {
        detection = GetComponentInParent<JockDetection>();

    }

    private void Start() {

    }
    private void Update() {




        if (target) {
            Vector3 dif = target.transform.position - transform.position;

            if (dif.x > 0 && !facingLeft) {
                Flip();
            } else if (dif.x < 0 && facingLeft) {
                Flip();
            }

        }


    }

    private void FixedUpdate() {

        if (detection.target != null) {
            target = detection.target;
        } else if (detection.target == null) {
            target = null;
        }


        if (target) {
            Shootime -= Time.deltaTime;

            if (Shootime < 0) {
                //anim.SetTrigger("ToShoot");

                Shoot();


                Shootime = shootimer;
            }
        }







    }

    void Shoot() {
        if (!facingLeft) {
            GameObject go2 = (GameObject)Instantiate(Virgin, Output1.transform.position, Quaternion.identity);
            go2.GetComponent<PoopyProjectile>().poopySpeed = new Vector2(-7.14f, 7f);
            GameObject go1 = (GameObject)Instantiate(Virgin, Output2.transform.position, Quaternion.identity);
            go1.GetComponent<PoopyProjectile>().poopySpeed = new Vector2(-9.37f, 3.5f);
            GameObject go3 = (GameObject)Instantiate(Virgin, Output3.transform.position, Quaternion.identity);
            go3.GetComponent<PoopyProjectile>().poopySpeed = new Vector2(-10f, 0f);
        } else {
            GameObject go2 = (GameObject)Instantiate(Virgin, Output1.transform.position, Quaternion.identity);
            go2.GetComponent<PoopyProjectile>().poopySpeed = new Vector2(7.14f, 7f);
            GameObject go1 = (GameObject)Instantiate(Virgin, Output2.transform.position, Quaternion.identity);
            go1.GetComponent<PoopyProjectile>().poopySpeed = new Vector2(9.37f, 4f);
            GameObject go3 = (GameObject)Instantiate(Virgin, Output3.transform.position, Quaternion.identity);
            go3.GetComponent<PoopyProjectile>().poopySpeed = new Vector2(10f, 0f);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {



        if (collision.gameObject.CompareTag("Player Projectile")) {
            Destroy(gameObject);
        }
    }

    

    private void Flip() {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingLeft = !facingLeft;
    }

    
}
