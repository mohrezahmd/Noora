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
    [SerializeField] protected GameObject playerLight;
    [SerializeField] protected GameObject fader;
    [SerializeField] protected float secondsAfterLose = .2f;

    [SerializeField] protected int manualSceneNum = 0;

    protected float tmpAlpha;
    protected Color tmpColor;

    [SerializeField] protected GameObject spawnManager;
    [SerializeField] protected VFXManager vFXManager;

    protected virtual void Start()
    {
        //vFXManager = GetComponent<VFXManager>();
        switch (manualSceneNum)
        {
            case 0:
                AfterEnteringScene("MainMenuTheme1");
                break;
            case 1:
                AfterEnteringScene("GameplayTheme1");
                break;
        }

        StartCoroutine(vFXManager.FadeIn());
    }

    // Buttons{-------------------------||||||
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        globalEffects.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1f;
        playerLight.SetActive(true);
        vFXManager.LightOn(player);

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
        Debug.Log(5);

        //gameIsPaused = true;
        yield return new WaitForSeconds(secondsAfterLose);
        Debug.Log(6);

        globalEffects.SetActive(true);
        spawnManager.SetActive(false);
        loseMenuUI.SetActive(true);
        AudioManager.instance.musicSource.gameObject.SetActive(false);
        AudioManager.instance.sfxSource.gameObject.SetActive(false);

        StartCoroutine(vFXManager.ScaleOut(loseMenuContainer));
        //Time.timeScale = 0f;
        Debug.Log(7);


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


}