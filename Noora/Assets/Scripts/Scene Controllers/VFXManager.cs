using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class VFXManager : MonoBehaviour
{
    [SerializeField] protected float secondsAfterLose = .2f;

    [SerializeField] protected int manualSceneNum = 0;

    protected float tmpAlpha;
    protected Color tmpColor;


    [Header("Fade In")]
    [SerializeField] float fadeInDuration = 1.0f;

    public float mixValue;

    public Color colourMix;
    public Color colourA;
    public Color colourB;

    public float lerpedValue;

    public IEnumerator FadeIn(float start, float end, GameObject fader)
    {
        fader.SetActive(true);
        float timeElapsed = 0;

        while (timeElapsed <= fadeInDuration)
        {
            float t = timeElapsed / fadeInDuration;
            fader.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(start, end, t));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        fader.GetComponent<CanvasRenderer>().SetAlpha(end);
        fader.SetActive(false);
    }

    public IEnumerator FadeOut(int sceneNum, GameObject fader)
    {
        fader.SetActive(true);
        tmpColor.a = 0;
        Color c = fader.GetComponent<Image>().color;

        for (float alpha = 0f; alpha <= 1f; alpha += 0.05f)
        {
            tmpColor.a = alpha;
            fader.GetComponent<Image>().color = tmpColor;
            yield return new WaitForSeconds(.1f);
        }
        c.a = 1;
        fader.GetComponent<Image>().color = c;

    }


    [Header("Scale Out")]
    [SerializeField] float scaleOutDuration = 1f;
    [SerializeField] float scale1num, scale2num;

    public IEnumerator ScaleOut(GameObject toScaleObj)
    {

        Vector3 scale1 = new Vector3(scale1num, scale1num, scale1num);
        Vector3 scale2 = new Vector3(scale2num, scale2num, scale2num);

        toScaleObj.SetActive(true);
        float timeElapsed = 0;

        toScaleObj.transform.localScale = scale1;

        while(timeElapsed < scaleOutDuration)
        {
            float t = timeElapsed/scaleOutDuration;
            toScaleObj.transform.localScale = Vector3.Lerp(scale1, scale2, t);
            timeElapsed += Time.deltaTime;

            yield return null;
            Debug.Log("timeElapsed: " + timeElapsed);
            Debug.Log("scale duration: " + scaleOutDuration);
            Debug.Log("local scale of lerp: " + toScaleObj.transform.localScale);
        }
            Debug.Log("2. local scale of lerp: " + toScaleObj.transform.localScale);

        toScaleObj.transform.localScale = scale2;
            Debug.Log("3. local scale of lerp: " + toScaleObj.transform.localScale);
    }

    public void LightOff(OperativeEntity _ally)
    {
        ally[] allies;
        if (_ally != null)
        {
            if (_ally.GetComponentsInChildren<ally>() != null)
            {
                allies = _ally.GetComponentsInChildren<ally>();

                for (int i = 0; i < allies.Length; i++)
                {
                    allies[i].myLight.gameObject.SetActive(false);
                }
            }
        }
    }

    public void LightOn(OperativeEntity _ally)
    {
        ally[] allies;
        if (_ally != null)
        {
            if (_ally.GetComponentsInChildren<ally>() != null)
            {
                allies = _ally.GetComponentsInChildren<ally>();

                for (int i = 0; i < allies.Length; i++)
                {
                    allies[i].myLight.gameObject.SetActive(true);
                }
            }
        }
    }
}


