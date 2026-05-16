using UnityEngine;
using UnityEngine.AI;

public class UnitControl : MonoBehaviour
{
    public int id;
    private NavMeshAgent agent;

    [SerializeField] private JoystickTouchZone joystick;

    public UnitManager manager;
    public TargetManager targetManager;

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
        if (!manager.IsActivatedByIdAndTag(id, tag)) {
            return;
        }

        if (joystick.IsNear)
        {
            agent.isStopped = false;
            agent.SetDestination(targetManager.targetNearObject.transform.position);
            lastKnownMiddlePosition = targetManager.targetMiddleObject.transform.position;
            lastKnownFarPosition = targetManager.targetFarObject.transform.position;
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