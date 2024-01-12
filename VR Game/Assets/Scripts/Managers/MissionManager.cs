using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance; // Singleton instance
    public Mission currentMission;

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
        SetCurrentMission(gameObject.AddComponent<FindBucket>());
    }

    private void HandleMissionCompleted(Mission nextMission)
    {
        if (currentMission != null) {
            currentMission.OnMissionCompleted -= HandleMissionCompleted;
            Destroy(currentMission);
        }

        SetCurrentMission(nextMission);
    }

    private void SetCurrentMission(Mission mission) {
        currentMission = mission;
        currentMission.OnMissionCompleted += HandleMissionCompleted;

        UIManager.Instance.UpdateMissionText(currentMission.prefix + currentMission.description);
    }
}
