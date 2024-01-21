using UnityEngine;

public class Objective : MonoBehaviour
{
    public string prefix = "Objective: ";
    public string description;

    public delegate Objective ObjectiveCreationDelegate();

    public ObjectiveCreationDelegate nextObjective;

    public delegate void ObjectiveCompletedEventHandler(Objective nextObjective);

    public event ObjectiveCompletedEventHandler OnObjectiveCompleted;

    public Objective() { }

    public void CompleteObjective(bool successful)
    {
        if (!successful)
        {
            nextObjective = () => gameObject.AddComponent<MissionFailed>();
        }
        else if (nextObjective == null) {
            nextObjective = () => gameObject.AddComponent<MissionCompleted>();
        }

        // Trigger the event with a completion message
        OnObjectiveCompleted?.Invoke(nextObjective());
    }
}
