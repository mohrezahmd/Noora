using UnityEngine;

public class dataAssigner : MonoBehaviour
{
    //[SerializeField] SpawnManager spawnManager;

    [SerializeField] public string assignerName;
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
            verticalSpeedFrameCounter = 0;
        }
        else
        {
            verticalSpeedFrameCounter++;
        }
    }

    public float GetVerticalSpeed()
    {
        return verticalSpeed;
    }
}
