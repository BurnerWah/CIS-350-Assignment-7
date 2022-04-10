/*
 * Jaden Pleasants
 * Assignment 7
 * Spawns enemies at random locations, and handles most of the game's internal logic
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour {
    public GameObject enemyPrefab;
    private static readonly float spawnRange = 9;
    public int enemyCount;
    public int waveNumber;
    public GameObject powerupPrefab;

    public Text waveCounter;
    public Text winText;

    private bool hasGameEnded = false;

    void Start() {
        hasGameEnded = false;
        waveNumber = 1;
        SpawnEnemyWave(waveNumber);
        SpawnPowerup(1);
    }

    private void SpawnEnemyWave(int enemiesToSpawn) {
        for (int i = 0; i < enemiesToSpawn; i++) {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private void SpawnPowerup(int count) {
        for (int i = 0; i < count; i++) {
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }


    void Update() {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (!hasGameEnded && enemyCount == 0) {
            if (waveNumber++ <= 10) {
                SpawnEnemyWave(waveNumber);
                SpawnPowerup(1);
                waveCounter.text = $"Wave: {waveNumber}";
            } else {
                EndGame(true);
            }
        }
    }

    private Vector3 GenerateSpawnPosition() {
        var spawnPosX = Random.Range(-spawnRange, spawnRange);
        var spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    void EndGame(bool didWin) {
        hasGameEnded = true;
        winText.text = $"You {(didWin ? "win" : "lose")}!\nPress R to restart";
        winText.gameObject.SetActive(true);
    }

    public static void EarlyGameLoss() {
        GameObject.Find("/SpawnManager").GetComponent<SpawnManager>().EndGame(false);
    }
}
