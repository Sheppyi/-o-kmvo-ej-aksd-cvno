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
    public float targetTilt;
    public float targetFacing;
    float facing = 90;
    float tilt = 0;
    float speed;
    float oldSpeed;
    float maxSpeed = 10000;
    float accelModifier = 20000;
    float decelModifier = 20000;
    
    [HideInInspector]
    public Vector3 gravityDirection;     //angle from car
    float gravityStrength = 5000;
    const float defaultGravityStrength = 5000;

 
    //bools
    bool gravityEnabled = true;
    public bool onCollision;
    bool isGrounded;
    bool wasGrounded;


    private void Start() {
        onCollision = false;
        psC = this.GetComponent<PhysicsController>();
        animationController = this.GetComponent<AnimationController>();
        gravityDirection = new Vector3(0, -1, 0);
        
    }

    private void Update() {
        isGrounded = psC.isGrounded;
        ApplyGravity();
        CalculateRotation();
        Movement();
        //rotation += 50 * Time.deltaTime;
        //facing += 80 * Time.deltaTime;

        Finish();

        wasGrounded = isGrounded;
    }

    void ApplyGravity() {
        if (gravityEnabled) {
            gravity = gravityDirection * gravityStrength;
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
            if (rotation >= 360)
                rotation -= 360;
            else if (rotation < 0)
                rotation += 360;
        }
        else {
            targetRotation = psC.hitObject.transform.localEulerAngles.x;
            targetTilt = -psC.hitObject.transform.localEulerAngles.z;
            targetFacing = psC.hitObject.transform.localEulerAngles.y;


            rotationAccel += animationController.angleDifference * angleDifferenceModifier * Time.deltaTime;
            rotation += rotationAccel * Time.deltaTime;
            if (rotationAccel > 0) {
                rotationAccel -= (rotationDecel + (rotationAccel / 2)) * Time.deltaTime;
                if (rotationAccel < 0)
                    rotationAccel = 0;
            }
            else {
                rotationAccel += (rotationDecel + (-rotationAccel / 2)) * Time.deltaTime;
                if (rotationAccel > 0)
                    rotationAccel = 0;
            }
            if (rotation >= 360)
                rotation -= 360;
            else if (rotation < 0)
                rotation += 360;

            if (rotation < 270 && rotation > 90) {
                if (!wasGrounded) {
                    Debug.Log("Crash");
                }
                rotationAccel = 0;
                if (rotation > 180) {
                    rotation = 270;
                }
                else {
                    rotation = 90;
                }
            }


        }
    }

    void Movement() {
        oldSpeed = speed;
        float time = Time.deltaTime;
        if (GetInput.trigger_right) {
            direction.x = accelModifier;
        }
        else {
            direction.x = 0;
        }
    }

    void Finish() {
        psC.Move(direction * Time.deltaTime, rotation, facing, tilt, gravity * Time.deltaTime);
    }

    public void ChangeGravity(Vector3 newGravityDirection , float newGravityStrength = defaultGravityStrength) {
        gravityDirection = newGravityDirection;
        gravityStrength = newGravityStrength;
        Debug.Log("Changed gravity to " + gravityDirection);
    }
}
