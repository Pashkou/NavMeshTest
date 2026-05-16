using UnityEngine;

public class TargetMove : MonoBehaviour
{
    public JoystickTouchZone touchZone;
    public Transform centerObject;

    public float radius = 2f;
    public float speed = 5f;

    void Update()
    {
        if (touchZone == null)
        {
             return;
        }

        Vector2 input = touchZone.input;

        if (input.sqrMagnitude < 0.001f)
            return;

        Vector3 dir = new Vector3(input.x, input.y, 0).normalized;

        Vector3 targetPos = centerObject.position + dir * radius;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            speed * Time.deltaTime
        );
    }
}