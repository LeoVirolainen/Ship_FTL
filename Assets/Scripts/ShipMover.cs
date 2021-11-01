using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipMover : MonoBehaviour {
    public bool hasDestination;
    public Vector3 destination;
    public NavMeshAgent myAgent;

    public GameObject destinationCol;

    void Start() {
        myAgent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        if (hasDestination == false) {
            destination = new Vector3(Random.Range(100, -100), 0, Random.Range(0, -100));
            GameObject newDestinationObject = Instantiate(destinationCol, destination, /*this is just "no rotation" >*/ Quaternion.identity);
            ShipDestination scriptOfDestObject = newDestinationObject.GetComponent<ShipDestination>();
            scriptOfDestObject.shipMoveScript = gameObject.GetComponent<ShipMover>();
            myAgent.SetDestination(destination);
            hasDestination = true;
        }
    }
}
