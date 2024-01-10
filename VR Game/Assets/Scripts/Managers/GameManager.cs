using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    public WeatherData weatherData;

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

        APIManager.Instance.GetWeatherData().ContinueWith((t) =>
        {
            if (t.IsFaulted)
            {
                // Handle exception
                Exception ex = t.Exception;
                Debug.LogError(ex.Message);
            }
            else if (t.IsCompleted)
            {
                // Use the result
                weatherData = t.Result;

                UIManager.Instance.HideLoadingPanel();
            }
        });
    }
}
