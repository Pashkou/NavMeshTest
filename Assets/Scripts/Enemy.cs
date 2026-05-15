using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;

    
    [SerializeField] private Transform targetNear;
    [SerializeField] private Transform targetMiddle;
    [SerializeField] private Transform targetFar;
    [SerializeField] private JoystickTouchZone joystick;

    private Vector3 lastKnownMiddlePosition;
    private Vector3 lastKnownFarPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (joystick.IsNear)
        {
            agent.isStopped = false;
            agent.SetDestination(targetNear.position);
            lastKnownMiddlePosition = targetMiddle.position;
            lastKnownFarPosition = targetFar.position;
            return;
        } else if (joystick.IsMiddle) {
            agent.isStopped = false;
            agent.SetDestination(lastKnownMiddlePosition);
        } else if (joystick.IsFar) {
            agent.isStopped = false;
            agent.SetDestination(lastKnownFarPosition);
        } else {
            agent.ResetPath();
            agent.isStopped = true;
            return;
        }
    }
}