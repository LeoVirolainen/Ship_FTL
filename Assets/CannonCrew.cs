using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCrew : MonoBehaviour {
    public int crewDamage;
    public int maxCrewDamage;

    public HealthPoints hpScript;

    public GameObject[] crewArray;

    private void Start() {
        maxCrewDamage = crewDamage;

        hpScript = GetComponentInChildren<HealthPoints>();
    }

    //in HPScript.takeDamage(), call crewScript.CrewDamaged()

    //decreases amount of visible GOs when is hit
    public void CrewDamaged() {
        //!!! THESE MUST BE ordered so that what we end up with is the right crewHealth value.hierarchy needed? start from lowest number
        //!!! I THINK CREWHEALTH NEEDS TO BE "CREWDAMAGE" INSTEAD, FOR THE FOR-LOOP TO WORK AS INTENDED
        if (hpScript.currentHp <= 0) {                      //if HP is 0
            crewDamage = 4;
        }
        if (hpScript.currentHp < hpScript.maxHp / 4) {          //if HP is < 25%
            crewDamage = 3;
        }
        if (hpScript.currentHp >= hpScript.maxHp / 4) {         //if HP is >= 25%
            crewDamage = 2;
        }
        if (hpScript.currentHp >= hpScript.maxHp / 2) {         //if HP is >= 50%
            crewDamage = 1;
        }
        if (hpScript.currentHp >= ((hpScript.maxHp / 4) * 3)) {   //if HP is >= 75%
            crewDamage = 0;
        }

        print(gameObject.name + " casualties: " + crewDamage);

        for (int i = 0; i < crewDamage; ++i) {
            crewArray[i].GetComponent<MeshRenderer>().enabled = false;
        } //OR basically just a death animation that does !meshRenderer at end

        if (crewDamage == 4) {
            CannonWiped();
        }
    }
    //Turn my child PCannonTransform off at 0 HP
    public void CannonWiped() {
        foreach (GameObject crewman in crewArray) {
            GetComponent<MeshRenderer>().enabled = false;
        }
        GameObject myCannon = GetComponentInChildren<CannonManager>().gameObject;
        myCannon.SetActive(false);
    }
}

//(this needs to be in its own object (cannon's parent? NEED NEW PARENT FOR THIS TO WORK :d CALL NEW PARENT "PCannonStation")

/* public void CrewReinforced() { //(only allows 100% restoration, but should work for now)

    StationToReinforce = the station you clicked before clicking "reinforce!"
        if (StationToReinforce.GameObject.FindObjectsInChildren("PCannonTransform") "is not active") //if the TransformGO is not active, make it active
            StationToReinforce.GameObject.FindObjectsInChildren("PCannonTransform").SetActive(true)
            //make all crewmen visible
        foreach(GameObject crewman in StationToReinforce.CrewScript.crewArray) {
crewman.meshRenderer.isActive = true;
//set max crew Health
StationToReinforce.GetComponent<CannonCrew>().crewHealth = maxCrewHealth;
//set max HP
StationToReinforce.GameObject.FindObjectsInChildren("PCannonTransform").GetComponent<HealthPoints>().hP =
StationToReinforce.GameObject.FindObjectsInChildren("PCannonTransform").GetComponent<HealthPoints>().maxhP;
*/

