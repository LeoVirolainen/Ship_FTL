using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoints : MonoBehaviour {

    public int shipValue; //how much gp the player gets when destroyed
    public GameManager gM;

    public CannonManager cM;

    public GameObject takeHitParticle;
    public float impactParticleDuration;

    public float currentHp = 100;
    public float maxHp;
    public float LoadTimeMultiplier = 40;

    private void Start() {
        gM = GameObject.Find("GameManager").GetComponent<GameManager>();
        cM = gameObject.GetComponent<CannonManager>();
        maxHp = currentHp;
    }

    public void TakeDamage(float damage, float reloadTime) {

        StartCoroutine("PlayImpactSFX");

        GameObject newImpactEffect = Instantiate(takeHitParticle, gameObject.transform);
        ParticleSystem ParticleSystemofNewImpactEffect = newImpactEffect.gameObject.GetComponent<ParticleSystem>();
        ParticleSystemofNewImpactEffect.Stop();
        var MainOfNewImpactEffect = ParticleSystemofNewImpactEffect.main;
        MainOfNewImpactEffect.duration = reloadTime;
        ParticleSystemofNewImpactEffect.Play();
        Destroy(newImpactEffect, impactParticleDuration);

        currentHp = currentHp - damage;
        //below: make PCannons' loadtime dependent on remaining HP
        if (tag == "PlayerCannon") {
            //below we get a number to add to load time when HP decreases.
            //LoadTimeMultiplier: something in the tens, bigger number = less punishing
            cM.loadTime = cM.loadTime + ((maxHp - currentHp) / LoadTimeMultiplier);
        }
        if (currentHp <= 0) {
            if (tag == "EnemyShip") { //if this is a ship, give player GP when this dies
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
