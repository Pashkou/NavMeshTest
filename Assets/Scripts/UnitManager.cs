using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UnitManager : MonoBehaviour
{

    [Header("List of all enemies")]
    public List<UnitControl> units = new List<UnitControl>();
    private Dictionary<UnitControl, bool> enemyStates = new Dictionary<UnitControl, bool>();
    public string activatedTag;

    private void Awake()
    {
        InitializeDictionary();
    }

    public void ActivateByTag(string tagName)
    {
        foreach (UnitControl enemy in units)
        {
            if (enemy != null && enemy.CompareTag(tagName))
            {
                SetActivated(enemy, true);
            }
        }
    }

    public void DeactivateAll()
    {
        foreach (UnitControl enemy in units)
        {
            if (enemy != null)
            {
                SetActivated(enemy, false);
            }
        }
    }

    public void SetActivated(UnitControl enemy, bool activated)
    {
        if (enemy == null)
            return;

        if (enemyStates.ContainsKey(enemy))
        {
            enemyStates[enemy] = activated;
        }
        else
        {
            enemyStates.Add(enemy, activated);
        }
    }

    private void InitializeDictionary()
    {
        enemyStates.Clear();

        foreach (UnitControl enemy in units)
        {
            if (enemy != null && !enemyStates.ContainsKey(enemy))
            {
                enemyStates.Add(enemy, false);
            }
        }
    }

    public bool IsActivated(UnitControl enemy)
    {
        if (enemy != null && enemyStates.TryGetValue(enemy, out bool state))
        {
            return state;
        }

        return false;
    }

    public bool IsActivatedByIdAndTag(int id, string tag)
    {
        foreach (UnitControl enemy in units)
        {
            if (enemy != null &&
                enemy.id == id &&
                enemy.CompareTag(tag))
            {
                return IsActivated(enemy);
            }
        }

        return false;
    }

    public bool SetActivatedByIdAndTag(int id, string tag, bool activated)
    {
        foreach (UnitControl enemy in units)
        {
            if (enemy != null &&
                enemy.id == id &&
                enemy.CompareTag(tag))
            {
                SetActivated(enemy, activated);
                return true;
            }
        }
        return false;
    }

    public List<UnitControl> GetActivatedUnits()
    {
        List<UnitControl> activatedEnemies = new List<UnitControl>();

        foreach (var pair in enemyStates)
        {
            UnitControl enemy = pair.Key;
            bool isActive = pair.Value;

            if (enemy != null && isActive)
            {
                activatedEnemies.Add(enemy);
            }
        }

        return activatedEnemies;
    }

    public void selectZilots() {
        activatedTag = "Zilot";
        DeactivateAll();
        ActivateByTag("Zilot");
    }

    public void selectStalkers() {
        activatedTag = "Stalker";
        DeactivateAll();
        ActivateByTag("Stalker");
    }
    public void selectTanks() { }
    public void selectMutalisk() { }

    public void selectNumber1() {
        DeactivateAll();
        SetActivatedByIdAndTag(1, "Zilot", true);

    }
    public void selectNumber2() { }
    public void selectNumber3() { }
    public void selectNumber4() { }
    public void selectNumber5() { }
    public void selectNumber6() { }
}
