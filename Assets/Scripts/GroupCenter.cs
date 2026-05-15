using UnityEngine;
using UnityEngine.InputSystem;

public class GroupCenter : MonoBehaviour
{
    [SerializeField] Transform[] enemies;

    [SerializeField] Transform centerMarker;

    void Update()
    {
        centerMarker.position = GetCenterPoint();
    }

    Vector3 GetCenterPoint()
    {
        if (enemies.Length == 0) return Vector3.zero;

        Vector3 sum = Vector3.zero;

        foreach (Transform enemy in enemies)
            sum += enemy.position;

        return sum / enemies.Length;
    }


}