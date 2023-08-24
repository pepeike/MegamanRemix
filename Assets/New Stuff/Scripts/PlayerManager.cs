using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private static float maxHP = 10;
    public static float playerHP = 10;

    [SerializeField]
    private float speed = 5f;

    public Rigidbody2D myRigidBody;
    float xmov;
    private bool grounded;
    private bool facingRight = true;
    bool jump, doublejump;
    float jumptime, jumptimeside;

    [SerializeField]
    private float rayLength;
    [SerializeField]
    private float rayPositionOffset;

    Vector3 RayPositionCenter;
    Vector3 RayPositionLeft;
    Vector3 RayPositionRight;

    private CapsuleCollider2D coll;

    [SerializeField]
    private LayerMask jumpableGround;

    RaycastHit2D[] GroundHitsCenter;
    RaycastHit2D[] GroundHitsLeft;
    RaycastHit2D[] GroundHitsRight;

    RaycastHit2D[][] AllRaycastHits = new RaycastHit2D[3][];

    

    [SerializeField]
    private Animator anim;

    //public GameObject health;
    //public Health lifebar;

    [SerializeField]
    private float jumpForce = 10f;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xmov = Input.GetAxis("Horizontal");

        myRigidBody.velocity = new Vector2(xmov * speed, myRigidBody.velocity.y);

        /*if (xmov > 0 && !facingRight)
        {
            Flip();
        }
        if (xmov < 0 && facingRight)
        {
            Flip();
        }*/

        Jump();

        

        
        /*if (Input.GetButtonDown("Jump") && grounded)
        {
            //myRigidBody.velocity = Vector3.up * jumpForce;
            if (jumptime < 0.1f)
            {
                doublejump = true;
            }
        }*/
        

        if (Input.GetButton("Jump") && grounded)
        {
            //jump = true;
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0);
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
        } else
        {
            /*jump = false;
            doublejump = false;
            jumptime = 0;
            jumptimeside = 0;*/
        }

        

    }

    private void FixedUpdate()
    {
        Reverser();
        anim.SetFloat("Velocity", Mathf.Abs(xmov));

        myRigidBody.AddForce(new Vector2(xmov * 20 / (myRigidBody.velocity.magnitude + 1), 0));

        float distance;

        RaycastHit2D ray = Physics2D.Raycast(RayPositionCenter, Vector2.down, 10f, jumpableGround);

        distance = ray.distance - .2f;

        if (distance < 0)
        {
            distance = 5f;
        }

        Debug.Log(distance);

        anim.SetFloat("Height", distance);

        

        

        /*RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, Vector2.down);

        

        if (hit)
        {
            Debug.Log("Hitting");
            anim.SetFloat("Height", hit.distance);
            JumpRoutine(hit);
        }

        RaycastHit2D hitright;
        hitright = Physics2D.Raycast(transform.position + Vector3.up * 0.5f, transform.right, 1);

        if (hitright)
        {
            if (hitright.distance < 0.3f)
            {
                JumpRoutineSide(hitright);
            }
        }*/

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (grounded && collision.gameObject.CompareTag("Terrain"))
        {
            grounded = false;
        }
    }

    private void JumpRoutine(RaycastHit2D hit)
    {
        if (hit.distance < .1f)
        {
            jumptime = 1;
        }

        if (jump)
        {
            jumptime = Mathf.Lerp(jumptime, 0, Time.fixedDeltaTime * 10);
            myRigidBody.AddForce(Vector2.up * jumptime, ForceMode2D.Impulse);
        }

    }

    private void JumpRoutineSide(RaycastHit2D hitside)
    {
        if (hitside.distance < 0.3f)
        {

            jumptimeside = 1;

        }

        if (doublejump)
        {
            PhysicalReverser();
            jumptimeside = Mathf.Lerp(jumptimeside, 0, Time.fixedDeltaTime * 10);
            myRigidBody.AddForce((hitside.normal * 50 + Vector2.up * 80) * jumptimeside);
        }
    }


    private void Damage(int damage)
    {
        playerHP -= damage;

        if (playerHP <= 0)
        {
            Dead();
            playerHP = maxHP;
        }

    }

    private void Dead()
    {
        Destroy(gameObject);
    }

    private void Reverser()
    {
        if (xmov > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (xmov < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void PhysicalReverser()
    {
        if (myRigidBody.velocity.x > .1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (myRigidBody.velocity.x < .1f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    private void Jump()
    {
        RayPositionCenter = transform.position + new Vector3(0, -.5f, 0);
        RayPositionLeft = transform.position + new Vector3(-rayPositionOffset, -.5f, 0);
        RayPositionRight = transform.position + new Vector3(rayPositionOffset, -.5f, 0);

        GroundHitsCenter = Physics2D.RaycastAll(RayPositionCenter, Vector2.down, rayLength);
        GroundHitsLeft = Physics2D.RaycastAll(RayPositionLeft, Vector2.down, rayLength);
        GroundHitsRight = Physics2D.RaycastAll(RayPositionRight, Vector2.down, rayLength);

        AllRaycastHits[0] = GroundHitsCenter;
        AllRaycastHits[1] = GroundHitsLeft;
        AllRaycastHits[2] = GroundHitsRight;

        grounded = GroundCheck(AllRaycastHits);

        Debug.DrawRay(RayPositionCenter, Vector2.down * rayLength, Color.red);
        Debug.DrawRay(RayPositionLeft, Vector2.down * rayLength, Color.red);
        Debug.DrawRay(RayPositionRight, Vector2.down * rayLength, Color.red);

        
        

    }

    private bool GroundCheck(RaycastHit2D[][] GroundHits)
    {
        

        foreach (RaycastHit2D[] Hitlist in GroundHits)
        {
            foreach (RaycastHit2D hit in Hitlist)
            {
                if (hit.collider != null && hit.collider.tag != "Player")
                {
                    return true;
                }
            }
        }

        return false;

    }

}
