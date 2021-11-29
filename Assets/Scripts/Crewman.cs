using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crewman : MonoBehaviour {
    public Animator animator;
    public int animToPlay = 0;

    public bool playingAnim = true;

    private void Start() {
        animator = GetComponent<Animator>();
        StartCoroutine("RandomizeStartWait");
    }

    private void FixedUpdate() {
        if (playingAnim == false) {
            StartCoroutine("PlayNextIdle");
        }
    }

    IEnumerator PlayNextIdle() {
        playingAnim = true;
        animToPlay = Random.Range(0, 3);
        animator.Play("Crew_Idle_" + animToPlay);
        yield return new WaitForSeconds(5);
        playingAnim = false;
    }

    IEnumerator RandomizeStartWait() {
        yield return new WaitForSeconds(Random.Range(0f, 4.5f));
        playingAnim = false;
    }
}
