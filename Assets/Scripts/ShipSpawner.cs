using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour {
    public GameObject shipToBeSpawned;
    public GameObject shipSpawnPoint;
    public GameObject[] shipsInScene;

    public float difficultyChangeInterval = 20f;
    public bool waitingToRaiseDifficulty = false;

    public int maxShips;
    public bool waitingForNextShipToSpawn = false;

    private void Update() {
        shipsInScene = GameObject.FindGameObjectsWithTag("EnemyShip");
        if (shipsInScene.Length < maxShips) {
            if (waitingForNextShipToSpawn == false) {
                StartCoroutine("SpawnShip");
            }
        }
        if (waitingToRaiseDifficulty == false) {
            StartCoroutine("RaiseDifficulty");
        }
    }
    IEnumerator SpawnShip() {
        waitingForNextShipToSpawn = true;
        Instantiate(shipToBeSpawned, shipSpawnPoint.transform);
        yield return new WaitForSeconds(10f);
        waitingForNextShipToSpawn = false;
    }

    IEnumerator RaiseDifficulty() {
        waitingToRaiseDifficulty = true;
        maxShips = maxShips + 1;
        yield return new WaitForSeconds(difficultyChangeInterval);
        waitingToRaiseDifficulty = false;
    }
}
