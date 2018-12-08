using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyAi : MonoBehaviour {

    public float rotationSpeed = 1f;
    public float speed = 2f;
    public float WaitTime = 5f;
    public float minDamage = 5f;
    public float maxDamage = 20f;
    private float RuntimeWaitTime = 0;
    public List<Transform> WaypointsFound = new List<Transform>();
    public float EnemyHealth;
    public GameObject DamageReplacement;
    //private NavMeshAgent NavAgent;
    private int WaypointIndex;
    private bool ChasePlayer;
    private bool stopped;
    private RaycastHit ray;
    private Transform playerTransform;
    private Transform destination;
    private Vector3 offset = new Vector3(180, 0, 0);

    private void Start()
    {
        FindAllWaypoints();
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

    void SelectRandomDestination(bool random = false)
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

        }

        destination = WaypointsFound[WaypointIndex];
    }

    private void OnCollisionEnter(Collision trigger)
    {
        if(trigger.transform.tag.Equals("Player",System.StringComparison.Ordinal))
        {
            trigger.transform.SendMessage("ApplyDamage", Random.Range(minDamage, maxDamage), SendMessageOptions.DontRequireReceiver);
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
                transform.LookAt(playerTransform);
                //transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, playerTransform.position - transform.position, Time.deltaTime, 0f));
                transform.position = Vector3.Lerp(transform.position, playerTransform.position, Time.deltaTime*speed);
            
            }

            if (stopped)
            {
                stopped = false;
            }

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out ray, 50f))
            {
                if (Random.Range(0, 100) <= 5 && ray.transform.tag.Equals("Player", System.StringComparison.Ordinal))
                {
                    ray.transform.SendMessage("ApplyDamage",Random.Range(minDamage,maxDamage),SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        if (!ChasePlayer && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out ray, 50f))
        {

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 50f);

            //If we saw the player CHASE HIM!
            if (playerTransform == null && ray.transform.tag.Equals("Player", System.StringComparison.Ordinal))
            {
                playerTransform = ray.transform;
                ChasePlayer = true;
            }
        }
        if (destination != null && !stopped && !ChasePlayer)
        {
            transform.LookAt(destination);
            transform.position = Vector3.Lerp(transform.position, destination.position, Time.deltaTime * speed);

            if ((transform.position - destination.position).magnitude < 0.5f)
            {
                if (!stopped)
                {
                    stopped = true;
                    RuntimeWaitTime = WaitTime;
                }
            }
        }
        if (stopped)
        {
            if (RuntimeWaitTime <= 0)
            {
                SelectRandomDestination(false);
                stopped = false;
            }
            else
            {
                RuntimeWaitTime -= Time.fixedUnscaledDeltaTime;
            }
        }
	}
}
