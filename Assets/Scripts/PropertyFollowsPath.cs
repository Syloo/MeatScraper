using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyFollowsPath : MonoBehaviour
{
    public float movementSpeed;
    public Transform[] path;

    private int nextWaypoint;
    private float remainingBreakTime;

    // Start is called before the first frame update
    private void Start()
    {
        nextWaypoint = 0;
        remainingBreakTime = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (remainingBreakTime > 0f)
        {
            remainingBreakTime -= Time.deltaTime;
            return;
        }

        Vector2 toWaypoint = path[nextWaypoint].position - transform.position;
        if (toWaypoint.magnitude < Time.deltaTime * movementSpeed)
        {
            transform.position = path[nextWaypoint].position;
            remainingBreakTime = path[nextWaypoint].position.z;

            nextWaypoint++;
            if (nextWaypoint >= path.Length) nextWaypoint = 0;
        }
        else
        {
            Vector3 increment = Time.deltaTime * movementSpeed * toWaypoint.normalized;
            transform.position += increment;
        }
    }
}
