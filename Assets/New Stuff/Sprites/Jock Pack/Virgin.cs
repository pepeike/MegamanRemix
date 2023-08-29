using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virgin : MonoBehaviour
{

    [SerializeField]
    public float virginSpeed = 1f;

    private float virginTime = 5f;
    private float timer;

    private void Awake()
    {
        timer = virginTime;
    }


    void Update()
    {

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            Destroy(gameObject);
        }

        


        transform.position += new Vector3(virginSpeed, 0, 0);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
