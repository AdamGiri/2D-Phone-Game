using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;
    public float smoothingFactor;

    Vector3 offset;
    


    // Use this for initialization
    void Start () {
        offset = transform.position - player.transform.position;


    }
	
	// Update is called once per frame
	void FixedUpdate () {

        
        Vector3 targetPos = player.transform.position + offset;
        
      
        Vector3 correctedPos = new Vector3(0, targetPos.y, targetPos.z);
        
    
        transform.position = Vector3.Lerp(transform.position, correctedPos, Time.deltaTime * smoothingFactor);
        
    }
}
