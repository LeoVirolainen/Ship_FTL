using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRangeCylinderControl : MonoBehaviour {
    public CannonManager cm;

    private void Start() {
        cm = gameObject.GetComponentInParent<CannonManager>();
    }

    // Update is called once per frame
    void Update() {
        gameObject.transform.localScale = new Vector3(cm.rangeOfGuns, 1, cm.rangeOfGuns);
        gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
