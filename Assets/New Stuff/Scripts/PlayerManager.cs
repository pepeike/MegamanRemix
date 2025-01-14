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
    private bool inControl = true;
    float controlTime = 1f;
    float controlTimer = 1f;
    float dmgCooldown = 2f;
    float dmgTimer = 2f;
    bool invincible = false;

    [SerializeField]
    private GameObject shoot;
    [SerializeField]
    private GameObject projectile;

    private float shotCooldown = 2f;
    private float shotTimer;

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
        shotTimer = shotCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        xmov = Input.GetAxis("Horizontal");

        if (inControl)
        {
            myRigidBody.velocity = new Vector2(xmov * speed, myRigidBody.velocity.y);
        } else
        {
            controlTimer -= Time.deltaTime;
            if (controlTimer < .1f)
            {
                inControl = true;
                controlTimer = controlTime;
            }
        }
        

        Jump();


        if (Input.GetButton("Jump") && grounded)
        {
            //jump = true;
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0);
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
        }
        else
        {
            /*jump = false;
            doublejump = false;
            jumptime = 0;
            jumptimeside = 0;*/
        }

        shotTimer -= Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            if (shotTimer < 0)
            {
                //shoot.GetComponent<Animator>().SetTrigger("Shoot");

                if (!facingRight)
                {
                    GameObject go = (GameObject)Instantiate(projectile, shoot.transform.position, Quaternion.identity);
                    go.GetComponent<PlayerProjectile>().projectileSpeed *= -1;
                }
                else if (facingRight)
                {
                    GameObject go = (GameObject)Instantiate(projectile, shoot.transform.position, Quaternion.identity);
                    go.GetComponent<PlayerProjectile>().projectileSpeed *= 1;
                }

                shotTimer = shotCooldown;

            }
        }


    }

    private void FixedUpdate()
    {
        Reverser();
        anim.SetFloat("Velocity", Mathf.Abs(xmov));

        //myRigidBody.AddForce(new Vector2(xmov * 20 / (myRigidBody.velocity.magnitude + 1), 0));

        float distance;

        RaycastHit2D ray = Physics2D.Raycast(RayPositionCenter, Vector2.down, 10f, jumpableGround);

        distance = ray.distance - .2f;

        if (distance < 0)
        {
            distance = 5f;
        }

        //Debug.Log(distance);

        anim.SetFloat("Height", distance);

        if (invincible)
        {
            dmgTimer -= Time.deltaTime;
            
            if (dmgTimer < 0)
            {
                invincible = false;
                dmgTimer = dmgCooldown;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        foreach (ContactPoint2D contactPoint in collision.contacts)
        {
            if (contactPoint.collider.CompareTag("Terrain"))
            {
                grounded = true;
            }

            if ((contactPoint.collider.CompareTag("Damage") || contactPoint.collider.CompareTag("Enemy")) && !invincible)
            {
                Damage(1);

                inControl = false;

                invincible = true;

                float dir = (transform.position.x - collision.transform.position.x);

                Vector2 knockback = new Vector2(dir * 5f, 5f);

                Debug.Log(knockback);

                //myRigidBody.velocity.Set(dir * 100f, 5f);
                myRigidBody.AddForce(knockback, ForceMode2D.Impulse);

                

            }


        }






    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        if (grounded && collision.gameObject.CompareTag("Terrain"))
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    /*private void JumpRoutine(RaycastHit2D hit)
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
    }*/


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
            facingRight = true;
        }
        if (xmov < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            facingRight = false;
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
