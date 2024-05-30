using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DebugMiniPanel : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] bool miniPanelIsOn = false;

    private void Start()
    {

    }

    public void SetTextToShow(float tmpSpeed)
    {
        text.text = tmpSpeed.ToString();
    }

}
