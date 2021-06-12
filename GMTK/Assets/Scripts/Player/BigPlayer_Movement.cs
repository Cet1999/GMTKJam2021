using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BigPlayer_Movement : MonoBehaviour {
    Rigidbody rb;

    Vector3 rawInputMovement;
    bool jumpPressed;
    bool isGrounded;

    [SerializeField] float maxVelocityWhileGrounded = 5f;
    [SerializeField] float maxVelocityWhileFalling = 3f;
    [SerializeField] float moveForce = 10000f;
    [SerializeField] float jumpForce = 50000f;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() { }

    private void FixedUpdate() {
        // set isGrounded
        float distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
        isGrounded = Physics.Raycast(rb.position, -Vector3.up, distToGround + 0.1f);

        // calculate force applied by input
        Vector3 moveDir = rawInputMovement.normalized;
        Vector3 force = moveDir * moveForce;

        if (!isGrounded) {
            force *= 0.5f;
        }
        
        rb.AddForce(force);

        // clamp velocity
        Vector3 vel = rb.velocity;
	    vel.y = 0;

        if (isGrounded) {
	        vel = Vector3.ClampMagnitude(vel, maxVelocityWhileGrounded);
        } else {
	        vel = Vector3.ClampMagnitude(vel, maxVelocityWhileFalling);
        }

	    vel.y = rb.velocity.y;
	    rb.velocity = vel;

        // attempt to jump
        if (jumpPressed && isGrounded) {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    public void onMovement(InputAction.CallbackContext value) { 
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0f, inputMovement.y);
    } 

    public void onJump(InputAction.CallbackContext value) {
        if (value.started) jumpPressed = true;
        if (value.canceled) jumpPressed = false;
    } 
}
