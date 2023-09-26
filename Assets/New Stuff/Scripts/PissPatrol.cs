using UnityEngine;

public class PissPatrol : MonoBehaviour {
    
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject pointA;
    [SerializeField]
    private GameObject pointB;

    private Transform currentPoint;

    //private Animator anim;

    [SerializeField]
    private float moveSpeed;


    private void Start () {

        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        currentPoint = pointA.transform;

    }

    private void Update() {

        if (currentPoint == pointA.transform) {

            rb.velocity = new Vector2(moveSpeed, 0);

        }
        if (currentPoint == pointB.transform) {

            rb.velocity = new Vector2(-moveSpeed, 0);

        }

        if (Vector2.Distance(transform.position, currentPoint.position) < .2f && currentPoint == pointA.transform) {

            Flip();
            currentPoint = pointB.transform;
            
        } else if (Vector2.Distance(transform.position, currentPoint.position) < .2f && currentPoint == pointB.transform) {

            Flip();
            currentPoint = pointA.transform;

        }


        

    }

    private void Flip() {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player Projectile")) {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.1f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.1f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    

}
