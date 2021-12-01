using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreFetcher : MonoBehaviour {

    public ScoreDDOL scoreScript;

    void Start() {
        scoreScript = GameObject.Find("ScoreKeeper").GetComponent<ScoreDDOL>();
        GetComponentInChildren<TextMeshProUGUI>().text = ("Game Over! Score: " + scoreScript.score);
    }

    // Update is called once per frame
    void Update() {

    }
}
