using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance; // Singleton instance
    public Objective currentObjective;
    public GameObject rain;
    public GameObject tornado;

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

    private void Start()
    {
        tornado.SetActive(true);
        SetCurrentObjective(gameObject.AddComponent<FindPlanks>());
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
