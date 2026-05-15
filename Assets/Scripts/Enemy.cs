using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] private Transform target;
    [SerializeField] private PlayerMove player;
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
        Debug.Log("input " + joystick.input);
        

        if (joystick == null || player == null || target == null)
            return;

        // 🟢 1. ИГРОК ДВИГАЕТСЯ
        if (joystick.IsMoving)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);

            lastKnownPosition = target.position;
            return;
        }else if (joystick.IsReleased)
        {
            agent.isStopped = false;
            agent.SetDestination(lastKnownPosition);
        }
        else
        {
            agent.ResetPath();
            agent.isStopped = true;
            return;
        }
        

        // 🔴 2. ДЖОЙСТИК В DEADZONE → СТОП
        /*if (joystick.input == Vector2.zero && joystick.IsReleased)
        {
            agent.ResetPath();
            agent.isStopped = true;
            return;
        }

        // 🟡 3. ОТПУСТИЛ, НО БЫЛ ДВИЖ → ИДЁМ В ПОСЛЕДНЮЮ ТОЧКУ
        if (joystick.IsReleased)
        {
            agent.isStopped = false;
            agent.SetDestination(lastKnownPosition);
        }*/
    }
}