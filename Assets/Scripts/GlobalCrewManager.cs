using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCrewManager : MonoBehaviour {
    public MouseInputFW mouseScript;
    public GameManager gM;

    public int reinforcePrice = 50;

    private void Start() {
        mouseScript = GameObject.Find("MouseInputFW").GetComponent<MouseInputFW>();
        gM = GetComponent<GameManager>();
    }

    public void ReinforceButtonPressed() {
        if (gM.goldPieces >= reinforcePrice) {
            gM.goldPieces = gM.goldPieces - reinforcePrice;
            if (mouseScript.selectedPCannon != null) { //start crewreinforced() in active player cannon
                mouseScript.selectedPCannon.GetComponentInParent<CannonCrew>().CrewReinforced();
            }
        } else {
            print("Cannot afford reinforcements!");
        }
    }
}
