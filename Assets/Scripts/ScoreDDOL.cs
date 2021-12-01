using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreDDOL : MonoBehaviour {
    public GameManager gameManager;
    public float score;

    void Awake() {
        if (SceneManager.GetActiveScene().name == ("DevScene")) {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            score = gameManager.totalScore;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        if (SceneManager.GetActiveScene().name == ("DevScene")) {
            score = gameManager.totalScore;
        }
    }
}
