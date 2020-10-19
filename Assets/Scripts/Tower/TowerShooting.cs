﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooting : MonoBehaviour
{
    // Start is called before the first frame update
    public float colliderRadius;
    public string parentName;
    public GameObject bulletPrefab;

    private Actor shooterActor;
    private CircleCollider2D collider;
    private Vector3 startPosition;

    public GameObject currentTarget;

    public bool isShooting = false;

    void Start()
    {
        GameObject parent = GameObject.Find(parentName);
        gameObject.AddComponent<Actor>();
        shooterActor = gameObject.GetComponent<Actor>();
        shooterActor.team = 1; // team 1 means player

        // Set projectile properties
        TowerProjectileSetter tpSetter = parent.GetComponent<TowerProjectileSetter>();
        bulletPrefab = tpSetter.bulletPrefab;
        shooterActor.attackRate = tpSetter.attackRate;
        shooterActor.hitpoints = tpSetter.hitpoints;

        startPosition = gameObject.transform.position;
    }

    public void SetColliderRadius(float r)
    {
        gameObject.AddComponent<CircleCollider2D>();
        collider = gameObject.GetComponent<CircleCollider2D>();
        collider.radius = r;
        collider.isTrigger = true;

        Debug.Log("Parent Name is: " + parentName);
    }

    public void SetPosition(Vector3 pos)
    {
        gameObject.transform.position = pos;
    }

    public void ShootProjectile()
    {
        GameObject bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Transform targetTransform = currentTarget.transform;

        // shootDir is the direction of the enemy
        Vector3 shootDir = (targetTransform.position - bulletObject.transform.position).normalized;

        bulletObject.GetComponent<Bullet>().Setup(shootDir);
    }
}