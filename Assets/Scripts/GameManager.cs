using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public int goldPieces;
    public Text gpText;

    public float difficultyChangeInterval = 20f;
    public bool waitingToRaiseDifficulty = false;

    public ShipSpawner spawnerScript;
    public float fastestSpawnRate = 1f;

    public int absoluteMaxShipsAtHardest = 6;


    void Start() {
        spawnerScript = GameObject.Find("ShipSpawner").GetComponent<ShipSpawner>();
    }
    void Update() {
        gpText.text = ("Rubles: " + goldPieces);

        if (waitingToRaiseDifficulty == false) {
            StartCoroutine("RaiseDifficulty");
        }
    }

    IEnumerator RaiseDifficulty() {
        waitingToRaiseDifficulty = true;
        if (spawnerScript.maxShips < absoluteMaxShipsAtHardest) {
            spawnerScript.maxShips = spawnerScript.maxShips + 1;
        }
        if (spawnerScript.shipSpawnRate >= fastestSpawnRate)
            spawnerScript.shipSpawnRate = spawnerScript.shipSpawnRate - 0.3f;
        yield return new WaitForSeconds(difficultyChangeInterval);
        waitingToRaiseDifficulty = false;
    }
}
