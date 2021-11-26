using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTutorialButton : MonoBehaviour {
    public GameObject tutorial;

    public void CloseTutorial() {
        tutorial.SetActive(false);
    }

}
