using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //public static VFXManager instance;
    public static bool gameIsPaused = false;
    public MainManager manager;
    [SerializeField] protected GameObject pauseMenuUI, loseMenuUI, loseMenuContainer;
    [SerializeField] protected Player player;

    //private float defaultSpeed;

    [SerializeField] protected GameObject BG;
    [SerializeField] protected GameObject globalEffects;
    [SerializeField] protected GameObject gameplay;
    [SerializeField] protected GameObject playerLight;
    [SerializeField] protected GameObject fader;
    [SerializeField] protected float secondsAfterLose = .2f;

    [SerializeField] protected int manualSceneNum = 0;
    //public UnityEvent unityEvent;

    protected float tmpAlpha;
    protected Color tmpColor;

    protected virtual void Start()
    {
        switch (manualSceneNum)
        {
            case 0:
                AfterEnteringScene("MainMenuTheme1");
                break;
            case 1:
                AfterEnteringScene("GameplayTheme1");
                break;
        }

       StartCoroutine(FadeIn());
    }

    // Buttons{-------------------------||||||
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        globalEffects.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1f;
        playerLight.SetActive(true);
        LightOn(player);
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        globalEffects.SetActive(true);
        gameIsPaused = true;
        playerLight.SetActive(false);
        Time.timeScale = .01f;
    }

    public void Home()
    {
        Time.timeScale = 1f;
        //SceneManager.LoadScene(0);
        BeforeEnteringScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Game()
    {
        //SceneManager.LoadScene(1);
        BeforeEnteringScene(1);
    }
    // Buttons \\ }----------------------||||||


    public virtual IEnumerator Lose()
    {
        //gameIsPaused = true;
        yield return new WaitForSeconds(secondsAfterLose);
        globalEffects.SetActive(true);
        //gameplay.SetActive(false);
        loseMenuUI.SetActive(true);
        AudioManager.instance.musicSource.gameObject.SetActive(false);
        AudioManager.instance.sfxSource.gameObject.SetActive(false);

        StartCoroutine(ScaleOut(loseMenuContainer));
        //Time.timeScale = 0f;
    }


    void LightOff(OperativeEntity _ally)
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

    void LightOn(OperativeEntity _ally)
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

    protected void BeforeEnteringScene(int sceneNum)
    {
        AudioManager.instance.musicSource.gameObject.SetActive(false);
        AudioManager.instance.sfxSource.gameObject.SetActive(false);
        SceneManager.LoadScene(sceneNum);
    }

    protected virtual void AfterEnteringScene(string sceneName)
    {
        AudioManager.instance.musicSource.gameObject.SetActive(true);
        AudioManager.instance.sfxSource.gameObject.SetActive(true);

        AudioManager.instance.PlayMusic(sceneName);
    }




    // Visual Effects : --------------+++++++++++++++++++++++++++++++

    [Header("Fade In")]
    [SerializeField] float limitAlfa = 0f;
    [SerializeField] float stepAlfa = .05f;
    [SerializeField] float stepDelay = .1f;
    [SerializeField] float funcDelay = .1f;
    public virtual IEnumerator FadeIn()
    {
        fader.SetActive(true);
        tmpColor.a = 1;
        Color c = fader.GetComponent<Image>().color;
        for (float alpha = 1f; alpha > limitAlfa; alpha -= stepAlfa)
        {
            c.a = alpha;
            fader.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(stepDelay);
        }
        c.a = 0;
        fader.GetComponent<Image>().color = c;
        fader.SetActive(false);
        yield return new WaitForSeconds(funcDelay);
    }

    public virtual IEnumerator FadeOut(int sceneNum)
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
    [SerializeField] float tmpScaleInitial = .1f;
    [SerializeField] float tmpLimit = 1f;
    [SerializeField] float tmpIncreament = 0.05f;
    [SerializeField] float waitingTime = 0.05f;
    public virtual IEnumerator ScaleOut(GameObject toScaleObj)
    {
        //float tmpScale = .1f;
        Vector3 c = new Vector3(tmpScaleInitial, tmpScaleInitial, tmpScaleInitial);

        toScaleObj.transform.localScale = c;

        for (float tmpScale = tmpScaleInitial; tmpScale < tmpLimit; tmpScale += tmpIncreament)
        {
            c = new Vector3(tmpScale, tmpScale, tmpScale);
            toScaleObj.transform.localScale = c;

            yield return new WaitForSeconds(waitingTime);
        }
    }
    // Visual Effects : --------------+++++++++++++++++++++++++++++++




}