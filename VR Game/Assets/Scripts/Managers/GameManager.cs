using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    public Task<bool> loaded;
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
    }

    private void Start()
    {
        this.loaded = GetWeatherDataFromAPI();
    }

    private async Task<bool> GetWeatherDataFromAPI()
    {
        this.weatherData = await APIManager.Instance.GetWeatherData();

        UIManager.Instance.HideLoadingPanel();

        return true;
    }
}
