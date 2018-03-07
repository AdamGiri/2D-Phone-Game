using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    List<int> levelList;
    List<int> scoreList;

    public Text hiScoreDisplay;
    public Text hiLevelDisplay;

    private void Awake()
    {
       hiScoreDisplay.text = "Hi-Score: " + PlayerPrefs.GetInt("Score",0).ToString();
       hiLevelDisplay.text = "Hi-Level: " + PlayerPrefs.GetInt("Level", 0).ToString();
    }
}
