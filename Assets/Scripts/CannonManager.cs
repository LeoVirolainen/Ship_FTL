using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour {

    //VFX
    public GameObject muzzleParticle;

    [SerializeField] private bool targetHasBeenHit;
    public GameObject cannonTarget;
    public HealthPoints hpScript;
    public float loadTime = 2f;

    private float timeWhenLoaded;

    void Start() {
        hpScript = cannonTarget.GetComponent<HealthPoints>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            if (Time.time >= timeWhenLoaded) {
                FireCannon();
            }else {
                print("'STILL LOADING!!'");
            }
        }
    }

    void FireCannon() {
        print("'FIRE!'");
        muzzleParticle.SetActive(true);
        StartCoroutine("ParticleControl");
        targetHasBeenHit = (Random.value > 0.5f);

        if (targetHasBeenHit == true) {
            Debug.Log("splinters fly as the cannonball pierces the hull of the vessel!");
            hpScript.TakeDamage(10);
        } else {
            Debug.Log("water splashes around the ship as your valuable ammunition sinks beneath the waves...");
        }

        timeWhenLoaded = Time.time + loadTime;
    }

    IEnumerator ParticleControl() {
        yield return new WaitForSeconds(loadTime);
        muzzleParticle.SetActive(false);
    }
}
