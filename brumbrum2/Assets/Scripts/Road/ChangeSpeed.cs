using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpeed : MonoBehaviour {

    float speed = 40000;

    private void OnCollisionStay(Collision collision) {
        try {
            collision.gameObject.GetComponent<MovementController>().ChangeSpeed(speed);
        }
        catch {
            Debug.Log("Gravity collider collided with non player object");
        }
    }

}
