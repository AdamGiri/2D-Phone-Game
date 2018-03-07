using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour {

	public void QuitGame()
    {
        if (PlayerPrefs.GetInt("Level") < GameController.instance.level)
        {
            PlayerPrefs.SetInt("Level", GameController.instance.level);
        }
       
        if (PlayerPrefs.GetInt("Score") < GameController.instance.score)
        {
            PlayerPrefs.SetInt("Score", GameController.instance.score);
        }
        
        SceneManager.LoadScene(0);
    }
}
