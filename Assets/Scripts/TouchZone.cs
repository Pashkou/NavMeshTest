using UnityEngine;
using UnityEngine.EventSystems;

public class TouchZone : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Vector2 input;

    [Header("World center object")]
    public GameObject centerObject;

    [Range(0.1f, 300f)]
    public float radius = 100f;

    [Range(0f, 50f)]
    public float deadZone = 10f;

    public Camera uiCamera;

    public void OnPointerDown(PointerEventData eventData)
    {
        input = Vector2.zero;   // важно: сброс при новом касании
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
        if (centerObject == null)
            return;

        Vector2 centerScreen = Camera.main.WorldToScreenPoint(centerObject.transform.position);

        Vector2 dir = eventData.position - centerScreen;

        float distance = dir.magnitude;

        // deadzone
        if (distance <= deadZone)
        {
            input = Vector2.zero;
            return;
        }

        float normalized = Mathf.Clamp01(distance / radius);

        Vector2 direction = dir.normalized;

        input = direction * normalized;

        // защита от микродрейфа
        if (input.magnitude < 0.01f)
            input = Vector2.zero;
    }
}