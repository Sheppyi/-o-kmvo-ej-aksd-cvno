using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour {

    GameObject direction;
    Vector3 gravityDirection;

	void Start () {
        direction = new GameObject();
        calculateGravity();
	}

    private void OnCollisionEnter(Collision collision) {
        collision.gameObject.GetComponent<MovementController>().ChangeGravity(gravityDirection);
    }

    void calculateGravity() {
        direction.transform.position = transform.position;
        direction.transform.rotation = transform.rotation;
        direction.transform.position += -transform.up;
        gravityDirection = direction.transform.position-transform.position;
    }

   
}
