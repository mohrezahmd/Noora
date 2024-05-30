using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{

    GameObject debugMiniPanel;

    List<GameObject> miniPanelDebuggers;

    void Start()
    {
        miniPanelDebuggers = new List<GameObject>();
    }

    public GameObject createDebugPanelForEntity(GameObject operativeEntity)
    {
        debugMiniPanel = Instantiate(debugMiniPanel, operativeEntity.transform, true);
        miniPanelDebuggers.Add(debugMiniPanel);
        return debugMiniPanel;
    }
}
