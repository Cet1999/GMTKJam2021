using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BigPlayer_Movement : MonoBehaviour {
    Rigidbody rb;

    Vector3 rawInputMovement;
    bool jumpPressed;
    bool isGrounded;
    public bool BigPlayer;
    public Animator anim;
    private bool Flopped = false;
    public bool DestroyDetection = false;
    private GameObject ToBeDestroyed;

    [SerializeField] float maxVelocityWhileGrounded = 5f;
    [SerializeField] float maxVelocityWhileFalling = 3f;
    [SerializeField] float moveForce = 10000f;
    [SerializeField] float jumpForce = 50000f;

    [SerializeField] Transform[] collectibleLocations;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() { }

    private void FixedUpdate() {
        // set isGrounded
        float distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
        isGrounded = Physics.Raycast(rb.position, -Vector3.up, distToGround + 0.1f);
        //RaycastHit hit;
        //Ray ray = new Ray(transform.position, -Vector3.up);
        //if (Physics.Raycast(ray, out hit, distToGround + 0.1f, Physics.AllLayers)) {
        //    if (hit.collider != null) {
        //        if (hit.collider.CompareTag("Elevator")) {
        //            print("on elevator");
        //        }
        //    }
        //}

        // calculate force applied by input
        Vector3 moveDir = rawInputMovement.normalized;

        if (Flopped)
        {
            moveDir = new Vector3(0, 0, 0);
            anim.SetBool("Bounce", false);
            
        }

        //rotate player
        //transform.LookAt(transform.position + moveDir);
        if (moveDir != new Vector3(0, 0, 0))
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.1f);
            anim.SetBool("IsWalking", true);
        } else
        {
            anim.SetBool("IsWalking", false);
        }
        

        Vector3 force = moveDir * moveForce;

        if (!isGrounded) {
            force *= 0.5f;
        }
        
        rb.AddForce(force);

        // clamp velocity
        Vector3 vel = rb.velocity;
	    vel.y = 0;


        if (isGrounded)
        {
            vel = Vector3.ClampMagnitude(vel, maxVelocityWhileGrounded);
            if (!BigPlayer)
            {
                anim.SetBool("IsJumping", false);
            }
        }
        else
        {
            vel = Vector3.ClampMagnitude(vel, maxVelocityWhileFalling);
            if (!BigPlayer)
            {
                anim.SetBool("IsJumping", true);
            }
        }

        vel.y = rb.velocity.y;
	    rb.velocity = vel;

        // attempt to jump
        if (jumpPressed && (isGrounded || Flopped)) {
            if (BigPlayer)
            {
                Flopped = !Flopped;
                if (Flopped)
                {
                    GetComponent<CapsuleCollider>().enabled = false;
                    GetComponent<BoxCollider>().enabled = true;
                }
                else
                {
                    GetComponent<CapsuleCollider>().enabled = true;
                    GetComponent<BoxCollider>().enabled = false;
                }
                anim.SetBool("Flop", Flopped);
            } else
            {
                rb.AddForce(Vector3.up * jumpForce);
            }
            jumpPressed = false;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (Flopped && collision.gameObject.tag == "Player")
        {
            collision.rigidbody.AddForce(Vector3.up * 5000f);
            anim.SetBool("Bounce", true);
        } else if (Flopped && collision.gameObject.tag == "Destructable")
        {
            if (DestroyDetection)
            {
                collision.gameObject.GetComponent<Destructable>().Destruction();
            }
            else
            {
                ToBeDestroyed = collision.gameObject;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Collectible")) {
            if (other.GetComponent<Collectible>().IsCollected()) return;

            for (int i = 0; i < collectibleLocations.Length; i++) {
                if (collectibleLocations[i].transform.childCount == 0) {
                    other.transform.parent = collectibleLocations[i].transform;
                    other.transform.localPosition = Vector3.zero;
                    other.GetComponent<Collectible>().Collect();
                    break;
                }
            }
        }
    }

    public void DestroyObject()
    {
        if (ToBeDestroyed)
        {
            ToBeDestroyed.GetComponent<Destructable>().Destruction();
        }
    }
}
