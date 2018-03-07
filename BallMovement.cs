using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

    public float speed;
    public float bounceSpeedFactor;
    public int playerInputSpeedX;
    public int playerInputSpeedY;
    public float acceleration;
    public static BallMovement instance = null;
     float bounceForce;

    Rigidbody rigidBody;
    Vector3 initialVelocity;
    int minBounceSpeed = 5;
    bool hasHitObstruction;
    float bounceSpeed;
    float currentSpeed;
    Vector3 totalPlayerVec;

    // Use this for initialization
    void Awake () {
       
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        rigidBody = GetComponent<Rigidbody>();
        bounceSpeed = bounceSpeedFactor * speed;
    }

    // Update is called once per frame
    void FixedUpdate () {

        Move();
        initialVelocity = rigidBody.velocity;
    }

    void Move() {
        if (!hasHitObstruction)
        {
           

            if (currentSpeed < speed)
            {
                currentSpeed += acceleration * Time.deltaTime;
            } else
            {
                currentSpeed = speed;
            }
          
            Vector3 playerInputVec = new Vector3(-Input.acceleration.x * playerInputSpeedX, 0,
               -Input.acceleration.y * playerInputSpeedY) ;
            totalPlayerVec = (transform.forward + playerInputVec) * currentSpeed;
            rigidBody.MovePosition(transform.position + totalPlayerVec*Time.deltaTime);
          
           
        }
    }

  

    private void OnCollisionEnter(Collision collision)
    {
        //TODO change to the ball smashing or something eventually
        if (collision.gameObject.CompareTag("Obstruction"))
        {
           
            GameOver();
        }
    }

  

    void Bounce(Collision collision)
    {
     
        //rigidBody.AddForce(Vector3.up * bounceForce);

        /*  float collisionSpeed = initialVelocity.magnitude;
          Vector3 reflectDir = Vector3.Reflect(initialVelocity.normalized, collision.contacts[0].normal);
          rigidBody.velocity = reflectDir * Mathf.Max(collisionSpeed, minBounceSpeed);*/
    }

    public void GameOver()
    {
        Debug.Log("gameover");
        rigidBody.velocity = Vector3.zero;
        hasHitObstruction = true;
        GameController.instance.enableTotalUI(true);
    }
}
