﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;

    public float width = 10f;
    public float heigth = 7f;
    public float speed = 5f;
    public float spawnDelay = 0.5f;
    private bool movienRight = true;
    float xmax;
    float xmin;
	void Start ()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        xmax = rightBoundary.x;
        xmin = leftBoundary.x;
        SpawnUntillFull();
        
	}
    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }
    void SpawnUntillFull()
    {
        Transform nextPosition = NextFreePosition();
        if (nextPosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, nextPosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = nextPosition;
        }
        if (NextFreePosition()) Invoke("SpawnUntillFull", spawnDelay);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, heigth,0));
    }
    void Update ()
    {

        if (movienRight) transform.position += Vector3.right * speed * Time.deltaTime;
        else transform.position += Vector3.left * speed * Time.deltaTime;

        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        if (leftEdgeOfFormation < xmin) movienRight = true;
        else if  (rightEdgeOfFormation > xmax) movienRight = false;

        if (EmptyFormation()) SpawnUntillFull();
    }
    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0) return childPositionGameObject;
        }
        return null;
    }
    bool EmptyFormation()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount> 0) return false;
        }
        return true;
    }
}
