using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Power Up SO", menuName = "ScriptableObjects/PowerUpSO", order = 1)]
public class PowerUp_SO : ScriptableObject
{
    public new string name;
    public float activeTime;

    public virtual void Activate(GameObject parent){
    }

}
