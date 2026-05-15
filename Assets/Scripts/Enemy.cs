using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] private Transform target; // 🎯 цель

    [SerializeField] float stopDistance = 1.2f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.stoppingDistance = stopDistance;
    }

    private void Update()
    {
        if (target == null) return;

        agent.SetDestination(target.position);

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.ResetPath();
        }
    }

}