
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthPoints : MonoBehaviour {

    public int shipValue; //how much gp the player gets when destroyed
    public GameManager gM;

    public CannonManager cM;

    public CannonCrew cC;

    public PCannonUIHandler cUIh;

    public GameObject takeHitParticle;

    public Animator anim;
    public string nameOfSinkAnim;

    public float impactParticleDuration;
    public float currentHp = 100;
    public float maxHp;
    public float LoadTimeMultiplier = 40;

    public bool goldHasBeenGiven = false;
    public bool isSinking;

    private void Start() {
        gM = GameObject.Find("GameManager").GetComponent<GameManager>();
        cM = gameObject.GetComponent<CannonManager>();
        maxHp = currentHp;
        anim = GetComponent<Animator>();

        if (tag == "PlayerCannon") {
            cC = GetComponentInParent<CannonCrew>();
            cUIh = GetComponentInChildren<PCannonUIHandler>();
            cC.WipeAndStart();
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
            cUIh.maxRadial = cM.loadTime;
            //tell CannonCrew script that we got hit
            cC.CrewDamaged();
        }
        if (currentHp <= 0) {
            if (tag == "EnemyShip") { //if this is a ship, give player GP and destroy this
                StartCoroutine ("ShipSink");                
            }
        }
    }

    public IEnumerator ShipSink() {
        if (goldHasBeenGiven == false) {
            StartCoroutine("PlayMoneySFX");
            gM.goldPieces = gM.goldPieces + shipValue;
            gM.totalScore = gM.totalScore + shipValue;
            goldHasBeenGiven = true;
        }       
        GetComponent<CannonManager>().loadTime = 99;
        
        isSinking = true;
        Destroy(gameObject, 5f);
        yield return new WaitForSeconds(0.7f); //emission -> time value of spawned particle effect in TakeDamage + 0.1f
        Destroy(GetComponent<ShipMover>().newDestinationObject);
        GetComponent<NavMeshAgent>().speed = 0;
        AudioFW.Play("sfx_ShipSink");
        anim.Play(nameOfSinkAnim);
    }

    private IEnumerator PlayMoneySFX() {
        yield return new WaitForSeconds(2f);
        AudioFW.Play("sfx_GetMoney");
    }

    private IEnumerator PlayImpactSFX() {
        yield return new WaitForSeconds(0.6f);
        if (gameObject.tag == "PlayerCannon") {
            AudioFW.Play("sfx_FortImpact");
            //shake the camera
            CameraShake.Shake(0.2f, 0.4f);
        } else {
            AudioFW.Play("sfx_ShipImpact");
        }
    }
}
