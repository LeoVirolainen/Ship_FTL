using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputFW : MonoBehaviour {
    public GameObject selectedPCannon;
    public GameObject selectedTargetForPCannon;

    public UIFW uIScript;

    private void Start() {
        uIScript = GameObject.Find("UIManager").GetComponent<UIFW>();
    }

    public void ClickedOnPCannon(GameObject clickedCannon) {
        selectedPCannon = clickedCannon;
        print("You clicked a cannon!");
        //Make selected cannon find all enemy ships
        selectedPCannon.GetComponent<PlayerTargetManager>().enemyTargets = GameObject.FindGameObjectsWithTag("EnemyShip");

        uIScript.CannonSelectedStart(clickedCannon);
    }

    public void ClickedOnShip(GameObject clickedAgent) {
        selectedTargetForPCannon = clickedAgent;
        print("You clicked a ship!");
            //give clickedAgent (ship) to active cannon
        selectedPCannon.GetComponent<CannonManager>().targetedGO = selectedTargetForPCannon;
        selectedPCannon.GetComponent<PlayerTargetManager>().FindTarget();

        uIScript.ShipSelectedStart(clickedAgent);
    }
}
