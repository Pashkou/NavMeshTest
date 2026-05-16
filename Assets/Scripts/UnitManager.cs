using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UnitManager : MonoBehaviour
{

    [Header("List of all enemies")]
    public List<Enemy> enemies = new List<Enemy>();
    private Dictionary<Enemy, bool> enemyStates = new Dictionary<Enemy, bool>();
    public string activatedTag;

    private void Awake()
    {
        InitializeDictionary();
    }

    public void ActivateByTag(string tagName)
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null && enemy.CompareTag(tagName))
            {
                SetActivated(enemy, true);
            }
        }
    }

    public void DeactivateAll()
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                SetActivated(enemy, false);
            }
        }
    }
    /// <summary>
    /// Set activation state for a specific enemy.
    /// </summary>
    public void SetActivated(Enemy enemy, bool activated)
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

        // Optionally activate/deactivate the GameObject in the scene
        //enemy.gameObject.SetActive(activated);
    }

    private void InitializeDictionary()
    {
        enemyStates.Clear();

        foreach (Enemy enemy in enemies)
        {
            if (enemy != null && !enemyStates.ContainsKey(enemy))
            {
                enemyStates.Add(enemy, false);
            }
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsActivated(Enemy enemy)
    {
        if (enemy != null && enemyStates.TryGetValue(enemy, out bool state))
        {
            return state;
        }

        return false;
    }

    public bool IsActivatedByIdAndTag(int id, string tag)
    {
        foreach (Enemy enemy in enemies)
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
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null &&
                enemy.id == id &&
                enemy.CompareTag(tag))
            {
                SetActivated(enemy, activated);
                return true;
            }
        }

        Debug.LogWarning($"No enemy found with ID {id} and tag '{tag}'");
        return false;
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
