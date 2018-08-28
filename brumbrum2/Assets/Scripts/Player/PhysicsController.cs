using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhysicsController : MonoBehaviour {


    //references
    
    public LayerMask collisionMask;
    public GameObject car;
    MovementController movementController;
    public GameObject hitObject;
    Rigidbody thisRigidbody;

    //gamevariables
    float maxGroundedDistance = 8;
    public bool isGrounded = false;
    

    //internal
    Vector3 extents;    //extends of collider
    Vector3 finalDirection;
    float cornerCorrection = 0.1f;
    float lengthModifier = 2;
    float cornerCorrectionModifier = 5;

    private void Start() {
        thisRigidbody = GetComponent<Rigidbody>();
        movementController = this.GetComponent<MovementController>();
        extents = this.GetComponent<Collider>().bounds.extents;
    }

    public void Move(Vector3 direction, float rotation,float facing, float tilt,Vector3 worldDirectionGravity) {
        CheckIfGrounded();
        finalDirection += -finalDirection + -transform.right * direction.x + transform.forward * direction.y + transform.up * direction.z + worldDirectionGravity;

        thisRigidbody.AddForce(finalDirection);
        transform.localRotation = Quaternion.Euler(rotation, facing, -tilt);
    }

    void CheckIfGrounded() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, movementController.gravityDirection ,out hit, maxGroundedDistance, collisionMask)) {
            isGrounded = true;
            hitObject = hit.transform.gameObject;
        }
        else {
            isGrounded = false;
            hitObject = null;
        }
    }






}
