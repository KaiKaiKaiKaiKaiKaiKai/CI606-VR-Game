using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextMission()
    {
        MissionManager.Instance.currentMission.CompleteMission();
    }
}
