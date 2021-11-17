using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {

    public CannonManager myCannonScript;
    public MouseInputFW clickScript;
    public UIFW uIScript;

    //CONTROLS
    public KeyCode seekTargetKey;

    //LOCAL VARIABLES
    public GameObject[] enemyTargets;
    public bool CloseEnoughToFire;
    public float distanceToTarget;

    public bool waitingToScoutAgain = false;
    public float scoutingInterval;

    private void Start() {
        myCannonScript = gameObject.GetComponent<CannonManager>();
        clickScript = GameObject.Find("MouseInputFW").GetComponent<MouseInputFW>();
        uIScript = GameObject.Find("UIManager").GetComponent<UIFW>();
    }

    private void OnMouseDown() { //delegate all click tasks to MouseInputFW
        clickScript.ClickedOnShip(gameObject);
    }

    private void Update() {
            if (waitingToScoutAgain == false) {
                StartCoroutine("FindBestTarget");
            }
        //check if close enough to fire
        if (distanceToTarget <= myCannonScript.rangeOfGuns) {
            CloseEnoughToFire = true;
        } else {
            CloseEnoughToFire = false;
        }
    }

    IEnumerator FindBestTarget() { //add available PCannons to array --- measure new distance to first cannon in array
        waitingToScoutAgain = true;
        enemyTargets = GameObject.FindGameObjectsWithTag("PlayerCannon");
        myCannonScript.targetedGO = enemyTargets[0]; //FAILSAFE: set first member of array as target
        distanceToTarget = Vector3.Distance(gameObject.transform.position, enemyTargets[0].transform.position);
        print(gameObject.name + "'s dist to target: " + distanceToTarget);

        for (int i = 1; i < enemyTargets.Length; ++i) { //Go through the list of previously found cannon --- Find nearest cannon and set as target
            float nextCannonDist = Vector3.Distance(gameObject.transform.position, enemyTargets[i].transform.position);
            print(gameObject.name + "'s dist to target: " + distanceToTarget);
            if (nextCannonDist < distanceToTarget) {
                distanceToTarget = nextCannonDist;
                print(distanceToTarget);
                myCannonScript.targetedGO = enemyTargets[i];
            }
        } //find HP script for new target cannon
        myCannonScript.AssignNewTargetHPScript();

        yield return new WaitForSeconds(scoutingInterval);
        waitingToScoutAgain = false;
    }
}
