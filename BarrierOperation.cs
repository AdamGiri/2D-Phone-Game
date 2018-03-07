using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierOperation : MonoBehaviour {

    public int operationSpeed;
    Vector3 barrierRotation;

    private void Start()
    {
        barrierRotation = new Vector3(0, 0,- 1);
    }

    private void FixedUpdate()
    {
        transform.Rotate(barrierRotation * Time.deltaTime * operationSpeed);
       
        if (transform.eulerAngles.z < 270)
        {
            
            barrierRotation.z *= -1;
        }

     
    }
}
