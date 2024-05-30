using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;
    //public static List<GameObject> allEntities;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject ally;

    [SerializeField] private GameObject shrinker;
    //[SerializeField] private GameObject sideRight;
    //[SerializeField] private GameObject sideLeft;
    [SerializeField] private GameObject sides;

    SpawnManager spawnManager;
    SpeedManager speedManager;
    UIManager uiManager;
    VFXManager vFXManager;

    [SerializeField] GameObject UIText;

    [SerializeField] GameObject[] borders;  // minXObj, maxXObj, minYObj, maxYObj
    private float minX, maxX, minY, maxY;

    private Text scoreText, highScoreText;
    private int highScore = 0;
    private int score = 0;

    [SerializeField] GameObject BG;
    [SerializeField] float backgroundSpeed = .5f;
    Material bgMaterial;
    Vector2 bgOffset;

    //[SerializeField] private float verticalSpeed, maxVerticalSpeed, minVerticalSpeed;
    //[SerializeField] private float verticalSpeedRiseRate;
    //[SerializeField] private float verticalSpeedFrameCounterLimit; // in frames
    //[SerializeField] private int verticalSpeedFrameCounter = 0;

    [SerializeField] int howManyTimesIn1Sec;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject, 0.1f);
        }
    }
    void Start()
    {
        spawnManager = gameObject.GetComponent<SpawnManager>();
        speedManager = gameObject.GetComponent<SpeedManager>();
        uiManager = gameObject.GetComponent<UIManager>();
        vFXManager = gameObject.GetComponent<VFXManager>();

        //PlayerPrefs.SetInt("HighScore",0);
        highScoreText = UIText.GetComponentsInChildren<Transform>()[1].GetComponent<Text>();
        scoreText = UIText.GetComponentsInChildren<Transform>()[2].GetComponent<Text>();
        //errorText = UIText.GetComponentsInChildren<Transform>()[3].GetComponent<Text>();

        minX = borders[0].GetComponent<Border>().transform.position.x;
        maxX = borders[1].GetComponent<Border>().transform.position.x;
        minY = borders[2].GetComponent<Border>().transform.position.y;
        maxY = borders[3].GetComponent<Border>().transform.position.y;

        //spawnManager.CallManagerForSpeedData(verticalSpeed);

        bgMaterial = BG.GetComponent<Renderer>().material;
        bgOffset = new Vector2(0f, backgroundSpeed);

        sides.GetComponentsInChildren<SideCollider>()[0].setRelatives(player, gameObject);
        sides.GetComponentsInChildren<SideCollider>()[1].setRelatives(player, gameObject);

        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = highScore.ToString();
        score = 0;

    }

    void s(float _backgroundSpeed)
    {
        bgOffset = new Vector2(0, _backgroundSpeed);

    }

    private void Update()
    {
        bgMaterial.mainTextureOffset += bgOffset * Time.deltaTime;
    }

    public void CallToSpawner(GameObject spawnedObject)
    {
        spawnManager.PushToDeactivate(spawnedObject);
    }

    public float CallForData(string assignedObjName)
    {
        return speedManager.CallForData(assignedObjName);
    }

    public void CallForTransition()
    {

    }


    public void setScore(int newScore)
    {
        score += newScore;
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = highScore.ToString();
        }
        scoreText.text = score.ToString();
    }

    public void Lose()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        StartCoroutine(uiManager.Lose());

        PlayerPrefs.SetInt("HighScore", highScore);
    }

    IEnumerator DeployDelay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }
    public void StopBackground()
    {
        bgOffset = Vector2.zero;
    }

}
