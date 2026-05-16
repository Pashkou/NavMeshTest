using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public UnitManager manager;

    [Header("TargetNear Reference")]
    public GameObject targetNear;

    [Header("TargetMiddle Reference")]
    public GameObject targetMiddle;

    [Header("TargetFar Reference")]
    public GameObject targetFar;

    [Header("TargetCenter Reference")]
    public GameObject targetCenter;

    public JoystickTouchZone touchZone;

    public GameObject targetNearObject;
    public GameObject targetMiddleObject;
    public GameObject targetFarObject;

    List<UnitControl> selectedUnits;

    void Update()
    {
        if (selectedUnits != null && targetCenter != null)
        {
            targetCenter.transform.position = GetCenterPoint();
        }
    }

    public void onZilotGroupSelected() {
        destroyAllTargets();
        createAllTargets();
    }

    public void onStalkerGroupSelected()
    {
        destroyAllTargets();
        createAllTargets();
    }

    private void createAllTargets()
    {
        selectedUnits = manager.GetActivatedUnits();

        targetCenter = Instantiate(targetCenter, Vector3.zero, Quaternion.identity);
        targetCenter.transform.position = GetCenterPoint();

        targetNearObject = Instantiate(targetNear, Vector3.zero, Quaternion.identity);
        TargetMove targetNearMove = targetNearObject.GetComponent<TargetMove>();
        targetNearMove.centerObject = targetCenter.transform;
        targetNearMove.touchZone = touchZone;


        targetMiddleObject = Instantiate(targetMiddle, Vector3.zero, Quaternion.identity);
        TargetMove targetMiddleMove = targetMiddleObject.GetComponent<TargetMove>();
        targetMiddleMove.centerObject = targetCenter.transform;
        targetMiddleMove.touchZone = touchZone;


        targetFarObject = Instantiate(targetFar, Vector3.zero, Quaternion.identity);
        TargetMove targetFarMove = targetFarObject.GetComponent<TargetMove>();
        targetFarMove.centerObject = targetCenter.transform;
        targetFarMove.touchZone = touchZone;
    }
    private void destroyAllTargets() {
        Destroy(targetNearObject);
        Destroy(targetMiddleObject);
        Destroy(targetFarObject);
    }

    Vector3 GetCenterPoint()
    {

        if (selectedUnits.Count == 0) return Vector3.zero;

        Vector3 sum = Vector3.zero;

        foreach (UnitControl unit in selectedUnits)
            sum += unit.transform.position;

        return sum / selectedUnits.Count;
    }
}
