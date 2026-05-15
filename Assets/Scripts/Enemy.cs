using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] private Transform targetFar;
    [SerializeField] private Transform targetNear;
    [SerializeField] private JoystickTouchZone joystick;

    private Vector3 lastKnownPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (joystick.IsMoving)
        {
            agent.isStopped = false;
            agent.SetDestination(targetNear.position);
            lastKnownPosition = targetFar.position;
            return;
        } else if (joystick.IsReleased) {
            agent.isStopped = false;
            agent.SetDestination(lastKnownPosition);
        } else {
            agent.ResetPath();
            agent.isStopped = true;
            return;
        }
    }
}