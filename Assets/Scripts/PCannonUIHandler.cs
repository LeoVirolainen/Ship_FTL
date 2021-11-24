using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCannonUIHandler : MonoBehaviour {
    public float minRadial = 0;
    public float maxRadial;
    public float currentRadial;

    private bool isVisible = false;

    public Image mask;
    public Image fill;
    public Canvas radialCanvas;

    public float timeWhenTimerStopped;

    public CannonManager cm;
    void Start() {
        maxRadial = GetComponentInParent<CannonManager>().loadTime;
        radialCanvas = GetComponentInChildren<Canvas>();
        cm = GetComponentInParent<CannonManager>();
    }

    void Update() {
        GetCurrentReloadFill();

        radialCanvas.transform.eulerAngles = new Vector3(90, 0, 0);

        if (GetComponentInParent<CannonManager>().targetedGO != null) {
            if (isVisible == false) {
                radialCanvas.enabled = true;
                isVisible = true;
            }
        } else {
            timeWhenTimerStopped = Time.time;
            if (isVisible == true) {
                if (fill.fillAmount <= 0) { 
                    radialCanvas.enabled = false;
                    isVisible = false;
                }
            }
        }
    }

    public void GetCurrentReloadFill() {
        currentRadial = cm.timeWhenLoaded - Time.time;
        float currentOffset = currentRadial - minRadial;
        float maximumOffset = maxRadial - minRadial;
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;
    }
}
