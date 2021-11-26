using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverChecker : MonoBehaviour {
    public GameObject[] PCannons;
    public float totalHealth;

    private void Start() {
        CheckPCannons();
    }

    public void CheckPCannons() {
        totalHealth = 0;
        PCannons = GameObject.FindGameObjectsWithTag("PlayerCannon");
        foreach (GameObject cannon in PCannons) {
            totalHealth = totalHealth + cannon.GetComponent<HealthPoints>().currentHp;
        }

        if (totalHealth <= 0) {
            GameOver();            
        }
    }
    public void GameOver() {
        print("game over! total score:" + (GameObject.Find("GameManager").GetComponent<GameManager>().totalScore));
        Time.timeScale = 0;
    }
}
