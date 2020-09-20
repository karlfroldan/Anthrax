using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyMove : MonoBehaviour
{
    // Start is called before the first frame update

    public float waypointDistanceThreshold;

    void Start()
    {
        // get the distance of the next waypoint
        float waypoint_distance;

        waypoint_distance = gameObject.GetComponent<SpawnerWaypoint>().Distance();

        Debug.Log("waypoint_distance=" + waypoint_distance);
    }

    // Update is called once per frame
    public void Move()
    {
        // get the angle of rotation
        // we randomize so that the enemies wouldn't all 
        // overlap each other
        float error = Random.Range(0.0f, 0.03f);
        
        SpawnerWaypoint spawnerWaypoint = gameObject.GetComponent<SpawnerWaypoint>();

        Debug.Log("current angle: " + spawnerWaypoint.Angle());
    }
}
