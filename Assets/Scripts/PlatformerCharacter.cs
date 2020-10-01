using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerCharacter : MonoBehaviour
{
    public bool isGrounded; 
    public float moveSpeed = 15f;
    public float jumpPower = 20f;
    public Transform cam;
    public float camOffset = .5f;
    Rigidbody2D myRigidBody;
    Transform myTransform;
    float inputHorizontal;
    bool isJumping = false;

    void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
    }

    void Update() { 
        inputHorizontal = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded)  {
            isJumping = true;
        }
        moveCamera();
    }

    void FixedUpdate() { 
        myRigidBody.velocity = new Vector2 (
            inputHorizontal * moveSpeed, myRigidBody.velocity.y); 

        if (isJumping) {
            myRigidBody.velocity = new Vector2 
                (myRigidBody.velocity.x, jumpPower);
            isJumping = false;
        }
    }

    void moveCamera() {
         cam.position = new Vector3 (0f, myTransform.position.y + camOffset, -10f);
         if (cam.position.y < .5) {
            cam.position = new Vector3 (0f, camOffset, -10f);
         }
    }
}