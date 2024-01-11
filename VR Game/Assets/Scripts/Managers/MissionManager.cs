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

    private void SetCurrentMission(Mission mission) {
        this.currentMission = mission;
        
        UIManager.Instance.UpdateMissionText(this.currentMission.prefix + this.currentMission.description);
    }

    public void CompleteCurrentMission() {
        currentMission.completed = true;

        Mission nextMission = currentMission.nextMission;
        
        if (nextMission == null) return;

        SetCurrentMission(nextMission);
    }
}
