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
            1- player
            0- enemy
    */
    public int team;

    public HealthBar healthBar;
    public ShieldBar shieldBar;

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

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D col)
    {
        float secondsLeft = Random.Range(0f, 1.8f);

        if(enemies.Any(s => col.gameObject.name.Contains(s)))
        {
            // if the actor is a bacteria or virus or whatsoever
            // we halt randomly
            if(team == 0)
            {
                StartCoroutine(EnemyWaiter(secondsLeft, col.gameObject));
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
        // in-game for team 1, we use house
        if(targetActor.team == 1)
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

    public void DestroyActor(GameObject actor)
    {
        Debug.Log("Actor Destroyed");
        if(actor.GetComponent<Actor>().team == 1)
        {
            actor.GetComponent<DestroyCity>().CityDestroyed();
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
