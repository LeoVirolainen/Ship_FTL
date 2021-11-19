using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoints : MonoBehaviour {

    public int shipValue; //how much gp the player gets when destroyed
    public GameManager gM;

    public CannonManager cM;

    public CannonCrew cC;

    public GameObject takeHitParticle;
    public float impactParticleDuration;

    public float currentHp = 100;
    public float maxHp;
    public float LoadTimeMultiplier = 40;

    public bool goldHasBeenGiven = false;

    private void Start() {
        gM = GameObject.Find("GameManager").GetComponent<GameManager>();
        cM = gameObject.GetComponent<CannonManager>();
        maxHp = currentHp;

        if (tag == "PlayerCannon") {
            cC = GetComponentInParent<CannonCrew>();
        } else {
            cC = null;
        }
    }

    public void TakeDamage(float damage, float reloadTime) {
        //if (GetComponent<CannonManager>().targetedGO != null && GetComponent<CannonManager>().targetedGO.activeSelf == true) {
            StartCoroutine("PlayImpactSFX");
        //}

        GameObject newImpactEffect = Instantiate(takeHitParticle, gameObject.transform);
        ParticleSystem ParticleSystemofNewImpactEffect = newImpactEffect.gameObject.GetComponent<ParticleSystem>();
        ParticleSystemofNewImpactEffect.Stop();
        var MainOfNewImpactEffect = ParticleSystemofNewImpactEffect.main;
        MainOfNewImpactEffect.duration = reloadTime;
        ParticleSystemofNewImpactEffect.Play();
        Destroy(newImpactEffect, impactParticleDuration);

        if (currentHp > 0) {
            currentHp = currentHp - damage;
        } else if (currentHp < 0) {
            currentHp = 0;
        }
        //below: make PCannons' loadtime dependent on remaining HP
        if (tag == "PlayerCannon") {
            //below we get a number to add to load time when HP decreases.
            //LoadTimeMultiplier: something in the tens, bigger number = less punishing
            cM.loadTime = cM.loadTime + ((maxHp - currentHp) / LoadTimeMultiplier);
            //tell CannonCrew script that we got hit
            cC.CrewDamaged();
        }
        if (currentHp <= 0) {
            if (tag == "EnemyShip") { //if this is a ship, give player GP and destroy this
                if (goldHasBeenGiven == false) {
                    gM.goldPieces = gM.goldPieces + shipValue;
                    goldHasBeenGiven = true;
                }
                Destroy(GetComponent<ShipMover>().newDestinationObject);
                Destroy(gameObject, 1f);
            }
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
