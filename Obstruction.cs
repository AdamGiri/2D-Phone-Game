using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract class for all obstruction environments
public abstract class Obstruction : MonoBehaviour {

    public float zMax = 0.5f;
    public float zMin = -0.5f;
    public float xMax = 0.4f;
    public float xMin = -0.4f;
    public float x;
    public float z;
    public float xScaleModifier = 0;
    public float yScaleModifier = 0;

    public int maxObstructionCount;

    public Vector3 obstructionPos;
    public  Vector3 obstructionScale;

    public bool safeToAddObstruction;

    public Rect spawnRect;
    public List<Rect> spawnRects;
    public List<Vector3> spawnRectsPositions;
    public GameObject obstruction;
    public  float surfaceY = -0.439f;
    public string objTag = "Obstruction";

    public Obstruction()
    {
        spawnRects = new List<Rect>();
        spawnRectsPositions = new List<Vector3>();
    }

    public void GenerateRandomCoords()
    {
        x = Random.Range(xMin, xMax);
        z = Random.Range(zMin, zMax);
        
        spawnRect = new Rect();
        spawnRect.xMax = x + obstructionScale.x/2 + xScaleModifier;
        spawnRect.xMin = x - obstructionScale.x/2 - xScaleModifier;
        spawnRect.yMax = z + obstructionScale.z / 2 + yScaleModifier; 
        spawnRect.yMin = z - obstructionScale.z/2 - yScaleModifier;
       
    }

    
    public  virtual void CheckObstructionOverlap()
    {
     
           
            safeToAddObstruction = false;
            while (!safeToAddObstruction)
            {
                GenerateRandomCoords();

                if (spawnRects.Count > 0)
                {
                    foreach (Rect existingRect in spawnRects)
                    {
                        if (spawnRect.xMin > existingRect.xMax || spawnRect.xMax < existingRect.xMin
                            && spawnRect.yMin > existingRect.yMax || spawnRect.yMax < existingRect.yMin)
                        {
                            safeToAddObstruction = true;
                            
                       
                        } else
                        {
                        safeToAddObstruction = false;
                        }

                        if (!safeToAddObstruction)
                        {
                           
                            break;
                        }

                    }
                    
                } else
                {
                    safeToAddObstruction = true;
                }
            }


            spawnRects.Add(spawnRect);
            GetRectCenterPoint();
        
       

    }

    public void GetRectCenterPoint()
    {
        float xCenter = spawnRect.xMin + ((spawnRect.xMax - spawnRect.xMin) / 2);
        float zCenter = spawnRect.yMin + ((spawnRect.yMax - spawnRect.yMin) / 2);
        spawnRectsPositions.Add(new Vector3(xCenter, surfaceY, zCenter));
    }

   public  void SpawnObstruction(GameObject definedObstruction, Transform parentTransform,string tag)
    {
        foreach (Vector3 spawnPos in spawnRectsPositions)
        {
            
            definedObstruction.transform.SetParent(parentTransform);
            definedObstruction.transform.localPosition = spawnPos;
            definedObstruction.gameObject.tag = tag;
          
        }
    }

    public virtual void CreateObstructions(Transform parentTransform,GameObject obstructionObject)
    {
        for (int i = 0; i < maxObstructionCount; i++)
        {
            obstruction = Instantiate(obstructionObject);

            CheckObstructionOverlap();
            if (obstruction != null)
            {
                SpawnObstruction(obstruction, parentTransform, objTag);
                UnitController.instance.childObstructions.Add(obstruction);
            }
            else
            {
                Debug.Log("ob is null"); 
            }
        }
    }
    
}
