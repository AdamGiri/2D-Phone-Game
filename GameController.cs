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
   

    int initialGroundLength = 260;
    bool firstLevel;
   
    GameObject transitionInstance;
    Vector3 transitionVec;
    int previousGroundLength;
    int score;
    Text levelText;
    Text scoreText;

    CanvasGroup canvasGroup;

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
        scoreUI = Instantiate(scoreUI);
        levelText = (Text)GameObject.Find("LevelText").gameObject.GetComponent<Text>();
        scoreText = (Text)GameObject.Find("ScoreText").gameObject.GetComponent<Text>();
        canvasGroup = scoreUI.GetComponent<CanvasGroup>();
        AddLevel();
    }

    public  void AddLevel()
    {
       
        int totalGroundIncrease = (groundLengthIncrease + UnitController.instance.subUnitSpace) * level;
       
        int newGroundLength = initialGroundLength + totalGroundIncrease;
        
        
       
        if (!firstLevel)
        {
            startingPosition.z += previousGroundLength + transitionLength+20;
            groundInstance.transform.SetPositionAndRotation(startingPosition, Quaternion.identity);
            
            transitionVec.z += newGroundLength+transitionLength - UnitController.instance.subUnitSpace/2;
           

        } else
        {
            firstLevel = false;
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
    }

    public void MoveTransition()
    {
      
        transitionInstance.transform.SetPositionAndRotation(transitionVec, Quaternion.identity);
    }

    void ShowScoreUI()
    {
        levelText.text = "Level: " + level.ToString();
        scoreText.text = "Score: " + score.ToString();
        StartCoroutine("HideUI");
    }

    IEnumerator HideUI()

    {
        yield return new WaitForSeconds(3f);
        canvasGroup.alpha = 0;
    }

}
