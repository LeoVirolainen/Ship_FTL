using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLineHandler : MonoBehaviour {
    public GameObject lineGraphicsPrefab;

    public GameObject lineStartObj;
    public GameObject lineEndObj;

    //"l_" stands for local
    public Vector3 l_lineStartPt;
    public Vector3 l_lineEndPt;

    public GameObject instantiatedLineParent;

    public CannonManager myCannonScript;
    public bool lineHasBeenMade = false;
    public bool lineIsNowVisible = false;

    private void Start() {
        myCannonScript = gameObject.GetComponent<CannonManager>();
    }

    private void FixedUpdate() {
        if (lineEndObj == null) {
            Destroy(instantiatedLineParent);
        }
        if (lineEndObj != null && lineEndObj.GetComponent<HealthPoints>().isSinking == true) {
            Destroy(instantiatedLineParent, 1);
        }
            if (lineHasBeenMade == true) {
            if (instantiatedLineParent != null) {
                if (lineEndObj != null) {
                    LineRenderer currentLine = instantiatedLineParent.GetComponentInChildren<LineRenderer>();

                    l_lineStartPt = lineStartObj.transform.position;
                    l_lineEndPt = lineEndObj.transform.position;

                    currentLine.SetPosition(0, l_lineStartPt);
                    currentLine.SetPosition(1, l_lineEndPt);
                    if (lineIsNowVisible == false) {
                        instantiatedLineParent.SetActive(true);
                    }
                }
            } else {
                lineHasBeenMade = false;
            }
        }
    }

    public void instantiateNewLine(GameObject lineStartPt, GameObject lineEndPt) {
        lineStartObj = myCannonScript.muzzleTransform;
        GameObject newInstantiatedGO = Instantiate(lineGraphicsPrefab, lineStartObj.transform);
        instantiatedLineParent = newInstantiatedGO;
        LineRenderer newLine = newInstantiatedGO.GetComponent<LineRenderer>();

        lineStartObj = lineStartPt;
        lineEndObj = lineEndPt;

        lineHasBeenMade = true;

        //Destroy(instantiatedLineParent, 3f);
    }
}
