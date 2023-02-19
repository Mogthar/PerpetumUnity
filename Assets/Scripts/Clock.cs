using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{

    [SerializeField] float loopLength = 60.0f;
    public static float loopTime = 0.0f;
    [SerializeField] Material sineWaveMaterial;

    // Update is called once per frame
    void Update()
    {
        loopTime += Time.deltaTime;
        // maybe wrap into a method
        // or have a separate materials manager and have this be an event
        sineWaveMaterial.SetFloat("LoopTime", loopTime);
        if(loopTime > loopLength)
        {
            RestartLoop();
        }
    }

    private void RestartLoop()
    {
        loopTime = 0.0f;
    }
}
