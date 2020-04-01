using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointControl : MonoBehaviour
{
    [SerializeField]
    Transform[] waypoints;

    public float moveSpeed = 2f;

    int waypointIndex = 0;

    void Start()
    {
        Debug.Log("statre");
        transform.position = waypoints[waypointIndex].transform.position;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position,waypoints[waypointIndex].transform.position,moveSpeed * Time.deltaTime);

        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex += 1;
        }

        if (waypointIndex == waypoints.Length)
            waypointIndex = 0;
    }

}
