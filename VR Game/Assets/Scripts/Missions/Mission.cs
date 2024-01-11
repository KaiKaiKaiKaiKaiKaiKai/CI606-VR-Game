using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public string prefix = "Objective: ";
    public string description;

    public delegate Mission MissionCreationDelegate();

    public MissionCreationDelegate nextMission;

    public delegate void MissionCompletedEventHandler(Mission nextMission);

    public event MissionCompletedEventHandler OnMissionCompleted;

    public Mission() { }

    public void CompleteMission()
    {
        // Perform any necessary actions for mission completion

        // Trigger the event with a completion message
        OnMissionCompleted?.Invoke(this.nextMission());
    }
}
