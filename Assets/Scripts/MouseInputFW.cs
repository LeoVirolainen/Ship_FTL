using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputFW : MonoBehaviour {
    public GameObject selectedPCannon;
    public GameObject selectedTargetForPCannon;

    public GlobalCrewManager gCM;
    public UIFW uIScript;
    public CursorGraphicsHandler cursorScript;

    public GameObject vfxForSelectedCannon;
    private GameObject instantiatedSelectionVFX;
    public GameObject vfxForHoveredCannon;
    private GameObject instantiatedHoverVFX;

    private void Start() {
        uIScript = GameObject.Find("UIManager").GetComponent<UIFW>();
        gCM = GameObject.Find("GameManager").GetComponent<GlobalCrewManager>();
        cursorScript = GameObject.Find("UIManager").GetComponent<CursorGraphicsHandler>();
    }

    public void ClickedOnPCannon(GameObject clickedCannon) {

        if (instantiatedSelectionVFX != null) { //destroy previous VFX
            Destroy(instantiatedSelectionVFX);
        }
        if (gCM.reinforcing == false) {
            instantiatedSelectionVFX = Instantiate(vfxForSelectedCannon, clickedCannon.transform.position + new Vector3(0, 2, 2), Quaternion.identity);
        }
        selectedPCannon = clickedCannon;
        print("You clicked a cannon!");

        if (gCM.reinforcing == false) {
            AudioFW.Play("sfx_CrewYes0");
            //Make selected cannon find all enemy ships
            selectedPCannon.GetComponent<PlayerTargetManager>().enemyTargets = GameObject.FindGameObjectsWithTag("EnemyShip");
            //tell ui script to show targeting graphics
            uIScript.CannonSelectedStart(clickedCannon);

        } else {
            //if reinforce button has been pressed:
            gCM.ReinforceCannonCrew(selectedPCannon);
        }
    }

    public void ClickedOnShip(GameObject clickedAgent) {
        //Play SFX
        AudioFW.Play("sfx_Targeting0"); AudioFW.Play("sfx_CrewYes1");

        selectedTargetForPCannon = clickedAgent;
        print("You clicked a ship!");
        //give clickedAgent (ship) to active cannon
        selectedPCannon.GetComponent<CannonManager>().targetedGO = selectedTargetForPCannon;
        selectedPCannon.GetComponent<PlayerTargetManager>().FindTarget();

        uIScript.ShipSelectedStart(clickedAgent);

        //cursorScript.TargetEnd();
    }

    public void MouseOverCannon(GameObject cannonUnderMouse) {
        if (instantiatedHoverVFX != null) { //destroy previous VFX
            Destroy(instantiatedHoverVFX);
        }
        instantiatedHoverVFX = Instantiate(vfxForHoveredCannon, cannonUnderMouse.transform.position + new Vector3(0, 1, 2), Quaternion.identity);
        uIScript.CannonHoverStart(cannonUnderMouse);
    }

    public void MouseExitCannon(GameObject cannonUnderMouse) {
        if (instantiatedHoverVFX != null) { //destroy previous VFX
            Destroy(instantiatedHoverVFX);
        }
        //uIScript.CannonHoverEnd(cannonUnderMouse);
    }

    public void MouseOverShip(GameObject shipUnderMouse, GameObject rangeCyl) {
        //Play SFX
        AudioFW.Play("sfx_Targeting1");

        rangeCyl.GetComponent<Animator>().Play("Intro");

        uIScript.ShipHoverStart(shipUnderMouse);
    }

    public void MouseExitShip(GameObject shipUnderMouse, GameObject rangeCyl) {
        rangeCyl.GetComponent<Animator>().Play("Outro");

        uIScript.ShipHoverEnd(shipUnderMouse);
    }
}
