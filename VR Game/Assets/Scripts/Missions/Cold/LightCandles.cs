using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightCandles : Objective
{
    private List<GameObject> candles;
    private GameObject lighter;
    private GameObject filter;
    private Windows[] candleScripts;
    private float filterSpeed;

    public LightCandles()
    {
        description = "Light the candles for heat";
    }

    public void Start()
    {
        lighter = GameObject.FindWithTag("MissionLighter");
        filter = GameObject.FindWithTag("MissionFilter");
        candles = GameObject.FindGameObjectsWithTag("MissionCandle").ToList();
        candleScripts = new Windows[candles.Count()];
        filterSpeed = 0.05f;

        foreach (GameObject candle in candles)
        {
            Windows candleScript = candle.GetComponent<Windows>();
            candleScript.OnCollisionOccurred += (otherObject) => HandleLighterCollision(candle, otherObject);

            candleScripts.Append(candleScript);
        }
    }

    private void Update()
    {
        Renderer filterRenderer = filter.GetComponent<Renderer>();

        Color newColor = filterRenderer.material.color;

        newColor.a += filterSpeed * Time.deltaTime;
        newColor.a = Mathf.Clamp01(newColor.a);

        if (newColor.a == 1)
        {
            CompleteObjective(false);
        }
        else if (newColor.a <= 0.02)
        {
            CompleteObjective(true);
        };

        filterRenderer.material.color = newColor;
    }

    private void HandleLighterCollision(GameObject candle, GameObject otherObject)
    {
        if (otherObject != lighter) return;

        candles.Remove(candle);

        Transform childTransform = candle.transform.Find("Candle_fire");
        childTransform.gameObject.SetActive(true);

        if (candles.Count == 0)
        {
            filterSpeed = -0.2f;
        }
    }
}
