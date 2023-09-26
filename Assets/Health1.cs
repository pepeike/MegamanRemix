using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Health1 : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    
    PlayerManager manager;
    [SerializeField]
    Animator health;
    



    private void Awake()
    {
        manager = player.GetComponent<PlayerManager>();
        //health.StartPlayback();
    }


    // Start is called before the first frame update
    void Start()
    {
        health.Play("Health");
        
    }

    // Update is called once per frame
    void Update()
    {

        //float health = PlayerManager.playerHP;

        //Debug.Log(health.ToString());

        /*if (manager.playerHP < 5)
        {
            Debug.Log("less than 5 hp");
        }*/

        //UpdateHealth(manager);

       switch(Mathf.RoundToInt(PlayerMini.playerHP))
        {
            case 10:
                health.Play("Health");
                break;
            case 9:
                health.Play("Health9");
                break;
            case 8:
                health.Play("Health8");
                break;
            case 7:
                health.Play("Health7");
                break;
            case 6:
                health.Play("Health6");
                break;
            case 5:
                health.Play("Health5");
                break;
            case 4:
                health.Play("Health4");
                break;
            case 3:
                health.Play("Health3");
                break;
            case 2:
                health.Play("Health2");
                break;
            case 1:
                health.Play("Health1");
                break;
            case 0:
                health.Play("Health0");
                break;
        }


    }

    /*public void UpdateHealth(PlayerManager playerManager)
    {
        if (playerManager.playerHP == 5)
        {
            health.Play("Health5");
        }
    }*/

    

}
