using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : MonoBehaviour, MainState
{
    [SerializeField] protected float secondsAfterLose = .2f;
    

    public void HandleRequest()
    {

    }

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
}
