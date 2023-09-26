using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{

    public int BossHealth = 12;
    public bool bossvul = false;
    public Animator anim;
    public float blinktimer = 12;



    private void Update()
    {

        #region Blink
        if (blinktimer > 0)
        {
            blinktimer -= Time.deltaTime;
        }

        else
        {
            anim.SetTrigger("Tim2blink");
            blinktimer = 12;
        }

        #endregion


        if (BossHealth == 6)
        {
            bossvul = true;
            gameObject.tag = "Interactable";
        }

        if(BossHealth == 3)
        {
            bossvul = true;
            gameObject.tag = "Interactable";
        }

        if (BossHealth == 0)
        {
            bossvul = true;
            gameObject.tag = "Interactable";
            SceneManager.LoadScene(7);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Projectile") && bossvul == false)
            {
               BossHealth -= 1;
                Destroy(collision.gameObject);
            }

 
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && bossvul == true)
        {
            BossHealth -= 1;
            Debug.Log("Hit");
            bossvul = false;
        }
    }

    private IEnumerator Blink()
    {
        yield return new WaitForSeconds(5);
        anim.SetTrigger("Tim2blink");

    }


}
