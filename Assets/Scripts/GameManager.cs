using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public int goldPieces;
    public int totalScore;
    public Text gpText;
    public Text tsText;

    public float PCannonHealth;
    public float difficultyChangeInterval = 20f;
    public bool waitingToRaiseDifficulty = false;

    public ShipSpawner spawnerScript;
    public GameOverChecker gameOverScript;

    public float shipSpawnRate;
    public float defaultShipSpawnRate = 20;
    public float maxSpawnRate = 1;
    public float spawnRateDecayPerDifficultyRaise = 0.3f;
    public int defaultMaxShips = 2;
    public int maxShips;


    void Start() {
        spawnerScript = GameObject.Find("ShipSpawner").GetComponent<ShipSpawner>();
        gameOverScript = GetComponent<GameOverChecker>();
        InvokeRepeating("CheckScore", 0.0f, 1f); //add 1 to score every second
    }

    public void CheckScore() {
        totalScore = totalScore + 1;
    }

    void FixedUpdate() {
        gpText.text = "Rubles: " + goldPieces;
        tsText.text = "SCORE: " + totalScore.ToString("0");

        if (waitingToRaiseDifficulty == false) {
            StartCoroutine("RaiseDifficulty");
        }
    }

    public void PCannonAmountChanged() {
        PCannonHealth = gameOverScript.totalHealth / 100;
    }

    IEnumerator RaiseDifficulty() {
        waitingToRaiseDifficulty = true;
        maxShips = defaultMaxShips + (int)PCannonHealth;

        shipSpawnRate = (defaultShipSpawnRate - (PCannonHealth * PCannonHealth));
        if (shipSpawnRate > maxSpawnRate) {
            shipSpawnRate -= spawnRateDecayPerDifficultyRaise;
            spawnRateDecayPerDifficultyRaise += 0.2f;
        }

        yield return new WaitForSeconds(difficultyChangeInterval);
        waitingToRaiseDifficulty = false;
    }
}
