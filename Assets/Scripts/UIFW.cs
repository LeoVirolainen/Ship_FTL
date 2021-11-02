using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFW : MonoBehaviour {
    public Vector3 mousePos;
    public GameObject cursor;

    public LineRenderer mouseLine;

    public GameObject lineStartPointGO;
    public Vector3 lineStartPoint;

    void FixedUpdate() {
        lineStartPoint = lineStartPointGO.transform.position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.transform.position = new Vector3 (mousePos.x, 10, mousePos.z);

        mouseLine.SetPosition(0, new Vector3(lineStartPoint.x, 10, lineStartPoint.z));
        mouseLine.SetPosition(1, cursor.transform.position);
    }
}
