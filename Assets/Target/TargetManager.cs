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

    private GameObject targetNearObject;
    private GameObject targetMiddleObject;
    private GameObject targetFarObject;

    List<UnitControl> selectedUnits;

    public void onZilotGroupSelected() {
        destroyAllTargets();


        selectedUnits = manager.GetActivatedUnits();

        targetCenter = Instantiate(targetNear, Vector3.zero, Quaternion.identity);
        targetCenter.transform.position = GetCenterPoint();




        targetNearObject = Instantiate(targetNear, Vector3.zero, Quaternion.identity);
        TargetMove targetNearMove = targetNearObject.GetComponent<TargetMove>();
        targetNearMove.centerObject = targetCenter.transform;

        targetMiddleObject = Instantiate(targetMiddle, Vector3.zero, Quaternion.identity);
        TargetMove targetMiddleMove = targetNearObject.GetComponent<TargetMove>();
        targetMiddleMove.centerObject = targetCenter.transform;

        targetFarObject = Instantiate(targetFar, Vector3.zero, Quaternion.identity);
        TargetMove targetFarMove = targetNearObject.GetComponent<TargetMove>();
        targetFarMove.centerObject = targetCenter.transform;
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
