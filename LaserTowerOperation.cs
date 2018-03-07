using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTowerOperation : MonoBehaviour {

    Vector3 rotationVec;
    public int rotationSpeed;

	// Use this for initialization
	void Start () {
        rotationVec = new Vector3(0, 1, 0);

    }
	
	// Update is called once per frame
	void Update () {
        Rotate();
	}

    void Rotate()
    {

        transform.Rotate(rotationVec * rotationSpeed * Time.deltaTime);


    }
}
