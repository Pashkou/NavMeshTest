using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickTouchZone : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Vector2 input;

    [Header("Joystick settings")]
    public RectTransform joystickBase;

    public float nearZone = 0.4f;
    public float middleZone = 1f;

    public bool IsNear = false;
    public bool IsMiddle = false;
    public bool IsFar = false;


    public GameObject targetNear;
    public GameObject targetMiddle;
    public GameObject targetFar;


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

        setColors(distance);
        input = Vector2.zero;
        if (distance < nearZone)
        {
            IsNear = false;
            IsMiddle = false;
            IsFar = false;
            return;
        }
        else if (distance < middleZone)
        {
            IsNear = false;
            IsMiddle = true;
            IsFar = true;
        }
        else {
            IsNear = false;
            IsMiddle = false;
            IsFar = true;
        }
       
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

        setColors(distance);

        if (distance < nearZone)
        {
            input = Vector2.zero;
            IsNear = false;
            IsMiddle = false;
            IsFar = false;
            return;
        }
        input = delta.normalized;
        IsNear = true;
    }

    private void setColors(float distance) {
        if (distance < nearZone)
        {
            targetNear.GetComponent<SpriteRenderer>().color = Color.blue;
            targetMiddle.GetComponent<SpriteRenderer>().color = Color.blue;
            targetFar.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if (distance < middleZone)
        {
            targetNear.GetComponent<SpriteRenderer>().color = Color.blue;
            targetMiddle.GetComponent<SpriteRenderer>().color = Color.red;
            targetFar.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            targetNear.GetComponent<SpriteRenderer>().color = Color.blue;
            targetMiddle.GetComponent<SpriteRenderer>().color = Color.blue;
            targetFar.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}