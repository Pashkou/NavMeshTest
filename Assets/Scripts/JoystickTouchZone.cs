using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickTouchZone : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Vector2 input;

    [Header("Joystick settings")]
    public RectTransform joystickBase;

    public float deadZone = 10f;

    public bool IsMoving => input != Vector2.zero;
    public bool IsReleased = false;

    public Vector2 lastInput;
    private Vector2 startPos;

    public void OnPointerDown(PointerEventData eventData)
    {
        input = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBase,
            eventData.position,
            eventData.pressEventCamera,
            out startPos
        );
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateInput(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector2 currentPos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBase,
            eventData.position,
            eventData.pressEventCamera,
            out currentPos
        );

        Vector2 delta = currentPos - startPos;

        float distance = delta.magnitude;

        if (distance < deadZone)
        {
            Debug.Log("DEAD");
            input = Vector2.zero;
            IsReleased = false;
            return;
        }

        Debug.Log("NOT DEAD");
        input = Vector2.zero;
        IsReleased = true;
        lastInput = input;

    }

    void UpdateInput(PointerEventData eventData)
    {
        Vector2 currentPos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBase,
            eventData.position,
            eventData.pressEventCamera,
            out currentPos
        );

        Vector2 delta = currentPos - startPos;

        float distance = delta.magnitude;

        if (distance < deadZone)
        {
            Debug.Log("DEAD");
            input = Vector2.zero;
            return;
        }

        Debug.Log("NOT DEAD");
        input = delta.normalized;

        lastInput = input;
    }
}