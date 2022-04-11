/*
 * Jaden Pleasants
 * Assignment 7
 * Handles player behaviour
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour {
    private Rigidbody playerRb;
    private readonly float speed = 500;
    private GameObject focalPoint;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;

    // how hard to hit enemy without powerup
    private readonly float normalStrength = 10;
    // how hard to hit enemy with powerup
    private readonly float powerupStrength = 25;

    private GameObject smokeParticle;
    private bool allowBoost = false;

    void Start() {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        smokeParticle = GameObject.Find("/Player/Focal Point/Smoke_Particle");
        smokeParticle.SetActive(false);
    }

    void Update() {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);

        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

        if (Input.GetKeyDown(KeyCode.Space) && allowBoost) {
            playerRb.AddForce(focalPoint.transform.forward * speed, ForceMode.Impulse);
            smokeParticle.SetActive(true);
            allowBoost = false;
            StartCoroutine(StopSmoke());
        }
    }

    private IEnumerator StopSmoke() {
        yield return new WaitForSeconds(3f);
        smokeParticle.SetActive(false);
        allowBoost = true;
    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Powerup")) {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown() {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * (hasPowerup ? powerupStrength : normalStrength), ForceMode.Impulse);
        }
    }

    public IEnumerator FixIntroBoost() {
        yield return new WaitForSeconds(1f);
        allowBoost = true;
    }
}
