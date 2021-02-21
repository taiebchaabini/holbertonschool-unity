using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static TMP_Text scoreText;
    public static GameObject list;

    public void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
        list = GameObject.Find("List");
    }
}
