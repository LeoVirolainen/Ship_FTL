using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour {
    public GameObject shipToBeSpawned;
    public GameObject shipSpawnPoint;
    public GameObject[] shipsInScene;

    public GameObject[] shipOptions;

    public GameManager gM;

    public bool waitingForNextShipToSpawn = false;

    public bool firstShipSpawned = false;

    private void Start() {
        gM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update() {
        shipsInScene = GameObject.FindGameObjectsWithTag("EnemyShip");
        if (shipsInScene.Length < gM.maxShips) {
            if (waitingForNextShipToSpawn == false) {
                StartCoroutine("SpawnShip");
            }
        }
        if (shipsInScene.Length == 0 && firstShipSpawned == true) {
            SpawnExtraShip();
        }
    }

    IEnumerator SpawnShip() {
        waitingForNextShipToSpawn = true;

        //choose random ship
        int shipSelectorInt = Random.Range(0, 3);
        shipToBeSpawned = shipOptions[shipSelectorInt];

        Instantiate(shipToBeSpawned, shipSpawnPoint.transform);
        yield return new WaitForSeconds(gM.shipSpawnRate);
        firstShipSpawned = true;
        waitingForNextShipToSpawn = false;
    }

    public void SpawnExtraShip() {
        int shipSelectorInt = Random.Range(0, 3);
        shipToBeSpawned = shipOptions[shipSelectorInt];
        Instantiate(shipToBeSpawned, shipSpawnPoint.transform);
    }
}
