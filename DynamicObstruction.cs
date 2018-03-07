using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObstruction : Obstruction
{

    public DynamicObstruction() : base()
    {
        maxObstructionCount =7;
        obstructionScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    public override void CheckObstructionOverlap()
    {
       
        safeToAddObstruction = false;
        while (!safeToAddObstruction)
        {
            GenerateRandomCoords();

            if (spawnRects.Count > 0)
            {
                foreach (Rect existingRect in spawnRects)
                {
                    if (spawnRect.yMin > existingRect.yMax || spawnRect.yMax < existingRect.yMin)
                    {
                        safeToAddObstruction = true;

                     
                    }
                    else
                    {
                        safeToAddObstruction = false;
                    }

                    if (!safeToAddObstruction)
                    {

                        break;
                    }

                }

            }
            else
            {
                safeToAddObstruction = true;
            }
        }


        spawnRects.Add(spawnRect);
        GetRectCenterPoint();
    }
}



