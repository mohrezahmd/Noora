using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    [SerializeField] private dataAssigner[] assigners;

    public float CallForData(string assignedObjName)
    {
        for (int i = 0; i < assigners.Length; ++i)
        {
            if (assignedObjName == assigners[i].assignerName)
            {
                return assigners[i].GetVerticalSpeed();
            }
        }
        return 0f;
    }

   public float SpeedAmount()
    {
        return GetComponent<dataAssigner>().GetVerticalSpeed();
    }
}
