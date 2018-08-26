using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhysicsController : MonoBehaviour {


    //references
    RaycastOrigins ro;
    public LayerMask collisionMask;
    public GameObject car;
    MovementController movementController;
    public GameObject hitObject;

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
        movementController = this.GetComponent<MovementController>();
        extents = this.GetComponent<Collider>().bounds.extents;
    }

    public void Move(Vector3 direction, float rotation,float facing, float tilt,Vector3 worldDirectionGravity) {
        UpdateRayCastOrigins();
        CheckIfGrounded();
        finalDirection += -finalDirection + -transform.right * direction.x + transform.forward * direction.y + transform.up * direction.z + worldDirectionGravity;
        CollisionCheck();

        transform.position += finalDirection;
        car.transform.localRotation = Quaternion.Euler(rotation, facing, -tilt);
    }

    void UpdateRayCastOrigins() {
        var thisMatrix = this.transform.localToWorldMatrix;
        ro.topRightBack = thisMatrix.MultiplyPoint3x4(extents);
        ro.topRightFront = thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, extents.y, extents.z));
        ro.topLeftBack = thisMatrix.MultiplyPoint3x4(new Vector3(extents.x, extents.y, -extents.z));
        ro.topLeftFront = thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, extents.y, -extents.z));
        ro.bottomRightBack = thisMatrix.MultiplyPoint3x4(new Vector3(extents.x, -extents.y, extents.z));
        ro.bottomRightFront= thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, -extents.y, extents.z));
        ro.bottomLeftBack = thisMatrix.MultiplyPoint3x4(new Vector3(extents.x, -extents.y, -extents.z));
        ro.bottomLeftFront = thisMatrix.MultiplyPoint3x4(-extents);
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

    void CollisionCheck() {
        
    }

    private void OnCollisionEnter(Collision collision) {
        movementController.onCollision = true;
        movementController.gravity = new Vector3(0, 0, 0);
    }

    private void OnCollisionExit(Collision collision) {
        movementController.onCollision = false;
    }


    struct RaycastOrigins {
        public Vector3 topLeftFront, topRightFront, bottomLeftFront, bottomRightFront;
        public Vector3 topLeftBack, topRightBack, bottomLeftBack, bottomRightBack;
    }




}
