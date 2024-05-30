using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    [SerializeField] private dataAssigner[] assigners;
    float sss;
    DebugMiniPanel debuggerMini;

    private void Start()
    {
        debuggerMini = GetComponent<DebugMiniPanel>();

        //for (int i = 0; i < assigners.Length; ++i)

        //{
        //   sss =  assigners[i].GetVerticalSpeed();
        //}
    }

    public float CallForData(string assignedObjName)
    {
        for (int i = 0; i < assigners.Length; ++i)
        {
            if (assignedObjName == assigners[i].assignerName)
            {
                float tmpSpeed = assigners[i].GetVerticalSpeed();
                return tmpSpeed;
            }
        }
        return 0f;
    }

   public float SpeedAmount()
    {
        return GetComponent<dataAssigner>().GetVerticalSpeed();
    }
}
