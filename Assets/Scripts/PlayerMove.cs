using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public JoystickTouchZone touchZone;
    public Transform centerObject;

    public float radius = 2f;
    public float speed = 5f;

    public Vector3 lastMoveDirection;

    public UnitManager manager;

    void Update()
    {
        if (!manager.activatedTag.Equals(tag)) {
            return;
        }

        Vector2 input = touchZone.input;

        if (input.sqrMagnitude < 0.001f)
            return;

        Vector3 dir = new Vector3(input.x, input.y, 0).normalized;

        lastMoveDirection = dir;

        Vector3 targetPos = centerObject.position + dir * radius;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            speed * Time.deltaTime
        );
    }
}