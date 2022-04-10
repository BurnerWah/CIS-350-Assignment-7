/*
 * Jaden Pleasants
 * Assignment 7
 * Moves the player among other things
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody playerRb;
    public float speed;

    private float forwardInput;
    private GameObject focalPoint;

    public bool hasPowerup;

    private static readonly float powerupStrength = 15;
    public GameObject powerupIndicator;

    void Start() {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.FindGameObjectWithTag("FocalPoint");
    }

    void Update() {
        if (transform.position.y < -10) {
            SpawnManager.EarlyGameLoss();
            Destroy(gameObject);
        } else {
            forwardInput = Input.GetAxis("Vertical");
            powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        }
    }

    void FixedUpdate() {
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PowerUp")) {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    private IEnumerator PowerupCountdownRoutine() {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy") && hasPowerup) {
            Debug.Log($"Player collided with: {other.gameObject.name} with powerup set to {hasPowerup}");
            var enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            var awayFromPlayer = (other.gameObject.transform.position - transform.position).normalized;
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
}
