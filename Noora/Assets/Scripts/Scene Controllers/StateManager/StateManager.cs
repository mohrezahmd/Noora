using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    State state;
    [SerializeField] State nextState;

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

    public void SetState(State state)
    {
        this.state = state;
    }

    public State GetState()
    {
        return state;
    }

    public void Request(State nextState)
    {
        state = nextState;
        state.HandleRequest(nextState);
    }

    //IEnumerator Lose()
    //{
    //    gameIsPaused = true;
    //    yield return new WaitForSeconds(secondsAfterLose);
    //    globalEffects.SetActive(true);
    //    gameplay.SetActive(false);
    //    loseMenuUI.SetActive(true);
    //    AudioManager.instance.musicSource.gameObject.SetActive(false);
    //    AudioManager.instance.sfxSource.gameObject.SetActive(false);

    //    StartCoroutine(ScaleOut(loseMenuContainer));
    //    Time.timeScale = 0f;
    //}
}
