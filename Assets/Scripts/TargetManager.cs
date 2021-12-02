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

    public GameObject rangeCylinder;

    private void Start() {
        myCannonScript = gameObject.GetComponent<CannonManager>();
        clickScript = GameObject.Find("MouseInputFW").GetComponent<MouseInputFW>();
        uIScript = GameObject.Find("UIManager").GetComponent<UIFW>();
        rangeCylinder = GetComponentInChildren<ShipRangeCylinderControl>().gameObject;
    }

    private void OnMouseDown() { //delegate all click tasks to MouseInputFW
        clickScript.ClickedOnShip(gameObject);
    }

    private void OnMouseEnter() {
        clickScript.MouseOverShip(gameObject, rangeCylinder);
    }

    private void OnMouseExit() {
        clickScript.MouseExitShip(gameObject, rangeCylinder);
    }

    private void Update() {
        if (waitingToScoutAgain == false || myCannonScript.targetedGO.GetComponentInParent<CannonCrew>().stationHasBeenWiped == true) {
            StartCoroutine("FindBestTarget");
        }
        //check if close enough to fire
        if ((distanceToTarget * 2) <= myCannonScript.rangeOfGuns) {
            CloseEnoughToFire = true;
        } else {
            CloseEnoughToFire = false;
        }
    }

    IEnumerator FindBestTarget() { //add available PCannons to array --- measure new distance to first cannon in array
        waitingToScoutAgain = true;
        enemyTargets = GameObject.FindGameObjectsWithTag("PlayerCannon");

        for (int i = 0; i < enemyTargets.Length; ++i) { //Go through the list of previously found cannon --- Find nearest cannon and set as target
            float nextCannonDist = Vector3.Distance(gameObject.transform.position, enemyTargets[i].transform.position);

            if (nextCannonDist < distanceToTarget)  {
                if (enemyTargets[i].GetComponentInParent<CannonCrew>().stationHasBeenWiped == false) {//check if new target candidate is intact
                    distanceToTarget = nextCannonDist;
                    print(distanceToTarget);
                    myCannonScript.targetedGO = enemyTargets[i];
                }
            }
        } //find HP script for new target cannon
        myCannonScript.AssignNewTargetHPScript();

        yield return new WaitForSeconds(scoutingInterval);
        waitingToScoutAgain = false;
    }
}
