using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickTouchZone : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Vector2 input;

    [Header("Joystick settings")]
    public RectTransform joystickBase;

    public float deadZone = 10f;

    public bool IsMoving = false;
    public bool IsReleased = false;

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
        IsMoving = false;

        if (distance < deadZone)
        {
            input = Vector2.zero;
            IsReleased = false;
            return;
        }
        input = Vector2.zero;
        IsReleased = true;
        IsMoving = false;
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
            input = Vector2.zero;
            IsMoving = false;
            return;
        }
        input = delta.normalized;
        IsMoving = true;
    }
}