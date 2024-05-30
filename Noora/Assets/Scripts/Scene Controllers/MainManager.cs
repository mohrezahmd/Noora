using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject ally;

    [SerializeField] private GameObject shrinker;

    [SerializeField] private GameObject sides;

    SpawnManager spawnManager;
    SpeedManager speedManager;
    DebugManager debugManager;

    [SerializeField] GameObject UIText;
    [SerializeField] GameObject UIGameplay;

    [SerializeField] GameObject[] borders;  // minXObj, maxXObj, minYObj, maxYObj
    private float minX, maxX, minY, maxY;

    private Text scoreText, highScoreText;
    private int highScore = 0;
    private int score = 0;

    [SerializeField] GameObject BG;
    [SerializeField] float backgroundSpeed = .5f;
    Material bgMaterial;
    Vector2 bgOffset;

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
        debugManager = gameObject.GetComponent<DebugManager>();

        highScoreText = UIText.GetComponentsInChildren<Transform>()[1].GetComponent<Text>();
        scoreText = UIText.GetComponentsInChildren<Transform>()[2].GetComponent<Text>();

        minX = borders[0].GetComponent<Border>().transform.position.x;
        maxX = borders[1].GetComponent<Border>().transform.position.x;
        minY = borders[2].GetComponent<Border>().transform.position.y;
        maxY = borders[3].GetComponent<Border>().transform.position.y;

        bgMaterial = BG.GetComponent<Renderer>().material;
        bgOffset = new Vector2(0f, backgroundSpeed);

        sides.GetComponentsInChildren<SideCollider>()[0].setRelatives(player, gameObject);
        sides.GetComponentsInChildren<SideCollider>()[1].setRelatives(player, gameObject);

        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = highScore.ToString();
        score = 0;

    }

    private void Update()
    {
        bgMaterial.mainTextureOffset += bgOffset * Time.deltaTime;
    }

    public void CallToSpawner(GameObject spawnedObject)
    {
        spawnManager.PushToDeactivate(spawnedObject);
    }

    public float CallToAssignerForData(string assignedObjName)
    {
        float tmpSpd = speedManager.CallForData(assignedObjName);
        return tmpSpd;
    }

    public void CallToDebugger(GameObject gameObject)
    {
        debugManager.createDebugPanelForEntity(gameObject);
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
        StartCoroutine(UIGameplay.GetComponent<SceneTransition>().Lose());
        //StartCoroutine(DeployDelay());

        PlayerPrefs.SetInt("HighScore", highScore);
    }

    public void StopBackground()
    {
        bgOffset = Vector2.zero;
    }

}
