using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject LoadingPanel;
    public TextMeshProUGUI MissionText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowLoadingPanel()
    {
        LoadingPanel.SetActive(true);
    }

    public void HideLoadingPanel()
    {
        LoadingPanel.SetActive(false);
    }

    public void UpdateMissionText(string description) {
        MissionText.text = description;
    }
}