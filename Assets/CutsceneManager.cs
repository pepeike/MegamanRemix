using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour {

    public List<PlayableDirector> directors;
    private GameObject bufa;
    private GameObject goodEnding;
    private GameObject badEnding;
    private bool choiceMade = false;
    private int choiceDone = 0;


    private void Awake() {
        bufa = GameObject.Find("FalaAnfitriao2");
        bufa.SetActive(false);
        goodEnding = GameObject.Find("GoodEnding");
        goodEnding.SetActive(false);
        badEnding = GameObject.Find("BadEnding");
        badEnding.SetActive(false);
        directors[1].gameObject.SetActive(false);
    }



    // Update is called once per frame
    void FixedUpdate() {
        if (directors[0].state == PlayState.Paused) {
            //Debug.Log("Scene Ended");

            Destroy(GameObject.Find("FalaAnfitriao_0"));

            if (!choiceMade) {
                bufa.SetActive(true);
            }


            if (Input.GetKeyDown(KeyCode.S) && !choiceMade) {
                //Debug.Log("You accept");
                //Destroy(directors[2]);
                directors[0].gameObject.SetActive(false);
                bufa.SetActive(false);
                choiceMade = true;
                directors[1].gameObject.SetActive(true);
                directors[1].Play();
                choiceDone = 1;

            }

            if (Input.GetKeyDown(KeyCode.N) && !choiceMade) {
                //Debug.Log("You decline");
                //Destroy(directors[2]);
                badEnding.SetActive(true);
                directors[0].gameObject.SetActive(false);
                choiceMade = true;
                

                bufa.SetActive(false);
                choiceMade = true;
                directors[2].gameObject.SetActive(true);
                directors[2].Play();
                choiceDone = 2;

            }

            /*if (choiceMade && (directors[1].state == PlayState.Playing || directors[2].state == PlayState.Playing)) {

            }*/


        }

        if (directors[1].state == PlayState.Paused && choiceMade && choiceDone == 1) {
            goodEnding.SetActive(true);
            StartCoroutine(GoodEnding());
        }

        if (directors[2].state == PlayState.Paused && choiceMade && choiceDone == 2) {
            StartCoroutine(BadEnding());
        }


    }

    IEnumerator GoodEnding() {

        yield return new WaitForSeconds(5);
        Debug.Log("THE END");
        SceneManager.LoadScene(7);
        //CHANGE SCENE TO END
    }

    IEnumerator BadEnding() {
        yield return new WaitForSeconds(2);
        Debug.Log("BOSSFIGHT");
        CompleteLevel();
    }

    private void CompleteLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    

}
