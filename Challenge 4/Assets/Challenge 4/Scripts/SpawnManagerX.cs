/*
 * Jaden Pleasants
 * Assignment 7
 * Handles spawning stuff
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManagerX : MonoBehaviour {
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private readonly float spawnRangeX = 10;
    // set min spawn Z
    private readonly float spawnZMin = 15;
    // set max spawn Z
    private readonly float spawnZMax = 25;

    public int enemyCount;
    // NOTE - this actually stores the next wave count, not the current one.
    public int waveCount = 1;

    public GameObject player;

    public static float EXTRA_SPEED = 0;
    private int enemyGoals = 0;
    private bool hasGameEnded = false;
    public Text endText;
    public Text waveText;

    // Update is called once per frame
    void Update() {
        if (hasGameEnded)
            return;
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0) {
            SpawnEnemyWave(waveCount);
        }
    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition() {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }


    void SpawnEnemyWave(int enemiesToSpawn) {
        if (waveCount >= 10) {
            EndGame(true);
            return;
        }

        // make powerups spawn at player end
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15);

        // If no powerups remain, spawn a powerup
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0) {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < enemiesToSpawn; i++) {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }

        // extra speed should just be the natural log of the current wave.
        EXTRA_SPEED = Mathf.Log(waveCount);

        enemyGoals = 0;

        waveText.text = $"Wave: {waveCount++}";

        // put player back at start
        ResetPlayerPosition();
    }

    // Move player back to position in front of own goal
    void ResetPlayerPosition() {
        player.transform.position = new Vector3(0, 1, -7);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    void EndGame(bool didWin) {
        hasGameEnded = true;
        endText.text = $"You {(didWin ? "win" : "lose")}!\nPress R to restart";
        endText.gameObject.SetActive(true);
    }

    private void HandleEnemyGoal() {
        if (++enemyGoals >= waveCount - 1) {
            EndGame(false);
        }
    }

    public static void EnemyGoal() {
        FindObjectOfType<SpawnManagerX>().HandleEnemyGoal();
    }
}
