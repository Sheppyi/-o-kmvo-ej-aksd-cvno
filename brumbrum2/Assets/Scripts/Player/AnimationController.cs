using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    public GameObject rightRocket;
    public GameObject leftRocket;
    public GameObject animationLayer;
    PhysicsController psC;
    MovementController movementController;
    float maxAngle = 45;
    float input;
    float angle;
    public float angleDifference;
    //Everything rotation
    public float animationRotation;
    public float animationRotationTarget;
    float rotationAccel;
    float rotationDecel = 100f;
    float angleDifferenceModifier = 15;
    float maxRotationAngle = 45;
    float responsiveness = 0;
    //Tilt
    Vector3 oldTilt;
    Vector3 fromTilt;
    Vector3 toTilt;
    Vector3 currentRotation;
    public float lerpPercentage;
    float timeItTakes = 500;
    float currentTime;
    bool inTiltAnimation;

    private void Start() {
        responsiveness = this.gameObject.GetComponent<CarStats>().responsiveness;
        psC = this.GetComponent<PhysicsController>();
        movementController = GetComponent<MovementController>();
    }

    private void Update() {
        //rightRocket
        input = GetInput.axis_right_vertical;
        if (Mathf.Abs(input) > GameSettings.axisRightVerticalDeadzone) {
            if (input > 0) {
                angle = SuperLerp(1, -maxAngle, (float)(GameSettings.axisRightVerticalDeadzone), 0.9f, input);
            }
            else {
                angle = SuperLerp(maxAngle, 1, -0.9f, (float)(-GameSettings.axisRightVerticalDeadzone), input);
            }
        }
        else {
            angle = 0;
        }
        rightRocket.transform.localRotation = Quaternion.Euler(-angle, -90, 180);
        angleDifference = -angle;
        //leftRocket
        input = GetInput.axis_left_vertical;
        if (Mathf.Abs(input) > GameSettings.axisLeftVerticalDeadzone) {
            if (input > 0) {
                angle = SuperLerp(1, -maxAngle, (float)(GameSettings.axisLeftVerticalDeadzone), 0.9f, input);
            }
            else {
                angle = SuperLerp(maxAngle, 1, -0.9f, (float)(-GameSettings.axisLeftVerticalDeadzone), input);
            }
        }
        else {
            angle = 0;
        }
        leftRocket.transform.localRotation = Quaternion.Euler(-angle, -90, 0);
        angleDifference += angle;
    }


    public float CalculateRotation(float currentRotation) {
        if (!psC.isGrounded) {
            rotationAccel += angleDifference * angleDifferenceModifier * Time.deltaTime;
            animationRotationTarget += rotationAccel * Time.deltaTime;
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
            if (animationRotationTarget > 180)
                animationRotationTarget -= 360;
            else if (animationRotationTarget < -180)
                animationRotationTarget += 360;
        }
        else {
            //PlayerInput
            animationRotationTarget = angleDifference / 2;
            if (animationRotationTarget >= 180)
                animationRotationTarget -= 360;
            else if (animationRotationTarget < -180)
                animationRotationTarget += 360;
            if (Mathf.Abs(animationRotationTarget) >= maxRotationAngle) {
                if (animationRotationTarget > 0) {
                    animationRotationTarget = maxRotationAngle;
                }
                else {
                    animationRotationTarget = -maxRotationAngle;
                }
                rotationAccel = 0;
            }
        }
        //Smooth Rotation
        if (psC.isGrounded) {
            animationRotation = animationRotationTarget - currentRotation;
            if (Mathf.Abs(animationRotation) > responsiveness * Time.deltaTime) {
                if (animationRotation > 0) {
                    animationRotation = (currentRotation + responsiveness * Time.deltaTime);
                }
                else {
                    animationRotation = (currentRotation + -responsiveness * Time.deltaTime);
                }
                return animationRotation;
            }
        }
        return animationRotationTarget;
    }


    public static float SuperLerp(float output1, float output2, float input1, float input2, float value) {
        if (value <= input1)
            return output1;
        else if (value >= input2)
            return output2;
        return (output2 - output1) * ((value - input1) / (input2 - input1)) + output1;
    }
}
