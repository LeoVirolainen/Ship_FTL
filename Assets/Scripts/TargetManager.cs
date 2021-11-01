using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {

    public CannonManager myCannonScript;
    public MouseInputFW clickScript;

    //CONTROLS
    public KeyCode seekTargetKey;

    //LOCAL VARIABLES
    public GameObject[] enemyTargets;
    public bool CloseEnoughToFire;
    public float distanceToTarget;

    private void Start() {
        myCannonScript = gameObject.GetComponent<CannonManager>();
        clickScript = GameObject.Find("MouseInputFW").GetComponent<MouseInputFW>();
    }

    private void OnMouseDown() {
        clickScript.selectedTargetForPCannon = gameObject;
        clickScript.ClickedOnShip();
    }

    private void Update() {
        if (Input.GetKeyDown(seekTargetKey)) {
            enemyTargets = GameObject.FindGameObjectsWithTag("PlayerCannon");
            FindBestTarget();
        }
    }

    void FindBestTarget() {
        myCannonScript.targetedGO = enemyTargets[0];
        distanceToTarget = Vector3.Distance(gameObject.transform.position, enemyTargets[0].transform.position);
        print("measured dist to first cannon");

        for (int i = 1; i < enemyTargets.Length; ++i) {
            float nextCannonDist = Vector3.Distance(gameObject.transform.position, enemyTargets[i].transform.position);
            print("measured distance to next cannon");
            if (nextCannonDist < distanceToTarget) {
                distanceToTarget = nextCannonDist;
                print(distanceToTarget);
                myCannonScript.targetedGO = enemyTargets[i];
            }
        }

        myCannonScript.AssignNewTargetHPScript();
        if (distanceToTarget <= myCannonScript.rangeOfGuns) {
            CloseEnoughToFire = true;
        } else {
            CloseEnoughToFire = false;
        }
    }
}
