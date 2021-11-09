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
    [SerializeField] private bool hasTarget = false;

    private void Start() {
        myCannonScript = gameObject.GetComponent<CannonManager>();
        clickScript = GameObject.Find("MouseInputFW").GetComponent<MouseInputFW>();        
    }

    private void Update() { //LOOKAT TARGET
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

    public void FindTarget() {
        myCannonScript.AssignNewTargetHPScript();
    }
}

