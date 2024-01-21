using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance; // Singleton instance
    public Objective currentObjective;

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
        SetCurrentObjective(gameObject.AddComponent<FindBucket>());
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
