using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravityComplex : MonoBehaviour {


    private void OnCollisionStay(Collision collision) {
        ContactPoint contact = collision.contacts[collision.contacts.Length - 1];
        try {
            contact.otherCollider.GetComponent<MovementController>().ChangeGravity(contact.normal);
        }
        catch {
            Debug.Log("Gravity collider collided with non player object");
        }
    }

}
