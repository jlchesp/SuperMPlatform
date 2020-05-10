using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour {
    
    //Función para matar al jugador al colisionar con la zona de muerte
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            PlayerController.sharedInstance.Kill();
        }
    }
}
