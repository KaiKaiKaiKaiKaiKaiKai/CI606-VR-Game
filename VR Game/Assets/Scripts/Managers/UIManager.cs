using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject LoadingPanel;
    public TextMeshProUGUI ObjectiveText;

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

    public void UpdateObjectiveText(string description) {
        ObjectiveText.text = description;
    }
}