using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFW : MonoBehaviour {
    public Vector3 mousePos;

    public GameObject mouseUIParent;
    public GameObject cursorObj;
    public LineRenderer mouseLine;

    public GameObject lineStartPointGO;
    public GameObject lineEndPointGO;

    public Vector3 lineStartPoint;
    public Vector3 lineEndPoint;

    void FixedUpdate() {
        if (lineStartPointGO != null) {
            //get Vector3 from mouse
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //make cursor graphics follow mouse Vector3
            cursorObj.transform.position = new Vector3(mousePos.x, 10, mousePos.z);

            //assign values to variables
            lineStartPoint = lineStartPointGO.transform.position;   //set lineRenderer start at startGO

            //make line end point be the cursor if no ship end point exists yet
            if (lineEndPointGO == null) {
                lineEndPoint = cursorObj.transform.position + new Vector3(0, 50, 0); //set line end value at cursor 
                cursorObj.transform.position = lineEndPoint;
                cursorObj.SetActive(false);
            } else { //set line end at endGO
                cursorObj.transform.position = lineEndPoint;
                lineEndPoint = lineEndPointGO.transform.position + new Vector3(0, 50, 0);
            }

            //Set line start and end coords
            mouseLine.SetPosition(0, new Vector3(lineStartPoint.x, 50, lineStartPoint.z));
            mouseLine.SetPosition(1, new Vector3(lineEndPoint.x, 50, lineEndPoint.z));
        }
    }

    public void CannonSelectedStart(GameObject startCannon) {
        lineEndPointGO = null;
        lineStartPointGO = startCannon; //set StartPointGO as the cannon we clicked
        if (mouseUIParent.activeSelf == true) {
            mouseUIParent.SetActive(false);
        }
        if (cursorObj == null) {
            cursorObj = mouseUIParent.GetComponentInChildren<MeshRenderer>().gameObject;
        }
        cursorObj.SetActive(false);
        mouseLine = mouseUIParent.GetComponentInChildren<LineRenderer>();

        mouseUIParent.SetActive(true);  //make graphics visible
        mouseLine.enabled = true;

        if (startCannon.GetComponent<TargetLineHandler>().instantiatedLineParent != null) { //reshow white line when clicking cannon again
            startCannon.GetComponent<TargetLineHandler>().instantiatedLineParent.SetActive(false);
            startCannon.GetComponent<TargetLineHandler>().instantiatedLineParent.SetActive(true);
        }
    }

    public void CannonHoverStart(GameObject cannonUnderMouse) {
        if (cannonUnderMouse.GetComponent<TargetLineHandler>().instantiatedLineParent != null) {
            cannonUnderMouse.GetComponent<TargetLineHandler>().instantiatedLineParent.SetActive(false);
            cannonUnderMouse.GetComponent<TargetLineHandler>().instantiatedLineParent.SetActive(true);
        }
    }

    public void CannonHoverEnd(GameObject cannonUnderMouse) {
    }

    public void ShipHoverStart(GameObject shipUnderMouse) {
        if (cursorObj == null) {
            cursorObj = mouseUIParent.GetComponentInChildren<MeshRenderer>().gameObject;
        }
        lineEndPointGO = shipUnderMouse;
        cursorObj.SetActive(true);
    }

    public void ShipHoverEnd(GameObject shipUnderMouse) {
        lineEndPointGO = null;
        cursorObj.SetActive(false);
    }

    public void ShipSelectedStart(GameObject endShip) {
        lineEndPointGO = endShip;  //set line end at ship we clicked
        //HERE: IF LINE HAS ALREADY BEEN INSTANTIATED DO NOT INSTANTIATE NEW ONE?
        lineStartPointGO.GetComponent<TargetLineHandler>().instantiateNewLine(lineStartPointGO, lineEndPointGO);

        mouseUIParent.SetActive(false);
    }
}
