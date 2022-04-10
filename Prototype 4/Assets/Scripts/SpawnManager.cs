/*
 * Jaden Pleasants
 * Assignment 7
 * Spawns enemies at random locations
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public GameObject enemyPrefab;
    private readonly float spawnRange = 9;

    void Start() {
        Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
    }

    void Update() {

    }

    private Vector3 GenerateSpawnPosition() {
        var spawnPosX = Random.Range(-spawnRange, spawnRange);
        var spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, 0, spawnPosZ);
    }
}
