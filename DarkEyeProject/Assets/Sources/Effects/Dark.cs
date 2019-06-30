using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dark : MonoBehaviour {
    [SerializeField]
    private Transform darkSpherePrefab;
    private float timer = 0;
    private float darkTimer = 0;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        darkTimer += Time.deltaTime * 0.5f;
        timer -= Time.deltaTime;
        if (timer < 0) {
            SpawnDark(new Vector3(Random.Range(-4f, 4), Random.Range(-0.3f, 0.2f), -darkTimer + 10));
        }
    }

    private void SpawnDark(Vector3 vector3) {
        var dark = Instantiate(darkSpherePrefab);
        dark.transform.SetParent(transform);
        dark.transform.position = vector3;
    }
}
