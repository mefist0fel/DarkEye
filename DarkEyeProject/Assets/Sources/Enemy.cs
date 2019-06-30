using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    private DeathAnimation deathAnimation = null;
    [SerializeField]
    private float waveSpeed = 2f;
    [SerializeField]
    private Vector3 WaveVector = Vector3.up * 0.1f;

    public int Count { get; private set; }

    float waveTimer = 0;
    Vector3 startPosition;

    private void Start() {
        startPosition = transform.position;
        Count += 1;
    }

    private void OnDestroy() {
        Count -= 1;
    }

    void Update() {
        MoveWave();
    }

    private void MoveWave() {
        waveTimer += Time.deltaTime / waveSpeed * Mathf.PI;
        transform.position = startPosition + WaveVector * Mathf.Sin(waveTimer);
    }

    internal void Hit() {
        enabled = false;
        deathAnimation.enabled = true;
    }
}
