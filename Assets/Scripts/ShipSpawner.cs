using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour {
    public GameObject shipToBeSpawned;
    public GameObject shipSpawnPoint;
    public GameObject[] shipsInScene;

    public GameObject[] shipOptions;
    public int spawnableShipTypesLimit = 2;
    
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
        int shipSelectorInt = Random.Range(0, spawnableShipTypesLimit);
        shipToBeSpawned = shipOptions[shipSelectorInt];

        Instantiate(shipToBeSpawned, shipSpawnPoint.transform);

        if (gM.hardModeTriggered == true) {
            int shipSelectorIntForSecondSpawn = Random.Range(0, spawnableShipTypesLimit);
            shipToBeSpawned = shipOptions[shipSelectorIntForSecondSpawn];
            Instantiate(shipToBeSpawned, shipSpawnPoint.transform);
        }
        yield return new WaitForSeconds(gM.shipSpawnRate);
        firstShipSpawned = true;
        waitingForNextShipToSpawn = false;
    }

    public void SpawnExtraShip() {
        int shipSelectorInt = Random.Range(0, spawnableShipTypesLimit);
        shipToBeSpawned = shipOptions[shipSelectorInt];
        Instantiate(shipToBeSpawned, shipSpawnPoint.transform);
    }

    public void EnterHardMode() {
        spawnableShipTypesLimit = 3;
    }
}
