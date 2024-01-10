using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject LoadingPanel;
    private bool isLoadingPanelVisible = true;

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

    private void Update()
    {
        if (LoadingPanel != null)
        {
            if (isLoadingPanelVisible)
            {
                LoadingPanel.SetActive(true);
            }
            else
            {
                LoadingPanel.SetActive(false);
            }
        }
    }

    public void ShowLoadingPanel()
    {
        isLoadingPanelVisible = true;
    }

    public void HideLoadingPanel()
    {
        isLoadingPanelVisible = false;
    }
}