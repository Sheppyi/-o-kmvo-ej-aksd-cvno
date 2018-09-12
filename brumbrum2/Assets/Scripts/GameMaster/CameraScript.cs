using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject player;

    float distance = 9;
    float height = 2f;
    


    private void Update() {









        
        transform.rotation = player.transform.rotation;
        transform.Rotate(0,-90,0,Space.Self);
        transform.position = player.transform.position;
        for (int i = 0; i < distance; i++) {
            transform.position += -transform.forward;
        }
        for (int i = 0; i < height; i++) {
            transform.position += transform.up;
        }
        
    }


}
