using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PhysicsController))]
[RequireComponent(typeof(AnimationController))]
public class MovementController : MonoBehaviour {

    PhysicsController psC;
    AnimationController animationController;
    Vector3 direction = new Vector3(0 , 0 , 0);
    public Vector3 gravity = new Vector3(0, 0, 0);

    public float rotation = 0;
    public float rotationAccel;
    float rotationDecel = 100f;
    float angleDifferenceModifier = 15;
    public float targetRotation;
    float facing = 0;
    float tilt = 0;
    
    [HideInInspector]
    public Vector3 gravityDirection;     //angle from car
    float gravityStrength = 80;
    Vector3 maxGravitySpeed = new Vector3(100,100,100);
 
    //bools
    bool gravityEnabled = true;
    public bool onCollision;


    private void Start() {
        onCollision = false;
        psC = this.GetComponent<PhysicsController>();
        animationController = this.GetComponent<AnimationController>();
        gravityDirection = new Vector3(0, -1f, 1);
        
    }

    private void Update() {
        ApplyGravity();
        CalculateRotation();
        //rotation += 50 * Time.deltaTime;
        //facing += 80 * Time.deltaTime;

        Finish();
    }

    void ApplyGravity() {
        if (gravityEnabled && !onCollision) {
            gravity += gravityDirection * gravityStrength * Time.deltaTime;
            if (Mathf.Abs(gravity.x) > maxGravitySpeed.x ) {
                Mathf.Clamp(gravity.x, -maxGravitySpeed.x, maxGravitySpeed.x);
            }
            if (Mathf.Abs(gravity.y) > maxGravitySpeed.y) {
                Mathf.Clamp(gravity.y, -maxGravitySpeed.y, maxGravitySpeed.y);
            }
            if (Mathf.Abs(gravity.z) > maxGravitySpeed.z) {
                Mathf.Clamp(gravity.z, -maxGravitySpeed.z, maxGravitySpeed.z);
            }
        }
    }

    void CalculateRotation() {
        if (!psC.isGrounded) {
            rotationAccel += animationController.angleDifference * angleDifferenceModifier * Time.deltaTime;
            rotation += rotationAccel * Time.deltaTime;
            if (rotationAccel > 0) {
                rotationAccel -= (rotationDecel + (rotationAccel / 2)) * Time.deltaTime ;
                if (rotationAccel < 0)
                    rotationAccel = 0;
            }
            else { 
                rotationAccel += (rotationDecel + (-rotationAccel / 2)) * Time.deltaTime ;
                if (rotationAccel > 0)
                    rotationAccel = 0;
            }
        }
        else {
            targetRotation = psC.hitObject.transform.localEulerAngles.x;
            rotation = targetRotation;
        }
    }

    void Finish() {
        if (rotation >= 360) 
            rotation -= 360;
        else if (rotation < 0) 
            rotation += 360;
        psC.Move(direction * Time.deltaTime, rotation, facing, tilt, gravity * Time.deltaTime);
    }

    public void ChangeGravity(Vector3 newGravityDirection , float newGravityStrength) {
        gravityDirection = newGravityDirection;
        gravityStrength = newGravityStrength;
    }
}
