using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhysicsController : MonoBehaviour {


    //references
    RaycastOrigins ro;
    public LayerMask collisionMask;
    public GameObject car;
    MovementController movementController;
    public Transform cool;

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
        finalDirection += -finalDirection + -transform.right * direction.x + transform.forward * direction.y + transform.up * direction.z + worldDirectionGravity;
        CheckCollisions();
        CheckIfGrounded();

        transform.position += finalDirection;
        car.transform.localRotation = Quaternion.Euler(rotation, facing, -tilt);
    }

    void CheckCollisions() {
        UpdateRayCastOrigins();
        CornerCheck();
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

    void CornerCheck() {
        RaycastHit hit;
        if (finalDirection.y < 0 && Physics.Raycast(ro.bottomLeftFront, Vector3.down, out hit, Mathf.Abs(finalDirection.y * lengthModifier), collisionMask)) {
            finalDirection.y = finalDirection.y > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.y = 0;
                movementController.gravity.y = 0;
            }
            else {
                finalDirection.y *= (hit.distance - cornerCorrection);
            }
        }
        if (finalDirection.x < 0 && Physics.Raycast(ro.bottomLeftFront, Vector3.left, out hit, Mathf.Abs(finalDirection.x * lengthModifier), collisionMask)) {
            finalDirection.x = finalDirection.x > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.x = 0;
                movementController.gravity.x = 0;
            }
            else {
                finalDirection.x *= (hit.distance - cornerCorrection);
            }
        }
        if (finalDirection.z < 0 && Physics.Raycast(ro.bottomLeftFront, Vector3.back, out hit, Mathf.Abs(finalDirection.z * lengthModifier), collisionMask)) {
            finalDirection.z = finalDirection.z > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.z = 0;
                movementController.gravity.z = 0;
            }
            else {
                finalDirection.z *= (hit.distance - cornerCorrection);
            }
        }
        //1
        if (finalDirection.y < 0 && Physics.Raycast(ro.bottomLeftBack, Vector3.down, out hit, Mathf.Abs(finalDirection.y * lengthModifier), collisionMask)) {
            finalDirection.y = finalDirection.y > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.y = 0;
                movementController.gravity.y = 0;
            }
            else {
                finalDirection.y *= (hit.distance - cornerCorrection);
            }
        }
        if (finalDirection.x > 0 && Physics.Raycast(ro.bottomLeftBack, Vector3.right, out hit, Mathf.Abs(finalDirection.x * lengthModifier), collisionMask)) {
            finalDirection.x = finalDirection.x > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.x = 0;
                movementController.gravity.x = 0;
            }
            else {
                finalDirection.x *= (hit.distance - cornerCorrection);
            }
        }
        if (finalDirection.z < 0 && Physics.Raycast(ro.bottomLeftBack, Vector3.back, out hit, Mathf.Abs(finalDirection.z * lengthModifier), collisionMask)) {
            finalDirection.z = finalDirection.z > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.z = 0;
                movementController.gravity.z = 0;
            }
            else {
                finalDirection.z *= (hit.distance - cornerCorrection);
            }
        }
        //2
        if (finalDirection.y < 0 && Physics.Raycast(ro.bottomRightFront, Vector3.down, out hit, Mathf.Abs(finalDirection.y * lengthModifier), collisionMask)) {
            finalDirection.y = finalDirection.y > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.y = 0;
                movementController.gravity.y = 0;
            }
            else {
                finalDirection.y *= (hit.distance - cornerCorrection);
            }
        }
        if (finalDirection.x < 0 && Physics.Raycast(ro.bottomRightFront, Vector3.left, out hit, Mathf.Abs(finalDirection.x * lengthModifier), collisionMask)) {
            finalDirection.x = finalDirection.x > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.x = 0;
                movementController.gravity.x = 0;
            }
            else {
                finalDirection.x *= (hit.distance - cornerCorrection);
            }
        }
         if (finalDirection.z > 0 && Physics.Raycast(ro.bottomRightFront, Vector3.forward, out hit, Mathf.Abs(finalDirection.z * lengthModifier), collisionMask)) {
            finalDirection.z = finalDirection.z > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.z = 0;
                movementController.gravity.z = 0;
            }
            else {
                finalDirection.z *= (hit.distance - cornerCorrection);
            }
        }
        //3
        if (finalDirection.y < 0 && Physics.Raycast(ro.bottomRightBack, Vector3.down, out hit, Mathf.Abs(finalDirection.y * lengthModifier), collisionMask)) {
            finalDirection.y = finalDirection.y > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.y = 0;
                movementController.gravity.y = 0;
            }
            else {
                finalDirection.y *= (hit.distance - cornerCorrection);
            }
        }
        if (finalDirection.x > 0 && Physics.Raycast(ro.bottomRightBack, Vector3.right, out hit, Mathf.Abs(finalDirection.x * lengthModifier), collisionMask)) {
            finalDirection.x = finalDirection.x > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.x = 0;
                movementController.gravity.x = 0;
            }
            else {
                finalDirection.x *= (hit.distance - cornerCorrection);
            }
        }
        if (finalDirection.z > 0 && Physics.Raycast(ro.bottomRightBack, Vector3.forward, out hit, Mathf.Abs(finalDirection.z * lengthModifier), collisionMask)) {
            finalDirection.z = finalDirection.z > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.z = 0;
                movementController.gravity.z = 0;
            }
            else {
                finalDirection.z *= (hit.distance - cornerCorrection);
            }
        }
        //4
        if (finalDirection.y > 0 && Physics.Raycast(ro.topLeftFront, Vector3.up, out hit, Mathf.Abs(finalDirection.y * lengthModifier), collisionMask)) {
            finalDirection.y = finalDirection.y > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.y = 0;
                movementController.gravity.y = 0;
            }
            else {
                finalDirection.y *= (hit.distance - cornerCorrection);
            }
        }
        if (finalDirection.x < 0 && Physics.Raycast(ro.topLeftFront, Vector3.left, out hit, Mathf.Abs(finalDirection.x * lengthModifier), collisionMask)) {
            finalDirection.x = finalDirection.x > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.x = 0;
                movementController.gravity.x = 0;
            }
            else {
                finalDirection.x *= (hit.distance - cornerCorrection);
            }
        }
        if (finalDirection.z < 0 && Physics.Raycast(ro.topLeftFront, Vector3.back, out hit, Mathf.Abs(finalDirection.z * lengthModifier), collisionMask)) {
            finalDirection.z = finalDirection.z > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.z = 0;
                movementController.gravity.z = 0;
            }
            else {
                finalDirection.z *= (hit.distance - cornerCorrection);
            }
        }
        //5
        if (finalDirection.y > 0 && Physics.Raycast(ro.topRightBack, Vector3.up, out hit, Mathf.Abs(finalDirection.y * lengthModifier), collisionMask)) {
            finalDirection.y = finalDirection.y > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.y = 0;
                movementController.gravity.y = 0;
            }
            else {
                finalDirection.y *= (hit.distance - cornerCorrection);
            }
        }
        if (finalDirection.x > 0 && Physics.Raycast(ro.topRightBack, Vector3.right, out hit, Mathf.Abs(finalDirection.x * lengthModifier), collisionMask)) {
            finalDirection.x = finalDirection.x > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.x = 0;
                movementController.gravity.x = 0;
            }
            else {
                finalDirection.x *= (hit.distance - cornerCorrection);
            }
        }
        if (finalDirection.z > 0 && Physics.Raycast(ro.topRightBack, Vector3.forward, out hit, Mathf.Abs(finalDirection.z * lengthModifier), collisionMask)) {
            finalDirection.z = finalDirection.z > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.z = 0;
                movementController.gravity.z = 0;
            }
            else {
                finalDirection.z *= (hit.distance - cornerCorrection);
            }
        }
        //
        if (finalDirection.y > 0 && Physics.Raycast(ro.topRightFront, Vector3.up, out hit, Mathf.Abs(finalDirection.y * lengthModifier), collisionMask)) {
            finalDirection.y = finalDirection.y > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.y = 0;
                movementController.gravity.y = 0;
            }
            else {
                finalDirection.y *= (hit.distance - cornerCorrection);
            }
        }
        if (finalDirection.x < 0 && Physics.Raycast(ro.topRightFront, Vector3.left, out hit, Mathf.Abs(finalDirection.x * lengthModifier), collisionMask)) {
            finalDirection.x = finalDirection.x > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.x = 0;
                movementController.gravity.x = 0;
            }
            else {
                finalDirection.x *= (hit.distance - cornerCorrection);
            }
        }
        if (finalDirection.z > 0 && Physics.Raycast(ro.topRightFront, Vector3.forward, out hit, Mathf.Abs(finalDirection.z * lengthModifier), collisionMask)) {
            finalDirection.z = finalDirection.z > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.z = 0;
                movementController.gravity.z = 0;
            }
            else {
                finalDirection.z *= (hit.distance - cornerCorrection);
            }
        }
        //
        if (finalDirection.y > 0 && Physics.Raycast(ro.topLeftBack, Vector3.up, out hit, Mathf.Abs(finalDirection.y * lengthModifier), collisionMask)) {
            finalDirection.y = finalDirection.y > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.y = 0;
                movementController.gravity.y = 0;
            }
            else {
                finalDirection.y *= (hit.distance - cornerCorrection);
            }
        }
        if (finalDirection.x > 0 && Physics.Raycast(ro.topLeftBack, Vector3.right, out hit, Mathf.Abs(finalDirection.x * lengthModifier), collisionMask)) {
            finalDirection.x = finalDirection.x > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.x = 0;
                movementController.gravity.x = 0;
            }
            else {
                finalDirection.x *= (hit.distance - cornerCorrection);
            }
        }
        if (finalDirection.z < 0 && Physics.Raycast(ro.topLeftBack, Vector3.back, out hit, Mathf.Abs(finalDirection.z * lengthModifier), collisionMask)) {
            finalDirection.z = finalDirection.z > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= cornerCorrection * cornerCorrectionModifier) {
                finalDirection.z = 0;
                movementController.gravity.z = 0;
            }
            else {
                finalDirection.z *= (hit.distance - cornerCorrection);
            }
        }
        
    }

    void CheckIfGrounded() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, movementController.gravityDirection ,out hit, maxGroundedDistance, collisionMask)) {
            isGrounded = true;
            cool = hit.collider.transform;
        }
        else {
            isGrounded = false;
        }
    }

    struct RaycastOrigins {
        public Vector3 topLeftFront, topRightFront, bottomLeftFront, bottomRightFront;
        public Vector3 topLeftBack, topRightBack, bottomLeftBack, bottomRightBack;
    }




}
