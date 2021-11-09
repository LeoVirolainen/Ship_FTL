using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoints : MonoBehaviour {

    public int shipValue; //how much gp the player gets when destroyed
    public GameManager gM;

    public GameObject takeHitParticle;
    public float impactParticleDuration;

    public int hP;

    private void Start() {
        gM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
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
            if (tag == "EnemyShip") {
                gM.goldPieces = gM.goldPieces + shipValue;
            }
            Destroy(gameObject, 1f);
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
