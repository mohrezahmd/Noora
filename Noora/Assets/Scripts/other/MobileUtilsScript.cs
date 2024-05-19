using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MobileUtilsScript : MonoBehaviour
{
    private float frequency = 1.0f;
    public Text myText;

    void Start()
    {
        StartCoroutine(FPS());
    }

    private IEnumerator FPS()
    {
        for (; ; )
        {
            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;

            // Display it

            string fps = "FPS: " + Mathf.RoundToInt(frameCount / timeSpan);
            myText.text = fps;
        }
    }

}