using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNode : MonoBehaviour
{
    [Header("This is the waypoint we are going towards, not yet reached")]
    public float minDistanceToReachWaypoint = 2;

    public WaypointNode[] nextWaypointNode;
}
