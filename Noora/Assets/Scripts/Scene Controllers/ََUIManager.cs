using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //public static VFXManager instance;
    public static bool gameIsPaused = false;
    public MainManager manager;
    [SerializeField] protected GameObject pauseMenuUI, loseMenuUI, loseMenuContainer;
    [SerializeField] protected Player player;

    //private float defaultSpeed;

    [SerializeField] protected GameObject globalEffects;
    [SerializeField] protected GameObject playerLight;
    [SerializeField] protected GameObject fader;
    [SerializeField] protected float secondsAfterLose = .2f;
    [SerializeField] protected GameObject particleS;
    [SerializeField] protected GameObject sides;
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

        //fader.SetActive(true);
        StartCoroutine(vFXManager.FadeIn(1, 0, fader));
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
        vFXManager.LightOff(player);
        Time.timeScale = .01f;
    }

    public void Home()
    {
        Time.timeScale = 1f;
        BeforeEnteringScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Game()
    {
        BeforeEnteringScene(1);
    }
    // Buttons \\ }----------------------||||||


    public virtual IEnumerator Lose()
    {
        yield return new WaitForSeconds(secondsAfterLose);

        particleS.SetActive(false);
        spawnManager.SetActive(false);
        player.gameObject.SetActive(false);

        globalEffects.SetActive(true);
        loseMenuUI.SetActive(true);
        MainManager.instance.StopBackground();
        sides.SetActive(false);

        //StartCoroutine(vFXManager.ScaleOut(loseMenuContainer));

    }

    protected void BeforeEnteringScene(int sceneNum)
    {
        AudioManager.instance.activateAudioObject(false);
        SceneManager.LoadScene(sceneNum);
    }

    protected virtual void AfterEnteringScene(string sceneName)
    {
        AudioManager.instance.activateAudioObject(true);
        AudioManager.instance.PlayMusic(sceneName);
    }


}