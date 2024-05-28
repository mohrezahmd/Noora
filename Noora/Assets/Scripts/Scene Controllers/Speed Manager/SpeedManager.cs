using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    void Start()
    {

        GetComponent<ObjectSpawner>().speedDemandAction += SpeedAmount;
    }

   public float SpeedAmount()
    {
        return 3f;
    }
}
