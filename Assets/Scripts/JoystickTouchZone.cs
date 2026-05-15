using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickTouchZone : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Vector2 input;

    [Header("Joystick settings")]
    public RectTransform joystickBase;

    [Range(3f, 200f)]
    public float radius = 100f;

    [Range(0f, 0.3f)]
    public float deadZone = 0.1f;

    private Vector2 startPos;

    public void OnPointerDown(PointerEventData eventData)
    {
        // фиксируем центр джойстика
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBase,
            eventData.position,
            eventData.pressEventCamera,
            out startPos
        );

        input = Vector2.zero;
        UpdateInput(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateInput(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
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

        // 🔥 deadzone (PUBG style)
        if (distance < radius * deadZone)
        {
            input = Vector2.zero;
            return;
        }

        // normalize
        Vector2 direction = delta.normalized;

        float strength = Mathf.Clamp01(distance / radius);

        input = direction * strength;
    }
}