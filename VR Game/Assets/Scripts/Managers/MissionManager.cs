using System;
using UnityEngine;
using UnityEngine.Windows;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance; // Singleton instance
    public Objective currentObjective;
    public GameObject rain;
    public GameObject tornado;
    public GameObject snow;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void StartMission(string weather) {
        switch (weather)
        {
            case "rain":
                rain.SetActive(true);
                SetCurrentObjective(gameObject.AddComponent<FindBucket>());

                break;

            case "wind":
                tornado.SetActive(true);
                SetCurrentObjective(gameObject.AddComponent<FindPlanks>());
                
                break;

            case "cold":
                snow.SetActive(true);
                SetCurrentObjective(gameObject.AddComponent<FindLighter>());

                break;

            default:
                break;
        }
    }

    private void HandleObjectiveCompleted(Objective nextObjective)
    {
        if (currentObjective != null) {
            currentObjective.OnObjectiveCompleted -= HandleObjectiveCompleted;
            Destroy(currentObjective);
        }

        SetCurrentObjective(nextObjective);
    }

    private void SetCurrentObjective(Objective objective) {
        currentObjective = objective;
        currentObjective.OnObjectiveCompleted += HandleObjectiveCompleted;

        UIManager.Instance.UpdateObjectiveText(currentObjective.prefix + currentObjective.description);
    }
}
