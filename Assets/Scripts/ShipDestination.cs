using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDestination : MonoBehaviour {
    public ShipMover shipMoveScript;

    private void OnTriggerEnter(Collider other) {
        shipMoveScript.hasDestination = false;
        Destroy(gameObject);
    }

}
