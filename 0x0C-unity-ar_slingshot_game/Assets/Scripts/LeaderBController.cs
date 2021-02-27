using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderBController : MonoBehaviour
{
    public List<GameObject> leaderBoardItems;
    public List<int> scores;
    // Start is called before the first frame update
    public void updateScores(int newScore)
    {
        scores = new List<int>{};
        int i = 0;
        foreach (GameObject item in leaderBoardItems)
            scores.Add(int.Parse(PlayerPrefs.GetString(item.name, "0")));
        scores.Add(newScore);
        scores.Sort();
        scores.Reverse();
        foreach (GameObject item in leaderBoardItems)
        {
            PlayerPrefs.SetString(item.name, scores[i].ToString());
            item.GetComponent<TMP_Text>().text = i+1 + ". " + scores[i].ToString();
            i++;
        }
    }
}
