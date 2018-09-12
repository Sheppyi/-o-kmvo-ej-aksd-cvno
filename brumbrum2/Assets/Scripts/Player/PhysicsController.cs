using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhysicsController : MonoBehaviour {


    //references
    
    public LayerMask collisionMask;
    public GameObject car;
    MovementController movementController;
    AnimationController animationController;
    public GameObject hitObject;
    Rigidbody thisRigidbody;
    Vector3 hitNormal;
    Vector3 oldRotation;

    //gamevariables
    float maxGroundedDistance = 6;
    public bool isGrounded = false;
    

    //internal
    public float animationRotation;

    private void Start() {
        thisRigidbody = GetComponent<Rigidbody>();
        movementController = this.GetComponent<MovementController>();
        animationController = this.GetComponent<AnimationController>();
    }

    public void Move(Vector3 direction,Vector3 worldDirectionGravity, float facing, float animationRotation) {
        CheckIfGrounded();
        if (isGrounded) {
            
            transform.rotation = animationController.CalculateTilt(oldRotation, Quaternion.FromToRotation(Vector3.up, hitNormal).eulerAngles);
            oldRotation = Quaternion.FromToRotation(Vector3.up, hitNormal).eulerAngles;
            transform.Rotate(0, facing, 0, Space.Self);
        }
        else {

        }
        car.transform.localEulerAngles = new Vector3(animationRotation,0,0);
        thisRigidbody.AddForce(worldDirectionGravity);
        thisRigidbody.AddRelativeForce(new Vector3(-direction.x, direction.y, direction.z));
    }

    void CheckIfGrounded() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, movementController.gravityDirection ,out hit, maxGroundedDistance, collisionMask)) {
            isGrounded = true;
            hitObject = hit.transform.gameObject;
            hitNormal = hit.normal;
            
        }
        else {
            isGrounded = false;
            hitObject = null;
        }
    }








}
