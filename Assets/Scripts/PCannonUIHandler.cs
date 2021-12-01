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

    public GameObject vfxForNoTargetAlert;
    public bool targetAlertHasBeenInstantiated;

    public CannonManager cm;

    void Start() {        
        radialCanvas = GetComponentInChildren<Canvas>();
        cm = GetComponentInParent<CannonCrew>().gameObject.GetComponentInChildren<CannonManager>();
    }

    void Update() {
        GetCurrentReloadFill();

        if (cm.targetedGO != null) {
            if (cm.targetedGO.GetComponent<HealthPoints>().isSinking == false) {
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
    }

    public void GetCurrentReloadFill() {
        maxRadial = GetComponentInParent<CannonCrew>().gameObject.GetComponentInChildren<CannonManager>().loadTime;
        currentRadial = cm.timeWhenLoaded - Time.time;
        float currentOffset = currentRadial - minRadial;
        float maximumOffset = maxRadial - minRadial;
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;
    }

    public IEnumerator NoTargetAlert() {
        if (targetAlertHasBeenInstantiated == false) {
            if (GetComponentInParent<CannonCrew>().stationHasBeenWiped == false) {                
                vfxForNoTargetAlert.SetActive(true);
                targetAlertHasBeenInstantiated = true;
                yield return new WaitForSeconds(2);
                vfxForNoTargetAlert.SetActive(false);
            }
        }
    }
}
