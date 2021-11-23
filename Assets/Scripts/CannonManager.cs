using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour {

    //VFX
    public GameObject muzzleParticleGO;
    //TRANSFORM FOR MUZZLE PARTICLE
    public GameObject muzzleTransform;

    //CONTROLS
    public KeyCode fireKey;

    [SerializeField] private bool targetHasBeenHit;
    public GameObject targetedGO;
    public HealthPoints targetHpScript;
    public TargetManager targetingScript;

    public float loadTime = 2f;
    public float damageOutput = 33;
    public float rangeOfGuns = 80f;

    public float timeWhenLoaded;

    private void Start() {
        if (gameObject.tag == "EnemyShip") {
            targetingScript = gameObject.GetComponent<TargetManager>();
        }

        muzzleTransform = gameObject.transform.Find("Muzzle").gameObject;
    }

    void Update() {

        if (targetedGO != null) {
            if (Time.time >= timeWhenLoaded) {
                if (gameObject.tag == "EnemyShip") {
                    if (targetingScript.CloseEnoughToFire == true) {
                        if (targetedGO.GetComponentInParent<CannonCrew>().stationHasBeenWiped == false) {
                            FireCannon();
                        }
                    }
                } else {
                    if (GetComponentInParent<CannonCrew>().stationHasBeenWiped == false) {
                        FireCannon();
                    }
                }
            }
        }
    }

    public void FireCannon() {
        print(gameObject.name + "'FIRES!'");
        if (gameObject.tag == "PlayerCannon") {
            AudioFW.Play("sfx_CannonFire");
        } else {
            AudioFW.Play("sfx_ShipCannonFire");
        }
        //Spawn muzzle flashes
        if (gameObject.tag == "EnemyShip") {
            //make muzzletransform look at its target
            muzzleTransform.transform.LookAt(targetedGO.transform.position);
            GameObject newMuzzleEffect = Instantiate(muzzleParticleGO, muzzleTransform.transform);
            ParticleSystem ParticleSystemOfNewMuzzleEffect = newMuzzleEffect.gameObject.GetComponent<ParticleSystem>();
            ParticleSystemOfNewMuzzleEffect.Stop();
            var MainOfNewMuzzleEffect = ParticleSystemOfNewMuzzleEffect.main;
            MainOfNewMuzzleEffect.duration = loadTime;
            ParticleSystemOfNewMuzzleEffect.Play();
            Destroy(newMuzzleEffect, loadTime);

        } else { //if tag is PlayerCannon
            GameObject newMuzzleEffect = Instantiate(muzzleParticleGO, muzzleTransform.transform);
            ParticleSystem ParticleSystemOfNewMuzzleEffect = newMuzzleEffect.gameObject.GetComponent<ParticleSystem>();
            ParticleSystemOfNewMuzzleEffect.Stop();
            var MainOfNewMuzzleEffect = ParticleSystemOfNewMuzzleEffect.main;
            MainOfNewMuzzleEffect.duration = loadTime;
            ParticleSystemOfNewMuzzleEffect.Play();
            Destroy(newMuzzleEffect, loadTime);
        }

        //RANDOMIZE HIT OR NO HIT
        if (gameObject.tag == "EnemyShip") {
            targetHasBeenHit = (Random.value > (targetingScript.distanceToTarget / 100));
            print(gameObject.name + targetingScript.distanceToTarget / 100);
        } else {
            targetHasBeenHit = (Random.value > 0.5f);
        }

        if (targetHasBeenHit == true) {
            Debug.Log(gameObject.name + " HAS HIT ITS ENEMY!");
            targetHpScript.TakeDamage(damageOutput, loadTime);
        } else {
            Debug.Log(gameObject.name + " MISSED!");
        }

        timeWhenLoaded = Time.time + loadTime;
    }


    public void AssignNewTargetHPScript() {
        targetHpScript = targetedGO.GetComponent<HealthPoints>();
        print(gameObject.name + " HAS SIGHTED AN ENEMY!");
    }
}


