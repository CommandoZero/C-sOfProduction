using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyAi : MonoBehaviour {

    public float WaitTime = 5f;
    private float RuntimeWaitTime = 0;
    public List<Transform> WaypointsFound = new List<Transform>();
    private NavMeshAgent NavAgent;
    private int WaypointIndex;
    private bool ChasePlayer;
    private bool debugPlayerDead;
    private RaycastHit ray;
    private Transform playerTransform;

    private void Start()
    {
        FindAllWaypoints();
        NavAgent = GetComponent<NavMeshAgent>();
        SelectRandomDestination();
    }

    void FindAllWaypoints()
    {
        GameObject[] pointsFound = GameObject.FindGameObjectsWithTag("Waypoint");

        foreach(GameObject g in pointsFound)
        {
            WaypointsFound.Add(g.transform);
        }
    }

    void SelectRandomDestination(bool random = true)
    {
        if(WaypointsFound.Count != 0)
        {
            if(random)
            {
                WaypointIndex = Random.Range(0, WaypointsFound.Count);
            }
            else
            {
                //If we hit the end of the list
                if(WaypointIndex >= WaypointsFound.Count)
                {
                    WaypointIndex = 0;
                }
                else
                {
                    WaypointIndex++;
                }
            }

            NavAgent.destination = WaypointsFound[WaypointIndex].position;
        }
    }

    private void OnCollisionEnter(Collision trigger)
    {
        if(trigger.transform.tag.Equals("Player",System.StringComparison.Ordinal))
        {
            //collision.transform.GetComponent<Player>().ApplyDamage();
            trigger.transform.GetComponent<Rigidbody>().isKinematic = false;
            ChasePlayer = false;
            debugPlayerDead = true;
            SelectRandomDestination();
        }
    }

    void Update()
    {
        if (ChasePlayer)
        {
            NavAgent.destination = playerTransform.position;

            if (NavAgent.isStopped)
            {
                NavAgent.isStopped = false;
            }
        }
        else if (!ChasePlayer && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out ray, 50f))
        {
            if (NavAgent.remainingDistance < 0.5f)
            {
                if (!NavAgent.isStopped)
                {
                    NavAgent.isStopped = true;
                    RuntimeWaitTime = WaitTime;
                }
                else
                {
                    if (RuntimeWaitTime <= 0)
                    {
                        SelectRandomDestination(false);
                        NavAgent.isStopped = false;
                    }
                    else
                    {
                        RuntimeWaitTime -= Time.fixedUnscaledDeltaTime;
                    }
                }
            }

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 50f);

            //If we saw the player CHASE HIM!
            if (!debugPlayerDead && ray.transform.tag.Equals("Player", System.StringComparison.Ordinal))
            {
                playerTransform = ray.transform;
                ChasePlayer = true;
            }
        }
        else
        {
            return;
        }
	}
}
