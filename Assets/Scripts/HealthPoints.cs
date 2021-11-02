using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoints : MonoBehaviour {

    public GameObject takeHitParticle;
    public float impactParticleDuration;

    public int hP;


    public void TakeDamage(int damage, float reloadTime) {

        StartCoroutine("PlayImpactSFX");

        GameObject newImpactEffect = Instantiate(takeHitParticle, gameObject.transform);
        ParticleSystem ParticleSystemofNewImpactEffect = newImpactEffect.gameObject.GetComponent<ParticleSystem>();
        ParticleSystemofNewImpactEffect.Stop();
        var MainOfNewImpactEffect = ParticleSystemofNewImpactEffect.main;
        MainOfNewImpactEffect.duration = reloadTime;
        ParticleSystemofNewImpactEffect.Play();
        Destroy(newImpactEffect, impactParticleDuration);

        hP = hP - damage;
        if (hP <= 0) {
            Destroy(gameObject, 5f);
        }
    }

    private IEnumerator PlayImpactSFX() {
        yield return new WaitForSeconds(0.6f);
        if (gameObject.tag == "PlayerCannon") {
            AudioFW.Play("sfx_FortImpact");
        } else {
            AudioFW.Play("sfx_ShipImpact");
        }
    }
}
