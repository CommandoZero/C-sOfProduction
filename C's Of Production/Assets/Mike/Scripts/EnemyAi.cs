using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyAi : MonoBehaviour {

    public float WaitTime = 5f;
    public float minDamage = 5f;
    public float maxDamage = 20f;
    private float RuntimeWaitTime = 0;
    public List<Transform> WaypointsFound = new List<Transform>();
    public float EnemyHealth;
    public GameObject DamageReplacement;
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

    public void ApplyDamage(float amount)
    {
        EnemyHealth -= amount;
        if(EnemyHealth <= 0)
        {
            Instantiate(DamageReplacement, transform.position, transform.rotation);
            Destroy(gameObject);
        }
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
            ray.transform.SendMessage("ApplyDamage", Random.Range(minDamage, maxDamage), SendMessageOptions.DontRequireReceiver);
        }
    }

    void Update()
    {
        if (ChasePlayer)
        {
            if (playerTransform == null)
            {
                ChasePlayer = false;
                SelectRandomDestination();
            }
            else
            {
                NavAgent.destination = playerTransform.position;
            }

            if (NavAgent.isStopped)
            {
                NavAgent.isStopped = false;
            }

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out ray, 50f))
            {
                if (Random.Range(0, 100) <= 5 && ray.transform.tag.Equals("Player", System.StringComparison.Ordinal))
                {
                    ray.transform.SendMessage("ApplyDamage",Random.Range(minDamage,maxDamage),SendMessageOptions.DontRequireReceiver);
                }
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
