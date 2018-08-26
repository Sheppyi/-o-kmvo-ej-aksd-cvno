using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    public GameObject rightRocket;
    public GameObject leftRocket;
    float maxAngle = 45;
    float input;
    float angle;
    public float angleDifference;

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

    public static float SuperLerp(float output1, float output2, float input1, float input2, float value) {
        if (value <= input1)
            return output1;
        else if (value >= input2)
            return output2;
        return (output2 - output1) * ((value - input1) / (input2 - input1)) + output1;
    }
}
