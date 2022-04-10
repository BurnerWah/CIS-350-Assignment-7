/*
 * Jaden Pleasants
 * Assignment 7
 * Moves the enemy to the player
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    private Rigidbody enemyRb;
    public GameObject player;
    public float speed;

    void Start() {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        if (transform.position.y < -10) {
            Destroy(gameObject);
        }
    }

    void FixedUpdate() {
        var lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
    }
}
