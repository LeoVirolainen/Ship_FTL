using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalCrewManager : MonoBehaviour {
    public MouseInputFW mouseScript;
    public GameManager gM;
    public CursorGraphicsHandler cursorScript;

    public GameObject reinforceButton;

    public bool reinforcing = false;

    public int reinforcePrice = 30;

    private void Start() {
        mouseScript = GameObject.Find("MouseInputFW").GetComponent<MouseInputFW>();
        cursorScript = GameObject.Find("UIManager").GetComponent<CursorGraphicsHandler>();
        gM = GetComponent<GameManager>();
    }

    public void ReinforceButtonPressed() {
        if (gM.goldPieces >= reinforcePrice) {
            if (reinforcing == false) {
                reinforcing = true;
                cursorScript.StartReinforceCursor();
                reinforceButton.GetComponentInChildren<Text>().text = ("Select Cannon");
            } else {
                StopReinforce(); //cancel reinforcing by clicking button again
            }
        } else {
            print("Cannot afford reinforcements!");
            AudioFW.Play("sfx_WrongBuzzer");
        }
    }

    public void ReinforceCannonCrew(GameObject cannonToReinforce) {
        if (gM.goldPieces >= reinforcePrice) {
            gM.goldPieces = gM.goldPieces - reinforcePrice;
            if (cannonToReinforce != null) { //start crewreinforced() in active player cannon
                cannonToReinforce.GetComponentInParent<CannonCrew>().CrewReinforced();
                reinforcePrice = reinforcePrice * 2;
            }
        }
        StopReinforce();
    }

    public void StopReinforce() {
        reinforcing = false;
        cursorScript.StopReinforceCursor();
        reinforceButton.GetComponentInChildren<Text>().text = ("Reinforce (" + reinforcePrice + "r)");
    }
}
