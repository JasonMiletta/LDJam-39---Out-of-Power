﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemySpawner : MonoBehaviour {

    public float spawnRate = 5.0f;
    public float spawnRadius = 10.0f;
    public float spawnCapacity = 2;
    public GameObject enemyPrefab;

    private float currentSpawnTime;

	// Use this for initialization
	void Start () {
        currentSpawnTime = spawnRate;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (enemyPrefab != null)
        {
            if(currentSpawnTime < spawnRate)
            {
                currentSpawnTime += Time.deltaTime;
            } else
            {
                currentSpawnTime = 0;
                spawnRate = spawnRate * 0.9f;

                for(var i = spawnCapacity; i > 0; --i)
                {
                    spawnEnemy();
                }
            }
        }
    }
   
    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    private void spawnEnemy()
    {
        Vector3 spawnLocation = getRandomPointOnCircle(transform.position, spawnRadius);
        Instantiate(enemyPrefab, spawnLocation, transform.rotation);
    }

    private Vector3 getRandomPointOnCircle(Vector3 center, float radius)
    {
        // create random angle between 0 to 360 degrees 
        var ang = Random.value * 360; 
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        return pos; 
    }
}
