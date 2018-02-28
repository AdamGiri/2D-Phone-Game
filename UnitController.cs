using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {

    public static UnitController instance = null;
    public Transform subUnit;
   
    Vector3 startingPosition = new Vector3(0.03428571f, 3.2f, -110);
    int levelNo;
    double subUnitStartingPos;
    GameController gcInstance = null;

    public GameObject barrierObstruction;
    public GameObject staticObstruction;
    public GameObject dynamicObstruction;
    public GameObject laserTowerObstruction;
    public GameObject lavaObstruction;
    public GameObject bouncePad;
    public List<GameObject> childObstructions;
    public int subUnitSpace;

    public enum SubUnitTypes{
        STATIC,
        BARRIER,
        DYNAMIC,
        LASERTOWER,
        LAVA,
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        childObstructions = new List<GameObject>();
    }


    public  void Initiate()
    {
        gcInstance = GameController.instance;
        levelNo = gcInstance.level + 5; 
       
        subUnitStartingPos = gcInstance.startingPosition.z - ((gcInstance.groundInstance.transform.localScale.z
            * 0.5)*0.9);

        startingPosition.z = (float) subUnitStartingPos;
       
        GenerateSubUnits();
        AssignUnitType();
    }

    void GenerateSubUnits()
    {
      
        if (gcInstance.subUnits.Count != 0)
        {

            for (int i = 0; i < gcInstance.subUnits.Count; i++)
            {

                startingPosition.z += 40f + subUnitSpace;
                gcInstance.subUnits[i].SetPositionAndRotation(startingPosition, Quaternion.identity);
                
            }
            
            AddUnit();
            
        } else
        {
            for (int i = 0; i < levelNo; i++)
            {
                AddUnit();
            }
        }
        
    }

    void AssignUnitType()
    {
        for (int i = 0; i < gcInstance.subUnits.Count; i++)
        {
            //below is to correct the mysterious transferring of last obstruction environment..
            if (i == gcInstance.subUnits.Count - 1)
            {
                int unitChildCount = gcInstance.subUnits[i].childCount;
                if (unitChildCount > 0)
                {
                    for (int j = 0; j < unitChildCount; j++)
                    {
                        Transform child = gcInstance.subUnits[i].GetChild(j);
                        Destroy(child.gameObject);
                    }
                }
            }

            SubUnitController subUnitController = gcInstance.subUnits[i].
                gameObject.GetComponent<SubUnitController>();
            SubUnitTypes type = (SubUnitTypes)Random.Range(0, System.Enum.GetValues(typeof(SubUnitTypes)).Length);
            subUnitController.setUnitType(type);
            if (i == 0)
            {
                subUnitController.SetSpeedTrigger(true);
            }
            
        
        }
    }

   

    
	void AddUnit()
    {
        startingPosition.z += 40f + subUnitSpace;
        subUnit = Instantiate(subUnit);
        subUnit.SetPositionAndRotation(startingPosition, Quaternion.identity);
        gcInstance.subUnits.Add(subUnit);
        

    }

  public void ClearSubUnits()
    {
        Debug.Log("childob size before: " + childObstructions.Count);
        int i = 0;
        foreach(GameObject obstruction in childObstructions)
        {
            Destroy(obstruction);
            Debug.Log("destroying:  " + i++);
        }
        
        childObstructions.Clear();
    }
	
}
