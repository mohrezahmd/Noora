using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpSO", menuName = "ScriptableObjects/ShrinkPO", order = 1)]
public class ShrinkPowerUp : PowerUp_SO
{ 
    public override void Activate(GameObject parent)
    {
        Debug.Log("shrink power up");

    }
}
