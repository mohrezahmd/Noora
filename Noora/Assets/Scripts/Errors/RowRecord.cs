using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RowRecord : MonoBehaviour
{
    public void set(string username, string rank, string score)
    {
        transform.Find("Usernames").GetComponent<TMP_Text>().text = username;
        transform.Find("Rank").GetComponent<Text>().text = rank;
        transform.Find("HighScore").GetComponent<Text>().text = score;
    }
}
