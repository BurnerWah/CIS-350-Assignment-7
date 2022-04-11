/*
 * Jaden Pleasants
 * Assignment 7
 * Waits for the player to press sapce to start the game
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroText : MonoBehaviour {
    public GameObject waveCounter;
    void Start() {
        Time.timeScale = 0f;
        waveCounter.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Time.timeScale = 1f;
            waveCounter.SetActive(true);
            StartCoroutine(FindObjectOfType<PlayerControllerX>().FixIntroBoost());
            gameObject.SetActive(false);
        }
    }
}
