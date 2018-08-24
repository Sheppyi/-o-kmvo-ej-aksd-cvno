using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PhysicsController))]
public class MovementController : MonoBehaviour {

    PhysicsController psC;
    Vector3 direction = new Vector3(0 , 0 , 0);
    Vector3 worldDirection = new Vector3(0, 0, 0);
    float rotation = 30;
    float facing = 0;
    float tilt = 10;

    Vector3 gravityDirection = new Vector3(0,-1,0);     //angle from car
    float gravityStrength = 80;
    Vector3 maxGravitySpeed = new Vector3(100,100,100);
    



    //bools
    bool gravityEnabled = true;

    private void Start() {
        psC = this.GetComponent<PhysicsController>();
    }


    private void Update() {
        ApplyGravity();
        
        
        psC.Move(direction * Time.deltaTime ,rotation, facing, tilt, worldDirection * Time.deltaTime);
    }



    void ApplyGravity() {
        if (gravityEnabled) {
            worldDirection += gravityDirection * gravityStrength * Time.deltaTime;
            if (Mathf.Abs(worldDirection.x) > maxGravitySpeed.x ) {
                Mathf.Clamp(worldDirection.x, -maxGravitySpeed.x, maxGravitySpeed.x);
            }
            if (Mathf.Abs(worldDirection.y) > maxGravitySpeed.y) {
                Mathf.Clamp(worldDirection.y, -maxGravitySpeed.y, maxGravitySpeed.y);
            }
            if (Mathf.Abs(worldDirection.z) > maxGravitySpeed.z) {
                Mathf.Clamp(worldDirection.z, -maxGravitySpeed.z, maxGravitySpeed.z);
            }
        }
    }





    public void ChangeGravity(Vector3 newGravityDirection , float newGravityStrength) {
        gravityDirection = newGravityDirection;
        gravityStrength = newGravityStrength;
    }
}
