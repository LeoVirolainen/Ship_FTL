using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTargetManager : MonoBehaviour {

    public CannonManager myCannonScript;
    public MouseInputFW clickScript;

    //LOCAL VARIABLES
    public GameObject[] enemyTargets;
    [SerializeField] private bool hasTarget = false;

    private void Start() {
        myCannonScript = gameObject.GetComponent<CannonManager>();
        clickScript = GameObject.Find("MouseInputFW").GetComponent<MouseInputFW>();
    }

    private void Update() {
        if (clickScript.selectedTargetForPCannon != null) {
            if (clickScript.selectedPCannon == gameObject) {
                hasTarget = true;
            }
        }
        if (hasTarget == true) {
            if (myCannonScript.targetedGO != null) {
                transform.LookAt(myCannonScript.targetedGO.transform);
                transform.Rotate(0, 180, 0);
            }
        }
    }

    private void OnMouseDown() { //delegate all click tasks to MouseInputFW
        clickScript.ClickedOnPCannon(gameObject);
    }

    private void OnMouseEnter() {
        if (GetComponentInParent<CannonCrew>().stationHasBeenWiped == false) {
            clickScript.MouseOverCannon(gameObject);
        }
    }

    private void OnMouseExit() {
        clickScript.MouseExitCannon(gameObject);
    }

    public void FindTarget() {
        myCannonScript.AssignNewTargetHPScript();
    }
}

