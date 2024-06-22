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

    public int allyID = 0;

    [SerializeField] private GameObject shrinker;
    [SerializeField] private GameObject sides;

    SpawnManager spawnManager;
    SpeedManager speedManager;
    UIManager uiManager;
    VFXManager vFXManager;
    StateManager stateManager;
    AudioManager audioManager;


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
        spawnManager = gameObject.GetComponentInChildren<SpawnManager>();
        speedManager = gameObject.GetComponentInChildren<SpeedManager>();
        uiManager    = gameObject.GetComponentInChildren<UIManager>();
        vFXManager   = gameObject.GetComponentInChildren<VFXManager>();
        audioManager = gameObject.GetComponentInChildren<AudioManager>(); 

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

        //sides.GetComponentsInChildren<SideCollider>()[0].setRelatives(player, gameObject);
        //sides.GetComponentsInChildren<SideCollider>()[1].setRelatives(player, gameObject);

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

    public float CallForData(string assignedObjName)
    {
        return speedManager.CallForData(assignedObjName);
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
        //StartCoroutine(uiManager.Lose());
        PlayerPrefs.SetInt("HighScore", highScore);
        StartCoroutine(uiManager.Lose());

    }

    public void StopBackground()
    {
        bgOffset = Vector2.zero;
    }

}
