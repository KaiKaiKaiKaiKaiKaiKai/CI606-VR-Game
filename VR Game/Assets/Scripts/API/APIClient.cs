using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System.Collections;
using System;

[System.Serializable]
public class IPData
{
    public string ip;
}

[System.Serializable]
public class LatLonData
{
    public string query;
    public string status;
    public string country;
    public string countryCode;
    public string region;
    public string regionName;
    public string city;
    public string zip;
    public float lat;
    public float lon;
    public string timezone;
    public string isp;
    public string org;
}

[System.Serializable]
public class WeatherData
{
    public float latitude;
    public float longitude;
    public float generationtime_ms;
    public int utc_offset_seconds;
    public string timezone;
    public string timezone_abbreviation;
    public float elevation;

    public CurrentUnits current_units;
    public Current current;

    [System.Serializable]
    public class CurrentUnits
    {
        public string time;
        public string interval;
        public string temperature_2m;
        public string is_day;
        public string rain;
        public string snowfall;
        public string windspeed_10m;
    }

    [System.Serializable]
    public class Current
    {
        public string time;
        public int interval;
        public float temperature_2m;
        public int is_day;
        public float rain;
        public float snowfall;
        public float windspeed_10m;
    }
}

public class APIClient : MonoBehaviour
{
    protected async Task<string> GetDataFromAPIAsync(string apiUrl)
    {
        // Create a task completion source to convert the Unity coroutine to a task.
        TaskCompletionSource<string> taskCompletionSource = new();

        StartCoroutine(GetDataFromAPI(taskCompletionSource, apiUrl));

        try
        {
            string data = await taskCompletionSource.Task;
            if (!string.IsNullOrEmpty(data))
            {
                Debug.Log("Received Data: " + data);
                return data;
            }
            else
            {
                Debug.LogWarning("No data received.");
                return "";
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error fetching data: " + e.Message);
            return null;
        }
    }

    public async Task<IPData> GetIPData()
    {
        string apiUrl = "https://api.ipify.org?format=json";
        string data = await GetDataFromAPIAsync(apiUrl);

        IPData ipData = JsonUtility.FromJson<IPData>(data);
        return ipData;
    }

    public async Task<LatLonData> GetLatLonData()
    {
        IPData ipData = await GetIPData();
        
        string apiUrl = $"http://ip-api.com/json/{ipData.ip}";
        string data = await GetDataFromAPIAsync(apiUrl);

        LatLonData latLonData = JsonUtility.FromJson<LatLonData>(data);
        return latLonData;
    }

    public async Task<WeatherData> GetWeatherData()
    {
        LatLonData latLonData = await GetLatLonData();

        string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latLonData.lat}&longitude={latLonData.lon}&current=temperature_2m,is_day,rain,snowfall,windspeed_10m&forecast_days=1";
        string data = await GetDataFromAPIAsync(apiUrl);

        WeatherData weatherData = JsonUtility.FromJson<WeatherData>(data);
        return weatherData;
    }

    private IEnumerator GetDataFromAPI(TaskCompletionSource<string> tcs, string apiUrl)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError
                || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
                tcs.SetResult(string.Empty); // Set the result to an error or an appropriate default value
            }
            else
            {
                string data = request.downloadHandler.text;
                tcs.SetResult(data); // Set the result to the API response data
            }
        }
    }
}
