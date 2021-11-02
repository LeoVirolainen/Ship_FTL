using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputFW : MonoBehaviour {
    public GameObject selectedPCannon;
    public GameObject selectedTargetForPCannon;

    public void ClickedOnPCannon() {
        print("You clicked a cannon!");
        //Make selected cannon find all enemy ships
        selectedPCannon.GetComponent<PlayerTargetManager>().enemyTargets = GameObject.FindGameObjectsWithTag("EnemyShip");
    }

    public void ClickedOnShip() {
        print("You clicked a ship!");
        selectedPCannon.GetComponent<CannonManager>().targetedGO = selectedTargetForPCannon;
        selectedPCannon.GetComponent<PlayerTargetManager>().FindTarget();
    }
}
