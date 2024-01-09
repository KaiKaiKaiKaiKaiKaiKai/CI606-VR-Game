using UnityEngine;
using System.Threading.Tasks;

public class GetWeatherData : MonoBehaviour
{
    private APIClient apiClient;
    private Renderer objectRenderer;

    private void Start()
    {
        apiClient = GetComponent<APIClient>();

        GetData();

        // Get the Renderer component of the object this script is attached to
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            // Change the color to red
            objectRenderer.material.color = Color.red;
        }
        else
        {
            Debug.LogError("Renderer component not found on this object.");
        }
    }

    private async Task<WeatherData> GetData() {
        WeatherData weatherData = await apiClient.GetWeatherData();

        Debug.Log(weatherData.current.time);

        return weatherData;
    }
}
