using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static System.TimeZoneInfo;

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

    float lerpSpeed = 1f;
    Material BGMaterial;
    Color goalColor;
    [SerializeField] float transitionTime = 2f;

    [SerializeField] int howManyTimesIn1Sec;
    [SerializeField] int tintCounter = 0;

    Color[] colors;

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
        bgMaterial = BG.GetComponent<Renderer>().material;
        bgOffset = new Vector2(0f, backgroundSpeed);
        goalColor = Random.ColorHSV(

            Random.Range(0, 51),
            Random.Range(51,100), 

            Random.Range(20, 31),
            Random.Range(31,81),

            Random.Range(20,31), 
            Random.Range(31, 81)
        );

        spawnManager = gameObject.GetComponentInChildren<SpawnManager>();
        speedManager = gameObject.GetComponentInChildren<SpeedManager>();
        uiManager = gameObject.GetComponentInChildren<UIManager>();
        vFXManager = gameObject.GetComponentInChildren<VFXManager>();
        audioManager = gameObject.GetComponentInChildren<AudioManager>();

        highScoreText = UIText.GetComponentsInChildren<Transform>()[1].GetComponent<Text>();
        scoreText = UIText.GetComponentsInChildren<Transform>()[2].GetComponent<Text>();
        //errorText = UIText.GetComponentsInChildren<Transform>()[3].GetComponent<Text>();

        minX = borders[0].GetComponent<Border>().transform.position.x;
        maxX = borders[1].GetComponent<Border>().transform.position.x;
        minY = borders[2].GetComponent<Border>().transform.position.y;
        maxY = borders[3].GetComponent<Border>().transform.position.y;

        //spawnManager.CallManagerForSpeedData(verticalSpeed);



        //sides.GetComponentsInChildren<SideCollider>()[0].setRelatives(player, gameObject);
        //sides.GetComponentsInChildren<SideCollider>()[1].setRelatives(player, gameObject);

        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = highScore.ToString();
        score = 0;

    }

    //private void OnEnable()
    //{
    //    StartCoroutine(ChangeMaterialColor());
    //}

    private void FixedUpdate()
    {
        if (tintCounter >= 1200)
        {
            StartCoroutine(ChangeMaterialColor()); 
            tintCounter = 0;
            goalColor = Random.ColorHSV(
                0 , 1,
                .5f, 1.95f,
                0.9f,1f
            );

            Debug.Log("color: " + goalColor.ToString());
        }
        else
        {
            tintCounter++;
        }
        //BG.GetComponent<Transform>().Rotate(new Vector3(backgroundSpeed * Time.deltaTime, 0, 0));
        bgMaterial.mainTextureOffset += bgOffset * Time.deltaTime;
    }

    IEnumerator ChangeMaterialColor()
    {
        float duration = 0.0f;
        while (duration < transitionTime)
        {
            float progress = duration / transitionTime;
            bgMaterial.color = Color.Lerp(bgMaterial.color, goalColor, progress);
            duration += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
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
