using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverChecker : MonoBehaviour {
    public GameObject[] PCannons;
    public float totalHealth;

    public GameManager gM;

    private void Start() {
        gM = GetComponent<GameManager>();
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
        gM.PCannonAmountChanged();
    }
    public void GameOver() {
        print("game over! total score:" + (GameObject.Find("GameManager").GetComponent<GameManager>().totalScore));
        //Time.timeScale = 0;
        SceneManager.LoadScene("GameOver");
    }
}
