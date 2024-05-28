using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectAssigner : MonoBehaviour
{
    //[SerializeField] MultiSpawn spawnManager;


    [SerializeField] private float verticalSpeed, maxVerticalSpeed, minVerticalSpeed;
    [SerializeField] private float verticalSpeedRiseRate;
    [SerializeField] private float verticalSpeedFrameCounterLimit; // in frames
    [SerializeField] private int verticalSpeedFrameCounter = 0;

    
    void FixedUpdate()
    {
        if (verticalSpeedFrameCounter >= verticalSpeedFrameCounterLimit)
        {
            verticalSpeed += verticalSpeedRiseRate; // += 0.05f
            verticalSpeed = Mathf.Clamp(verticalSpeed, minVerticalSpeed, maxVerticalSpeed);
            //spawnManager.ManagerToSpawnerData(verticalSpeed);
            verticalSpeedFrameCounter = 0;
        }
        else
        {
            verticalSpeedFrameCounter++;
        }
    }
}
