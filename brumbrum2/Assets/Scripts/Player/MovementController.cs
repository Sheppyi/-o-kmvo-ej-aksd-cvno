using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PhysicsController))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(CarStats))]
public class MovementController : MonoBehaviour {

    public GameObject car;
    PhysicsController psC;
    AnimationController animationController;
    public Vector3 direction = new Vector3(0 , 0 , 0);
    Vector3 gravity = new Vector3(0, 0, 0);
    static Vector3 defaultGravity = new Vector3(0,-2,0);
    Rigidbody thisRigidybody;
    
    
    public float facing = 0;
    float defaultAccelModifier = 30000;
    float accelModifier;
    float decelModifier = 20000;
    public float animationRotation;
    float facingModifier = 5;

    public Vector3 gravityDirection;     //angle from car
    float gravityStrength = 10000;
    const float defaultGravityStrength = 10000;

    //bools
    bool gravityEnabled = true;
    bool isGrounded;
    public bool wasGrounded;


    private void Start() {
        accelModifier = defaultAccelModifier;
        thisRigidybody = GetComponent<Rigidbody>();
        psC = this.GetComponent<PhysicsController>();
        animationController = this.GetComponent<AnimationController>();
        gravityDirection = defaultGravity;
    }

    private void Update() {
        isGrounded = psC.isGrounded;
        ApplyGravity();
        CalculateFacing();
        Movement();
        Finish();
        Misc();
    }

    void ApplyGravity() {
        if (wasGrounded && !isGrounded) {
            ChangeGravity(defaultGravity);
        }else if (gravityDirection != defaultGravity && !isGrounded && !wasGrounded) {
            ChangeGravity(defaultGravity);
        }

        if (gravityEnabled) {
            gravity = gravityDirection * gravityStrength;
        }
    }


    void Movement() {
        if (GetInput.trigger_right && isGrounded) {
            
            direction.x = accelModifier;
        }
        else if(!GetInput.trigger_left){
            direction.x = 0;
        }
        if (isGrounded && GetInput.trigger_left) {
            thisRigidybody.drag = 10;
            thisRigidybody.angularDrag = 1;
        }
        else if(isGrounded){
            thisRigidybody.drag = 1;
            thisRigidybody.angularDrag = 1;
        }
        else {
            thisRigidybody.drag = 0;
            thisRigidybody.angularDrag = 0;
        }
    }

    void CalculateFacing() {
        animationRotation = animationController.CalculateRotation(animationRotation);
        if (isGrounded) {
            facing += animationRotation * Time.deltaTime * facingModifier;
        }
    }

    void Finish() {
        psC.Move(direction * Time.deltaTime, gravity * Time.deltaTime, facing, animationRotation);
    }

    void Misc() {
        wasGrounded = isGrounded;
    }

    public void ChangeGravity(Vector3 newGravityDirection , float newGravityStrength = defaultGravityStrength) {
        gravityDirection = newGravityDirection;
        gravityStrength = newGravityStrength;
        //Debug.Log("Changed gravity to " + gravityDirection);
    }

    public void ChangeSpeed(float speed) {
        accelModifier = speed;
    }
}
