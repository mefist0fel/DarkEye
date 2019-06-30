using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIDirector : MonoBehaviour
{
    [SerializeField]
    private Enemy enemyPrefab = null;
    [SerializeField]
    private Transform[] enemySpawnPoints = null;
    [SerializeField]
    private float spawnTime = 2f;
    [SerializeField]
    private float spawnCount = 1f;
    private float timer = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0) {
            timer = spawnTime;
            spawnCount += 0.2f;
            SpawnEnemies(Mathf.RoundToInt(spawnCount));
        }
    }

    private void SpawnEnemies(int count) {
        for (int i = 0; i < count; i++) {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy() {
        var randomPosition = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length - 1)].position;
        var enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity, transform);
    }

    [ContextMenu("Create Spawn Points")]
    void Generate() {
        if (enemySpawnPoints != null)
            foreach (var enemy in enemySpawnPoints)
                DestroyImmediate(enemy.gameObject);
        var list = new List<Transform>();
        for (float x = -3.5f; x < 4; x += 1) {
            for (float y = 0.5f; y < 3f; y += 1) {
                for (float z = -2f; z < 15; z += 1) {
                    var position = new Vector3(x, y, z) + Random.insideUnitSphere * 0.3f;
                    if (Physics.OverlapSphere(position, 0.5f).Length == 0)
                        list.Add(CreateSpawn(position));
                }
            }
        }
        enemySpawnPoints = list.ToArray();
    }

    private Transform CreateSpawn(Vector3 position) {
        var obj = new GameObject("enemy_spawn");
        obj.transform.SetParent(transform);
        obj.transform.position = position;
        return obj.transform;
    }

    private void OnDrawGizmos() {
        if (enemySpawnPoints == null) {
            return;
        }
        foreach (var item in enemySpawnPoints) {
            Gizmos.DrawWireSphere(item.position, 0.5f);
        }
    }
}
