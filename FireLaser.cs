using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaser : MonoBehaviour {

    public int fireDelay = 2;
    public GameObject laser;
    public float projectileSpeed;
    public Vector3 correctedRotation;

    float timeElapsed;
    GameObject firedLaser;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= fireDelay)
        {
            Fire();
        }
        
	}

    void Fire()
    {
        
        timeElapsed = 0;
        firedLaser = Instantiate(laser,transform);
        firedLaser.GetComponent<Rigidbody>().AddForce(firedLaser.transform.forward * projectileSpeed) ;
        Destroy(firedLaser,3);
    }

   



}
