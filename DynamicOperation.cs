using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicOperation : MonoBehaviour {

    Rigidbody rigidBody;
    Vector3 moveVec;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        moveVec = new Vector3(1, 0, 0);

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Move();
	}

    void Move()
    {
        rigidBody.MovePosition(transform.position + moveVec * Time.deltaTime);
        
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            
            moveVec.x *= -1;
        }
    }
}
