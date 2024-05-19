using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MultiSpawn : MonoBehaviour
{
    [Header("Spawner Type:")]
    [SerializeField] ObjectSpawner[] objectSpawners;

    [SerializeField] GameObject player, manager;

    public void ManagerToSpawnerData(float verticalSpeed) {
        for (int i = 0; i < objectSpawners.Length; ++i)
        {
            objectSpawners[i].ManagerToSpawnerData(verticalSpeed);
        }
    }

    public void PushToDeactivate(GameObject toPushObject)
    {
        for (int i = 0; i < objectSpawners.Length; ++i)
        {
            if (toPushObject.tag == objectSpawners[i].objectName)
            {
                objectSpawners[i].PushToDeactivate(toPushObject);
            }
        }
    }



}
