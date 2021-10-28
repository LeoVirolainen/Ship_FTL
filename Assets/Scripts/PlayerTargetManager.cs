using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetManager : MonoBehaviour {

    public CannonManager myCannonScript;
    public MouseInputFW clickScript;

    //CONTROLS
    public KeyCode seekTargetKey;

    //LOCAL VARIABLES
    public GameObject[] enemyTargets;

    private void Start() {
        myCannonScript = gameObject.GetComponent<CannonManager>();
        clickScript = GameObject.Find("MouseInputFW").GetComponent<MouseInputFW>();
    }

    private void OnMouseDown() {
        clickScript.selectedPCannon = gameObject;
        clickScript.ClickedOnPCannon();
    }

    public void FindTarget() {
        myCannonScript.AssignNewTargetHPScript();
    }
}

