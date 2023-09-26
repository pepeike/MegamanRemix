using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerShake : MonoBehaviour
{

    public AnimationCurve curve;


    public void ScreenShake()
    {
        StartCoroutine(Shake());
    }


    IEnumerator Shake()
    {
        float duration = 1f;
        float elapsedtime = 0f;
        Vector3 startingpos = transform.position;

        while (elapsedtime < duration)
        {
            elapsedtime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedtime / duration);
            transform.position = startingpos + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startingpos;
    }
}
