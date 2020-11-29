using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/**
 * A script for things such as enemies, towers, characters that need protecting,
 * bosses, etc. This script will contain information as to what kind of enemy it is,
 * or what kind of tower it is, etc.
 */


public class Actor : MonoBehaviour
{
    /*
        Teams include:
            2- house
            1- player
            0- enemy
    */
    public int team;

    public HealthBar healthBar;
    public ShieldBar shieldBar;

    public Sprite projectile;

    public float shield = 0f;

    // max health is always at 100
    public float health = 100f; 

    // the attack rate
    public float attackRate = 1f;

    //hitpoints. How much a single hit will damage its target
    public float hitpoints = 2f;

    // the instances of targets the this actor will try to find
    public List<string> enemies;

    // we type this out. The actor will target every objects with an instance name
    // of this one.
    public string instanceName;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("UpdateEnemies", 0.2f, 0.2f);
    }

    void Update()
    {
        // use this for tower
        if(team == 1)
        {
            TowerShooting towerShooting = GetComponent<TowerShooting>();
            if(towerShooting.currentTarget != null && towerShooting.isShooting == false)
            {
                // if there is a target, we shoot
                // we start shooting. The Coroutine says will enable this to false again
                towerShooting.isShooting = true;
                // we finally shoot the projectile
                towerShooting.ShootProjectile();

                if(towerShooting.currentTarget.GetComponent<Actor>().health <= 0)
                    DestroyActor(towerShooting.currentTarget);

                StartCoroutine(WaitUntilShoot());
            }
        }
    }

    IEnumerator WaitUntilShoot()
    {
        yield return new WaitForSeconds(attackRate);
        GetComponent<TowerShooting>().isShooting = false;
    }

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D col)
    {
        float secondsLeft = Random.Range(0f, 1.8f);

        if(team == 1)
        {
            // if the current actor is a tower
            TowerShooting towerShooting = GetComponent<TowerShooting>();
            
            // If there is no current target, then we set this one as the target
            if(towerShooting.currentTarget == null && !towerShooting.isShooting)
            {
                // and get the target Actor
                towerShooting.currentTarget = col.gameObject;
            }
        }
        else if(team == 0)   // if it's an enemy team
        {
            if(enemies.Any(s => col.gameObject.name.Contains(s)))
            {
                StartCoroutine(EnemyWaiter(secondsLeft, col.gameObject));
            }
        }   
    }

    // when a target gets out of sight
    void OnTriggerExit2D(Collider2D col)
    {
        // this only works for the towers
        if(team == 1)
        {
            TowerShooting towerShooting = GetComponent<TowerShooting>();

            if(towerShooting.currentTarget == col.gameObject)
            {
                Debug.Log("No more target.");
                towerShooting.currentTarget = null;
            }
        }
    }

    // when there are targets inside the range of the tower
    void OnTriggerStay2D(Collider2D col)
    {
        if(team == 1)
        {
            TowerShooting towerShooting = GetComponent<TowerShooting>();
            // check if we currently have a target
            if(towerShooting.currentTarget == null && !towerShooting.isShooting)
            {
                // then we find a target.
                towerShooting.currentTarget = col.gameObject;
            }
        }
    }

    // a function that arbitrarily stops the enemy.
    IEnumerator EnemyWaiter(float s, GameObject target)
    {
        yield return new WaitForSeconds(s);
        // then halt the object 
        // this will only affect enemies
        gameObject.GetComponent<Pathfinding.AIPath>().canMove = false;

        Attack(target);
    }

    // attack of the enemy
    IEnumerator TeamEnemyAttack(GameObject target)
    {
        Actor targetActor = target.GetComponent<Actor>();

        while(targetActor.shield >= 0f && health >= 0)
        {
            // attack the target
            if(team == 0)
                StartCoroutine(EnemyAttack(target));
            targetActor.HitShield(hitpoints);
            targetActor.shieldBar.SetShield(targetActor.shield);

            // we weaken the hitpoint since shields make the hitpoints stronger
            // wait until we attack again
            yield return new WaitForSeconds(attackRate);
        }
        // prevent it from well... bugging lol
        targetActor.shield = 0f;
        

        // since house is pretty much the only thing that can be destroyed
        // in-game for team 2, we use house
        if(targetActor.team == 2 && targetActor.shield <= 0)
        {
            target.GetComponent<DestroyCity>().ShieldDestroyed();
        }

        // as long as the health of our actor is still above 0, it is still
        // capable of destroying
        while(targetActor.health >= 0f && health >= 0)
        {
            if(team == 0)
                StartCoroutine(EnemyAttack(target));
            // attack the target
            // we weaken the hitpoint since shields make the hitpoints stronger
            targetActor.HitHealth(hitpoints);
            targetActor.healthBar.SetHealth(targetActor.health);
            // wait until we attack again
            yield return new WaitForSeconds(attackRate);
        }

        targetActor.DestroyActor(target);
    }

    // attack
    void Attack(GameObject target)
    {
        if(team == 0)
        {
            StartCoroutine(TeamEnemyAttack(target));
        }
    }

    public void HitShield(float hitpoints)
    {
        shield = shield - (hitpoints - (hitpoints * 0.5f));
    }

    public void HitHealth(float hitpoints)
    {
        health = health - hitpoints;

        // since the house is pretty much the only team that
        // can be destroyed, we use house
    }

    public void DestroyActor(GameObject target)
    {
        //Debug.Log("Actor Destroyed");
        if(target.GetComponent<Actor>().team == 0)
        {
            Debug.Log("Destroying the enemy");
            // increment number of destroyed enemies
            EnemySpawner enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
            enemySpawner.enemiesKilled++; 
            Debug.Log("enemiesKilled: " + enemySpawner.enemiesKilled);
            Actor enemyActor = target.GetComponent<Actor>();
            // set hitpoints to 0
            enemyActor.hitpoints = 0;
            // set attack rate to 0
            enemyActor.attackRate = 0;
            // set AI to can't move
            target.GetComponent<Pathfinding.AIPath>().canMove = false;
            // hide the sprite
            GameObject gfx = target.transform.GetChild(0).gameObject;
            gfx.GetComponent<Renderer>().enabled=false;
            // set tcurrent target to null
            GetComponent<TowerShooting>().currentTarget = null;
            Destroy(target);
        }
        if(target.GetComponent<Actor>().team == 2)
        {
            target.GetComponent<DestroyCity>().CityDestroyed();
        }
            
    }

    // attack animation
    IEnumerator EnemyAttack(GameObject target)
    {
        Vector3 initialPosition = gameObject.transform.position;
        gameObject.transform.position += transform.up * Time.deltaTime * 30.7f;
        yield return new WaitForSeconds(attackRate * 0.1f); // 10% of the attack rate
        // go back
        gameObject.transform.position = initialPosition;
    }
}
