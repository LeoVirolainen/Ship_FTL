using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipMover : MonoBehaviour {
    public bool hasDestination;
    public Vector3 destination;
    public NavMeshAgent myAgent;

    public GameObject destinationCol;
    public GameObject newDestinationObject;

    public HealthPoints myHp;
    public float myHpAtStart;

    void Start() {
        myAgent = GetComponent<NavMeshAgent>();

        myHp = gameObject.GetComponent<HealthPoints>();
        myHpAtStart = myHp.currentHp;
    }

    void Update() {
        if (hasDestination == false) {
            //Check if HP is high enough to go close or to keep one's distance
            if (myHp.currentHp > (myHpAtStart / 2)) {
                destination = new Vector3(Random.Range(60, -70), 0, Random.Range(0, -30));
            } else { destination = new Vector3(Random.Range(100, -100), 0, Random.Range(-40, -100)); }
            newDestinationObject = Instantiate(destinationCol, destination, /*this is just "no rotation" ->*/ Quaternion.identity);
            ShipDestination scriptOfDestObject = newDestinationObject.GetComponent<ShipDestination>();
            scriptOfDestObject.shipMoveScript = gameObject.GetComponent<ShipMover>();
            myAgent.SetDestination(destination);
            hasDestination = true;
        }
    }
}
