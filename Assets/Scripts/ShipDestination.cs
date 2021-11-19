using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDestination : MonoBehaviour {
    public ShipMover shipMoveScript;

    private void OnTriggerEnter(Collider other) {
        if (shipMoveScript != null) {
            if (other == shipMoveScript.gameObject.GetComponent<CapsuleCollider>()) { //check if collider is MY ship
                shipMoveScript.hasDestination = false;
                Destroy(gameObject);
            }
        }
    }
}
