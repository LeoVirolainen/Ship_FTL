using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCrew : MonoBehaviour {
    public int crewDamage;
    public bool stationHasBeenWiped = false;
    public bool startAsWiped = false;

    public HealthPoints hpScript;

    public GameObject[] crewArray;
    public GameObject myCannon;

    public CannonManager myCannonScript;
    public GameOverChecker gameOverScript;

    public void WipeAndStart() {
        hpScript = GetComponentInChildren<HealthPoints>();
        myCannonScript = GetComponentInChildren<CannonManager>();
        gameOverScript = GameObject.Find("GameManager").GetComponent<GameOverChecker>();

        myCannon = GetComponentInChildren<CannonManager>().gameObject;


        if (startAsWiped == true) { //wipe cannon if so need be
            foreach (GameObject crewman in crewArray) {
                crewman.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            }
            hpScript.currentHp = 0;
            crewDamage = 4;
            stationHasBeenWiped = true;
        }
    }

    //decreases amount of visible GOs when is hit
    public void CrewDamaged() {
        //!!! THESE MUST BE ordered so that what we end up with is the right crewHealth value.hierarchy needed? start from lowest number
        //!!! I THINK CREWHEALTH NEEDS TO BE "CREWDAMAGE" INSTEAD, FOR THE FOR-LOOP TO WORK AS INTENDED
        if (hpScript.currentHp <= 0) {                      //if HP is 0
            crewDamage = 4;
            stationHasBeenWiped = true;
        }
        if (hpScript.currentHp < hpScript.maxHp / 4 && stationHasBeenWiped == false) {          //if HP is < 25%
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

        if (crewDamage == 4) {
            CannonWiped();
        }

        if (crewDamage < 4) {
            for (int i = 0; i < crewDamage; ++i) {
                crewArray[i].GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                //Play sound
                StartCoroutine("PlayCrewDeathSFX");               
            } //OR basically just a death animation that does !meshRenderer at end
        }
    }

    private IEnumerator PlayCrewDeathSFX() {
        int randomSound = Random.Range(0, 4);
        yield return new WaitForSeconds(0.6f);
        AudioFW.Play("sfx_CrewDeath" + randomSound);
    }

    //Turn my child crewmen off at 0 HP
    public void CannonWiped() {
        foreach (GameObject crewman in crewArray) {
            crewman.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        }
        gameOverScript.CheckPCannons();
        //myCannon.SetActive(false);
    }


    //This is to be triggered outside this script - clickscript tells reinforcer/globalCrew script who has been clicked? tell that cannoon to do CrewReinforced()
    public void CrewReinforced() { //(only allows 100% restoration, but should work for now)
        /*play sounds*/ AudioFW.Play("sfx_LoseMoney0"); AudioFW.Play("sfx_LoseMoney1");
        stationHasBeenWiped = false;
        hpScript.currentHp = hpScript.maxHp;        //restore cannon HP to max
        myCannonScript.loadTime = myCannonScript.loadTimeWhenFullHealth; //restore full loadingTime
        if (myCannon.activeSelf == false) {         //make sure the cannon is on
            myCannon.SetActive(true);            
        }
        crewDamage = 0; //nullify damage to crew
        foreach (GameObject crewman in crewArray) { //set crewmen visible again
            crewman.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;            
        }
        gameOverScript.CheckPCannons();
    }
}

