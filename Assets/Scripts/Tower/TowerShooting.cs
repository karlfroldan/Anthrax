using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooting : MonoBehaviour
{
    // Start is called before the first frame update
    public float colliderRadius;
    public string parentName;

    private Actor shooterActor;
    private CircleCollider2D collider;

    public GameObject currentTarget;

    // prevent the tower from changing targets while the target is locked
    public bool isTargetLocked;

    void Start()
    {
        gameObject.AddComponent<Actor>();
        shooterActor = gameObject.GetComponent<Actor>();
        shooterActor.team = 1; // team 1 means player
        shooterActor.attackRate = 3.2f; // doctor is the strongest player
        
        shooterActor.hitpoints = 0.8f; // but low damage since they mostly just write incomprehensible stuff
        isTargetLocked = false;

        // get the file of the projectile sprite
        shooterActor.projectile = GameObject.Find(parentName).GetComponent<TowerProjectileSetter>().projectile;
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
}
