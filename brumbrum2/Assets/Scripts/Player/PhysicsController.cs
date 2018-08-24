using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhysicsController : MonoBehaviour {


    //references
    RaycastOrigins ro;
    public LayerMask collisionMask;


    //gamevariables
    Vector3 finalDirection;
    


    //internal
    Vector3 extents;    //extends of collider



    private void Start() {
        extents = this.GetComponent<Collider>().bounds.extents;
    }





    public void Move(Vector3 direction, float rotation,float facing, float tilt,Vector3 worldDirecton) {
        finalDirection += -finalDirection + -transform.right * direction.x + transform.forward * direction.y + transform.up * direction.z + worldDirecton;

        CheckCollisions();

        





        transform.localRotation = Quaternion.Euler(0, transform.localRotation.y, transform.localRotation.z);
        transform.position += finalDirection;
        transform.localRotation = Quaternion.Euler(rotation, facing, -tilt);
    }



    void CheckCollisions() {
        UpdateRayCastOrigins();

        RaycastHit hit;
        if (finalDirection.y <= 0 && Physics.Raycast(ro.bottomLeftFront, Vector3.down, out hit, Mathf.Abs(finalDirection.y), collisionMask)) {
            finalDirection.y = finalDirection.y > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= 0.01f) {
                finalDirection.y = 0;
            }
            else {
                finalDirection.y *= (hit.distance - 0.01f);
            }
        }
        else if (finalDirection.y >= 0 && Physics.Raycast(ro.bottomLeftFront, Vector3.up, out hit, Mathf.Abs(finalDirection.y), collisionMask)) {
            finalDirection.y = finalDirection.y > 0 ? 1 : -1;
            if (Mathf.Abs(hit.distance) <= 0.01f) {
                finalDirection.y = 0;
            }
            else {
                finalDirection.y *= (hit.distance - 0.01f);
            }
        }
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



    struct RaycastOrigins {
        public Vector3 topLeftFront, topRightFront, bottomLeftFront, bottomRightFront;
        public Vector3 topLeftBack, topRightBack, bottomLeftBack, bottomRightBack;
    }
}
