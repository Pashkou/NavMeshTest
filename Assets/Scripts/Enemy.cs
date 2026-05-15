using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 destination;
    private bool hasDestination;

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
        // NEW INPUT SYSTEM CLICK
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            SetDestinationFromMouse();
        }

        if (hasDestination)
        {
            agent.SetDestination(destination);

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                hasDestination = false;
                agent.ResetPath();
            }
        }
    }

    void SetDestinationFromMouse()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        Plane plane = new Plane(Vector3.forward, Vector3.zero);

        if (plane.Raycast(ray, out float enter))
        {
            destination = ray.GetPoint(enter);
            hasDestination = true;
        }
    }
}