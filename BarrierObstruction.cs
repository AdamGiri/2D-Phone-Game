using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierObstruction : Obstruction
{


    public BarrierObstruction() : base()
    {
        maxObstructionCount = 8;
        xMax = 0.1f;
        obstructionScale = new Vector3(0.15f, 0.2f, 0.035f);

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
