using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDetection : MonoBehaviour {

    

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("initial objects: " + UnitController.instance.childObstructions.Count);
            UnitController.instance.ClearSubUnits();
            GameController.instance.levelCompleted = true;
        
            GameController.instance.AddLevel();
          
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
            GameController.instance.MoveTransition();

        }
    }
}
