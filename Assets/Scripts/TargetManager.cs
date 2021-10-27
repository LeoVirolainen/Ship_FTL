using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {

    public CannonManager myCannonScript;
    //[SerializeField] private GameObject myTarget;

    public KeyCode seekTargetKey;

    public GameObject[] enemyTargets;

    private void Start() {
        myCannonScript = gameObject.GetComponent<CannonManager>();

        if (gameObject.tag == "EnemyShip") {
            enemyTargets = GameObject.FindGameObjectsWithTag("PlayerCannon");

        } else if (gameObject.tag == "PlayerCannon") {
            enemyTargets = GameObject.FindGameObjectsWithTag("EnemyShip");
        }
    }

    private void Update() {
        if (Input.GetKeyDown(seekTargetKey)) {
            myCannonScript.targetedGO = enemyTargets[0];
            myCannonScript.AssignNewTargetHPScript();
        }
    }

}
