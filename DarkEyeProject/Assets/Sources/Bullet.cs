using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField]
    private TrailRenderer trail;
    [SerializeField]
    private float lifeTime = 1f;
    [SerializeField]
    private float speed = 50f;

    private float timer = 0;
    private Vector3 direction;

    public void Fire(Vector3 position, Vector3 fireDirection) {
        transform.position = position;
        direction = fireDirection;
        timer = lifeTime;

    }

    void Start() { }

    void Update() {
        timer -= Time.deltaTime;
        if (timer < 0) {
            Destroy();
        }
        var ray = new Ray(transform.position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, speed * 5f * Time.deltaTime)) {
            transform.position = hit.point;//  + hit.normal * 0.05f;
            direction = Vector3.Reflect(direction, hit.normal);
            trail.AddPosition(transform.position);
            TryHit(hit.collider.GetComponent<Enemy>());
        }
        // Second trace
        ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out hit, speed * 5f * Time.deltaTime)) {
            transform.position = hit.point;//  + hit.normal * 0.05f;
            direction = Vector3.Reflect(direction, hit.normal);
            trail.AddPosition(transform.position);
            TryHit(hit.collider.GetComponent<Enemy>());
        }
        transform.position += direction * speed * Time.deltaTime;
    }

    private void TryHit(Enemy enemy) {
        if (enemy == null) {
            return;
        }
        enemy.Hit();
    }

    private void Destroy() {
        enabled = false;
        Destroy(gameObject, 0.1f);
    }
}
