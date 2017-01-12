// Based on Unity's Interface Finite State Machine tutorial

using UnityEngine;
using System.Collections;

public class AgentBehavior : MonoBehaviour
{

    // Possible behavior states
    public enum State
    {
        Patrol,
        Alert,
        Chase
    }

    private State currentState = State.Patrol; // Patrol is the default behavior

    public Transform eyes;      // Set to a gameobject to position raycast origin
    public float sightRange;    // How far can the agent see
    public Vector3 sightOffset = new Vector3(0, .5f, 0);    // Adjustments to sight raycast

    private UnityEngine.AI.NavMeshAgent navMeshAgent;  // Navmesh Agent component
    private Transform chaseTarget;      // Set when in Chase state

    public Transform[] wayPoints;   // Array containing sequential waypoints
    private int nextWayPoint;       // Waypoint to move towards


    public float searchingDuration = 2f;        // How long to remain in Search state
    public float searchingTurnSpeed = 120f;     // Speed of search sweep
    private float searchTimer;                  // Keep track of time spent searching for target

    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        Look();     // Always look for target

        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Alert:
                Search();
                break;
            case State.Chase:
                Chase();
                break;
        }
    }

    // Look for target
    private void Look()
    {
        RaycastHit hit;
        if (currentState == State.Chase)
        {
            Vector3 enemyToTarget = (chaseTarget.position + sightOffset) - eyes.transform.position;
            Debug.DrawRay(eyes.position, enemyToTarget, Color.red);

            if (Physics.Raycast(eyes.transform.position, enemyToTarget, out hit, sightRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    chaseTarget = hit.transform;
                }
            }
            else
            {
                currentState = State.Alert;
            }
        }
        else
        {
            Debug.DrawRay(eyes.position, eyes.forward * sightRange, Color.green);

            if (Physics.Raycast(eyes.position, eyes.forward, out hit, sightRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    chaseTarget = hit.transform;
                    currentState = State.Chase;
                }
            }
        }
    }

    private void Patrol()
    {
        navMeshAgent.destination = wayPoints[nextWayPoint].position;
        navMeshAgent.Resume();

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            nextWayPoint = (nextWayPoint + 1) % wayPoints.Length;

        }
    }

    private void Search()
    {
        navMeshAgent.Stop();
        transform.Rotate(0, searchingTurnSpeed * Time.deltaTime, 0);
        searchTimer += Time.deltaTime;

        if (searchTimer >= searchingDuration)
        {
            currentState = State.Patrol;
            searchTimer = 0f;
        }

    }

    private void Chase()
    {
        navMeshAgent.destination = chaseTarget.position;
        navMeshAgent.Resume();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            currentState = State.Alert;
    }

}
