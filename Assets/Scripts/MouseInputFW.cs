using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputFW : MonoBehaviour {
    public GameObject selectedPCannon;
    public GameObject selectedTargetForPCannon;

    public UIFW uIScript;

    public GameObject vfxForSelectedCannon;
    public GameObject instantiatedSelectionVFX;

    private void Start() {
        uIScript = GameObject.Find("UIManager").GetComponent<UIFW>();
    }

   /* private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            GameObject[] hideableUIElements = GameObject.FindGameObjectsWithTag("UIGraphics");
            foreach (GameObject uiElement in hideableUIElements) {
                Destroy(gameObject);
            }
        }
    }*/

    public void ClickedOnPCannon(GameObject clickedCannon) {
        if (instantiatedSelectionVFX != null) { //destroy previous VFX
            Destroy(instantiatedSelectionVFX);
        }
        instantiatedSelectionVFX = Instantiate(vfxForSelectedCannon, clickedCannon.transform.position + new Vector3(0, 1, 0), Quaternion.identity);        

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
