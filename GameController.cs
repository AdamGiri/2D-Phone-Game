using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

    public static GameController instance = null;
    public  int level;
    public int groundLengthIncrease;
    public GameObject newGround;
    public GameObject transitionZone;
    public GameObject scoreUI;
    public  Vector3 startingPosition = Vector3.zero;
    public int groundWidth;
    public int groundHeight;
    public  List<Transform> subUnits;
    public int transitionLength = 34;
    public GameObject groundInstance;
    public int score = -10;
    public bool levelCompleted = false;
    public int levelDuration;

    int initialGroundLength = 260;
    bool firstLevel;
   
    GameObject transitionInstance;
    Vector3 transitionVec;
    
    int previousGroundLength;
    
    Text levelText;
    Text scoreText;
    Text countDownText;
   
    CanvasGroup canvasGroupScore;
    CanvasGroup canvasGroupGameOver;

    

    private void Awake()
    {

        firstLevel = true;
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        subUnits = new List<Transform>();
        levelText = (Text)GameObject.Find("LevelText").gameObject.GetComponent<Text>();
        scoreText = (Text)GameObject.Find("ScoreText").gameObject.GetComponent<Text>();
        countDownText = (Text)GameObject.Find("CountDownText").gameObject.GetComponent<Text>();

        canvasGroupScore = GameObject.Find("ScorePanel").GetComponent<CanvasGroup>();
        canvasGroupGameOver = GameObject.Find("GameOverPanel").GetComponent<CanvasGroup>();
        

        AddLevel();
    }

    public  void AddLevel()
    {
        levelCompleted = false;
        levelDuration = (level + 6) * 7;
        int totalGroundIncrease = (groundLengthIncrease + UnitController.instance.subUnitSpace) * level;
       
        int newGroundLength = initialGroundLength + totalGroundIncrease;
        
        
       
        if (!firstLevel)
        {
            startingPosition.z += previousGroundLength + transitionLength+20;
            groundInstance.transform.SetPositionAndRotation(startingPosition, Quaternion.identity);
            
            transitionVec.z += newGroundLength+transitionLength - UnitController.instance.subUnitSpace/2;
           

        } else
        {
            
            groundInstance = Instantiate(newGround, startingPosition, Quaternion.identity);
            
            transitionVec = new Vector3(startingPosition.x, startingPosition.y, startingPosition.z
            + (newGroundLength / 2) + transitionLength/2 );
            transitionInstance = Instantiate(transitionZone, transitionVec, Quaternion.identity);
        }

    
       
        groundInstance.transform.localScale = new Vector3(groundWidth, groundHeight,newGroundLength);
        UnitController.instance.Initiate();
        previousGroundLength = newGroundLength;
        level++;
        score += 10;
        ShowScoreUI();

        if (firstLevel)
        {
            firstLevel = false;
            StartCoroutine("LevelTimer");
        }
       
        

    }

    public void MoveTransition()
    {
        transitionInstance.transform.SetPositionAndRotation(transitionVec, Quaternion.identity);
    }

    void ShowScoreUI()
    {
        levelText.text = "Level: " + level.ToString();
        scoreText.text = "Score: " + score.ToString();
        Debug.Log("scoretext" + scoreText.text.ToString());
        enableScoreUI(true);
        StartCoroutine("HideUI");
    }

    IEnumerator HideUI()

    {
        yield return new WaitForSeconds(3f);
        enableScoreUI(false);
    }

    public void enableTotalUI(bool enableTotalUI)
    {
        enableScoreUI(enableTotalUI);
        enableGameOverUI(enableTotalUI);
    }

    public void enableScoreUI(bool enableScoreUI)
    {
        if (enableScoreUI)
        {
            canvasGroupScore.alpha = 1;
        } else
        {
            canvasGroupScore.alpha = 0;
        }

    }

     void enableGameOverUI(bool enableGameOverUI)
    {
        if (enableGameOverUI)
        {
            canvasGroupGameOver.alpha = 1;
        } else
        {
            canvasGroupGameOver.alpha = 0;
        }
    }

    public IEnumerator LevelTimer()
    {
        
       
        while (levelDuration > 0)
        {
            yield return new WaitForSeconds(1);
            levelDuration--;
            countDownText.text = "Time left: " + levelDuration.ToString();
        }
        

        if (!levelCompleted)
        {
            Debug.Log("level not complete");
            BallMovement.instance.GameOver();
        }
    }

}
