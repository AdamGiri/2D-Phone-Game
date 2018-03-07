using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaObstruction : Obstruction {



    GameObject lavaObstruction;
    GameObject tile;

    Vector3 bouncePadScale = new Vector3(0.25f, 0.2f, 0.05f);
    Vector3 incrementDistance = new Vector3(2.5f, 0, 0.5f);
    Vector3 currentTilePos;
    Vector3 westEndLocation;
    Vector3 eastEndLocation;

    bool alterPathDirection;
    bool pathAltered;
    int tileNo;

    List<Vector3> existingTilePositions;


    public LavaObstruction() : base()
    {
       
    }

    //The following code randomly generates a path over the lava field.
    public enum PathDirection
    {
        LEFT,
        DOWN,
        RIGHT,

    }

    public bool MoveDown()
    {
        Vector3 currentTilePosTmp = currentTilePos;
        currentTilePosTmp.z += incrementDistance.z;

        if (!existingTilePositions.Contains(currentTilePosTmp))
        {
            currentTilePos.z = currentTilePosTmp.z;
            return true;
        }
        return false;
    }

    public bool MoveLeft()
    {
        Vector3 currentTilePosTmp = currentTilePos;
        currentTilePosTmp.x += incrementDistance.x;
        
        if (!existingTilePositions.Contains(currentTilePosTmp))
        {
            currentTilePos.x = currentTilePosTmp.x;
            return true;
        }
        return false;
    }

    public bool MoveRight()
    {
        Vector3 currentTilePosTmp = currentTilePos;
        currentTilePosTmp.x += -incrementDistance.x;
       
        if (!existingTilePositions.Contains(currentTilePosTmp))
        {
            currentTilePos.x = currentTilePosTmp.x;
            return true;
        } 
        return false;
    }

   

    public override void CreateObstructions(Transform parentTransform, GameObject obstructionObject)
       {
           lavaObstruction = Instantiate(obstructionObject);
           lavaObstruction.transform.parent = parentTransform;
           lavaObstruction.transform.localPosition = new Vector3(0, -0.53f, 0);
           lavaObstruction.transform.localScale = new Vector3(0.11f, 0.2f, 0.1f);
           UnitController.instance.childObstructions.Add(lavaObstruction);
           CreatePath();
       }

    public void CreatePath()
    {
        existingTilePositions = new List<Vector3>();
        Vector3 startLocation = lavaObstruction.transform.GetChild(0).localPosition;
        
        float endLocationZ = 5.2f;
        westEndLocation = lavaObstruction.transform.GetChild(2).localPosition;
        eastEndLocation = lavaObstruction.transform.GetChild(3).localPosition;
        Vector3 startingTilePos = new Vector3(startLocation.x, 0.05f, startLocation.z);
        currentTilePos = startingTilePos;
    
        while (currentTilePos.z < endLocationZ)
        {
           
            tile = Instantiate(UnitController.instance.bouncePad, lavaObstruction.transform);
          
            tile.transform.localScale = bouncePadScale;
            tile.transform.localPosition = currentTilePos;
            IncrementPath();
        }
    }

    public void IncrementPath()
    {
        PathDirection direction = PathDirection.DOWN;
        bool pathRight = false;

        if (!alterPathDirection)
        {
         
            if (currentTilePos.x > westEndLocation.x &&
                           currentTilePos.x < eastEndLocation.x )
            {
             
                direction = (PathDirection)RandomDirectionGenerator(pathRight);
                
            }
            else 
            {
             
                if (!pathAltered)
                {
                  
                    direction = PathDirection.DOWN;
                    alterPathDirection = true;
                } else
                {
                    if (currentTilePos.x <= westEndLocation.x)
                    {
                      
                        pathRight = false;

                    }
                    else
                    {
                        
                        pathRight = true;
                    }

                    direction = (PathDirection)RandomDirectionGenerator(pathRight);
                }
                
            }
           
        } else
        {
           
            if (currentTilePos.x <= westEndLocation.x )
            {
               
                pathRight = false;

            }  else
            {
                
                pathRight = true;
            }
            
            direction = (PathDirection)RandomDirectionGenerator(pathRight);
        }

      

        if (!alterPathDirection)
        {
           
            while (true)
            {

                if (direction == PathDirection.LEFT)
                {
                    if (MoveLeft())
                    {
                        
                        if (pathAltered)
                        {
                            pathAltered = false;
                        }
                        break;
                    }
                    direction = PathDirection.DOWN;
                }

                if (direction == PathDirection.DOWN)
                {
                    if (MoveDown())
                    {
                      
                        break;
                    }
                    direction = PathDirection.RIGHT;
                }

                if (direction == PathDirection.RIGHT)
                {
                    if (MoveRight())
                    {
                        
                        if (pathAltered)
                        {
                            pathAltered = false;
                        }
                        break;
                    }
                }
                break;
            }
        } else
        {
            switch (direction)
            {
                case PathDirection.LEFT:
                    MoveLeft();
                    break;
                case PathDirection.DOWN:
                    MoveDown();
                  
                    break;
                case PathDirection.RIGHT:
                    MoveRight();
                    break;
            }
            pathAltered = true;
        }
      

        existingTilePositions.Add(currentTilePos);
       
    }

    public int RandomDirectionGenerator(bool pathRight)
    {
        int randomDirection;

        if (!alterPathDirection)
        {
            if (!pathAltered)
            {
                randomDirection = Random.Range(0, System.Enum.GetValues(typeof(PathDirection)).Length);
            } else
            {
                if (!pathRight)
                {
                    randomDirection = Random.Range(0, 2);
                }
                else
                {
                   
                    randomDirection = Random.Range(1, 3);

                }
            }
            
        } else
        {
            if (!pathRight)
            {
                randomDirection = Random.Range(0, 2);
            }
            else
            {
                
                randomDirection = Random.Range(1, 3);
            
            }
            alterPathDirection = false;
        }

     
        return randomDirection;
    }


    
}
