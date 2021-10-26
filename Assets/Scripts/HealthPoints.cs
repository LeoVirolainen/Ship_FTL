using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoints : MonoBehaviour {

    public GameObject takeHitParticle;

    [SerializeField] private int hP;


    public void TakeDamage(int damage) {
        takeHitParticle.SetActive(true);
        StartCoroutine("ParticleControl");
        hP = hP - 10;
        if (hP <= 0) {
            Destroy(gameObject);
        }
    }

    IEnumerator ParticleControl() {
        yield return new WaitForSeconds(2);
        takeHitParticle.SetActive(false);
    }
}
