using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour {

    //VFX
    public GameObject muzzleParticleGO;

    //HAS TO BE CHILD OBJECT WITH NAME 'MUZZLE'
    public GameObject muzzleTransform;

    //CONTROLS
    public KeyCode fireKey;

    [SerializeField] private bool targetHasBeenHit;
    public GameObject targetedGO;
    public HealthPoints targetHpScript;

    public float loadTime = 2f;
    public int damageOutput = 10;

    private float timeWhenLoaded;

    void Update() {

        if (Input.GetKeyDown(fireKey)) {
            if (targetedGO != null) {
                if (Time.time >= timeWhenLoaded) {
                    FireCannon();
                } else {
                    print(gameObject.name + " IS STILL LOADING!");
                }
            } else {
                print(gameObject.name + "'S TARGET UNCLEAR");
            }
        }
    }

    void FireCannon() {
        print("'FIRE!'");
        if (gameObject.tag == "PlayerCannon") {
            AudioFW.Play("sfx_CannonFire");
        } else {
            AudioFW.Play("sfx_ShipCannonFire");
        }
        GameObject newMuzzleEffect = Instantiate(muzzleParticleGO, muzzleTransform.transform);
        ParticleSystem ParticleSystemOfNewMuzzleEffect = newMuzzleEffect.gameObject.GetComponent<ParticleSystem>();
        ParticleSystemOfNewMuzzleEffect.Stop();
        var MainOfNewMuzzleEffect = ParticleSystemOfNewMuzzleEffect.main;
        MainOfNewMuzzleEffect.duration = loadTime;
        ParticleSystemOfNewMuzzleEffect.Play();
        Destroy(newMuzzleEffect, loadTime);

        //RANDOMIZE HIT OR NO HIT
        targetHasBeenHit = (Random.value > 0.5f);

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

