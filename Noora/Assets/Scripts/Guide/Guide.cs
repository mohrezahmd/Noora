using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    public float waitTime;
    public UnityEngine.GameObject guideRight, guideLeft;

    [Range(0.1f,2)]
    public float modifiedScale;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("isLeftRightGuideDone") && PlayerPrefs.GetInt("isLeftRightGuideDone") == 0)
        {
            guideRight.SetActive(true);
            modifiedScale = 0.3f;
            Time.timeScale = modifiedScale;
        }

    }

    public void RightGuideFinished()
    {
        StartCoroutine(IERightGuideFinished(waitTime));
    }

    public void LeftGuideFinished()
    {
        StartCoroutine(IELeftGuideFinished(waitTime));
    }

    public IEnumerator IELeftGuideFinished(float sec)
    {
        yield return new WaitForSeconds(sec);
        guideLeft.SetActive(false);
        modifiedScale = 1;
        Time.timeScale = modifiedScale;
        PlayerPrefs.SetInt("isLeftRightGuideDone",1);
    }

    public IEnumerator IERightGuideFinished(float sec)
    {
        yield return new WaitForSeconds(sec);
        guideRight.SetActive(false);
        guideLeft.SetActive(true);
    }

}
