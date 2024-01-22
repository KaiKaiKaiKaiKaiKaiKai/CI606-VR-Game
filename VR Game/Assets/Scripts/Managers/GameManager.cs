using System.Threading.Tasks;
using UnityEngine;
using static WeatherData;

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
        loaded = GetWeatherDataFromAPI();
    }

    private async Task<bool> GetWeatherDataFromAPI()
    {
        weatherData = await APIManager.Instance.GetWeatherData();

        UIManager.Instance.HideLoadingPanel();
        MissionManager.Instance.StartMission(SelectMission());

        return true;
    }

    private string SelectMission()
    {
        Current current = weatherData.current;
        
        float rainWeight = CalculateRainWeight(current.rain);
        float windWeight = CalculateWindWeight(current.windspeed_10m);
        float coldWeight = CalculateColdWeight(current.temperature_2m);

        // Compare the weights and select the mission with the highest weight
        if (rainWeight >= windWeight && rainWeight >= coldWeight)
        {
            return "rain";
        }
        else if (windWeight >= rainWeight && windWeight >= coldWeight)
        {
            return "wind";
        }
        else
        {
            return "cold";
        }
    }

    private static float CalculateRainWeight(float rainValue)
    {
        return Mathf.Clamp01(rainValue / 10f);
    }

    private static float CalculateWindWeight(float windValue)
    {
        return Mathf.Clamp01(windValue / 20f);
    }

    private static float CalculateColdWeight(float temperatureValue)
    {
        return Mathf.Clamp01((10f - temperatureValue) / 10f);
    }
}
