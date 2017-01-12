using UnityEngine;
using System.Collections;

public class MoveToGoal : MonoBehaviour
{
    public Transform target;
    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = target.position;
    }

    
    void LateUpdate()
    {
        agent.destination = target.position;
    }
}
