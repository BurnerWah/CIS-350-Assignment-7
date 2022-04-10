/*
 * Jaden Pleasants
 * Assignment 7
 * Moves the player
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody playerRb;
    public float speed;

    private float forwardInput;
    private GameObject focalPoint;

    void Start() {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.FindGameObjectWithTag("FocalPoint");
    }

    void Update() {
        forwardInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate() {
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }
}
