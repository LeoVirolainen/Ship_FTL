using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour {
    public GameObject shipToBeSpawned;
    public GameObject shipSpawnPoint;
    public GameObject[] shipsInScene;

    public GameObject[] shipOptions;

    public float shipSpawnRate = 5f;

    public int maxShips;
    public bool waitingForNextShipToSpawn = false;

    private void Update() {
        shipsInScene = GameObject.FindGameObjectsWithTag("EnemyShip");
        if (shipsInScene.Length < maxShips) {
            if (waitingForNextShipToSpawn == false) {
                StartCoroutine("SpawnShip");
            }
        }
    }
    IEnumerator SpawnShip() {
        waitingForNextShipToSpawn = true;

        //choose random ship
        int shipSelectorInt = Random.Range(0, 3);
        shipToBeSpawned = shipOptions[shipSelectorInt];

        Instantiate(shipToBeSpawned, shipSpawnPoint.transform);
        yield return new WaitForSeconds(shipSpawnRate);
        waitingForNextShipToSpawn = false;
    }
}
