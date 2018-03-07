using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubUnitController : MonoBehaviour {

    public List<GameObject> obstructions;


    private void Awake()
    {
        obstructions = new List<GameObject>();
    }

    public void setUnitType(UnitController.SubUnitTypes type ){


        

       switch (type)
          {

             case UnitController.SubUnitTypes.STATIC:

             StaticObstruction staticComponent = gameObject.GetComponent<StaticObstruction>();

              if (staticComponent != null)
             {
                 staticComponent = null;
             }
             StaticObstruction staticObstruction = gameObject.AddComponent<StaticObstruction>();
             staticObstruction.CreateObstructions(transform, UnitController.instance.staticObstruction);
             break;

            case UnitController.SubUnitTypes.BARRIER:

             BarrierObstruction barrierComponent = gameObject.GetComponent<BarrierObstruction>();

             if (barrierComponent != null)
               {
                 barrierComponent = null;
               }

              BarrierObstruction barrierObstruction = gameObject.AddComponent<BarrierObstruction>();
              barrierObstruction.CreateObstructions(transform,UnitController.instance.barrierObstruction);
              break;
                      
              case UnitController.SubUnitTypes.DYNAMIC:

                   DynamicObstruction dynamicComponent = gameObject.GetComponent<DynamicObstruction>();

                if (dynamicComponent != null)
                {
                    dynamicComponent = null;
                }

                 DynamicObstruction dynamicObstruction = gameObject.AddComponent<DynamicObstruction>();
                 dynamicObstruction.CreateObstructions(transform, UnitController.instance.dynamicObstruction);
                 break; 

            case UnitController.SubUnitTypes.LASERTOWER:

          LaserTowerObstruction laserComponent = gameObject.GetComponent<LaserTowerObstruction>();

          if (laserComponent != null)
          {
              laserComponent = null;
          }

           LaserTowerObstruction ltObstruction = gameObject.AddComponent<LaserTowerObstruction>();
           ltObstruction.CreateObstructions(transform, UnitController.instance.laserTowerObstruction);
           break;
            
            case UnitController.SubUnitTypes.LAVA:

            LavaObstruction lavaObstruction = gameObject.AddComponent<LavaObstruction>();
            lavaObstruction.CreateObstructions(transform, UnitController.instance.lavaObstruction);
            GameController.instance.levelDuration += 6;      
            break;

                   

        }
    }

    public void SetSpeedTrigger(bool b)
    {
        if (b)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("changing speed");
         //   BallMovement.instance.speed = 10;
        }
    }
}
