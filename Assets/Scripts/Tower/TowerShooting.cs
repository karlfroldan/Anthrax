using System.Collections;
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
    private float timeToReachTarget;
    private float t;
    private Vector3 startPosition;
    private Vector3 endPosition;

    public GameObject currentTarget;

    // prevent the tower from changing targets while the target is locked
    public bool isTargetLocked;

    void Start()
    {
        GameObject parent = GameObject.Find(parentName);
        gameObject.AddComponent<Actor>();
        shooterActor = gameObject.GetComponent<Actor>();
        shooterActor.team = 1; // team 1 means player

        isTargetLocked = false;

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

    void Update()
    {
        t += Time.deltaTime / timeToReachTarget;
        transform.position = Vector3.Lerp(startPosition, endPosition, t);
    }

    public void ShootProjectile(Transform endTransform)
    {
        // shoot a projectile at the location
        Debug.Log("Shooting projectiles at: " + endTransform.position);
        //GameObject tempBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        //tempBullet.GetComponent<Bullet>().Init(Vector3.up);
    }
}
