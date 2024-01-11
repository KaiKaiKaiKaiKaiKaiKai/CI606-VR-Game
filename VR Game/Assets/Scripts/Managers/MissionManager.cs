using System;
using Unity.VisualScripting;
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
        if (this.currentMission != null) {
            this.currentMission.OnMissionCompleted -= HandleMissionCompleted;
            Destroy(this.currentMission);
        }

        SetCurrentMission(nextMission);
    }

    private void SetCurrentMission(Mission mission) {
        this.currentMission = mission;
        this.currentMission.OnMissionCompleted += HandleMissionCompleted;

        UIManager.Instance.UpdateMissionText(this.currentMission.prefix + this.currentMission.description);
    }
}
