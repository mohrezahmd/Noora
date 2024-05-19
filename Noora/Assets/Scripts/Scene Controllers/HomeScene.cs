using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScene : MonoBehaviour
{
    public Text  welcome, selectUsernameText, highScoreText;
    public Text errorText;
    public Text messageText;
    string[] errors;
    int highScore = 0;
    [SerializeField] 

    public Button startButton;
    public TotalData totalData;

    [SerializeField] GameObject BG;
    [SerializeField] float backgroundSpeed = .5f;
    [SerializeField] float rotationBG;
    Material bgMaterial;
    Vector2 bgOffset;

    private void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
        highScoreText.text = highScore.ToString();

        bgMaterial = BG.GetComponent<Renderer>().material;
        bgOffset = new Vector2(0f, backgroundSpeed);
    }

    private void FixedUpdate()
    {
        bgMaterial.mainTextureOffset += bgOffset * Time.deltaTime;

        BG.transform.Rotate( 0 , 0, Random.Range(-rotationBG, rotationBG) * Time.deltaTime);
    }
 
    public void startGameButton()
    {
        StartCoroutine(DelaySeconds());
    }

    IEnumerator DelaySeconds()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }
}
