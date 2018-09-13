using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhysicsController : MonoBehaviour {


    //references
    
    public LayerMask collisionMask;
    public LayerMask collisionMaskForRotation;
    public GameObject car;
    MovementController movementController;
    AnimationController animationController;
    public GameObject hitObject;
    Rigidbody thisRigidbody;
    Vector3 pointToFixRotation;
    Vector3 hitNormal;
    Vector3[] averageNormal = new Vector3[20];
    int averageNormalArr;

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

        if (isGrounded) {
            pointToFixRotation = transform.position + transform.forward;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(hitNormal.x, hitNormal.y, hitNormal.z));
            
            transform.Rotate(0, facing, 0, Space.Self);
        }
        car.transform.localEulerAngles = new Vector3(animationRotation,0,0);
        thisRigidbody.AddForce(worldDirectionGravity);
        thisRigidbody.AddRelativeForce(new Vector3(-direction.x, direction.y, direction.z));
    }

    void FixedUpdate() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, movementController.gravityDirection ,out hit, maxGroundedDistance, collisionMask)) {
            isGrounded = true;
            hitObject = hit.transform.gameObject;
            if (Physics.Raycast(transform.position, movementController.gravityDirection, out hit, maxGroundedDistance, collisionMaskForRotation)) {
                averageNormal[averageNormalArr] = hit.normal;
                if (++averageNormalArr >= averageNormal.Length) {
                    averageNormalArr = 0;
                }
                hitNormal = new Vector3();
                for (int i = 0; i < averageNormal.Length; i++) {
                    hitNormal += averageNormal[i];
                }
                hitNormal /= averageNormal.Length;
            }
        }
        else {
            isGrounded = false;
            hitObject = null;
        }
    }








}
