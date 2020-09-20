using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWaypoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject waypoint;
    private List<Transform> waypoints = new List<Transform>();
    private Transform currentWaypoint; 

    void Start()
    {
        foreach(Transform waypointChild in waypoint.transform)
        {
            waypoints.Add(waypointChild);
        }
        currentWaypoint = waypoints[0];
        Debug.Log("currentWaypoint: " + currentWaypoint.transform);
    }

    // A method that gets the distance between the object
    // and the next waypoint
    public float Distance()
    {
        Transform currentGameObject = gameObject.transform;

        return Vector3.Distance(currentGameObject.position, 
            currentWaypoint.position);
    }

    // get the relative angle of the object compared to the 
    // game object
    public float Angle()
    {
        Transform currentGameObject = gameObject.transform;

        return 3.2f;
    }
}
