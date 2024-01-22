using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FindLighter : Objective
{
    private GameObject lighter;
    private GameObject[] candles;
    private GameObject filter;
    private XRGrabInteractable lighterInteractable;

    public FindLighter()
    {
        description = "Find a lighter to light the candles";
        nextObjective = () => gameObject.AddComponent<LightCandles>();
    }

    public void Start()
    {
        lighter = GameObject.FindWithTag("MissionLighter");
        filter = GameObject.FindWithTag("MissionFilter");
        candles = GameObject.FindGameObjectsWithTag("MissionCandle");

        lighterInteractable = lighter.GetComponent<XRGrabInteractable>();
        lighterInteractable.selectEntered.AddListener(OnObjectPickedUp);

        foreach (GameObject candle in candles)
        {
            Transform childTransform = candle.transform.Find("Candle_fire");
            childTransform.gameObject.SetActive(false);
        }

        UpdateLighterColor(Color.red);
    }

    private void Update()
    {
        Renderer filterRenderer = filter.GetComponent<Renderer>();

        Color newColor = filterRenderer.material.color;

        newColor.a += 0.02f * Time.deltaTime;
        newColor.a = Mathf.Clamp01(newColor.a);

        filterRenderer.material.color = newColor;

        if (newColor.a == 1)
        {
            CompleteObjective(false);
        };
    }

    private void UpdateLighterColor(Color color)
    {
        Transform childTransform = lighter.transform.Find("Lighter");

        // Get the Renderer component
        Renderer renderer = childTransform.GetComponent<Renderer>();

        renderer.material.color = color;
    }

    private void OnObjectPickedUp(SelectEnterEventArgs arg0)
    {
        UpdateLighterColor(Color.gray);

        CompleteObjective(true);
    }
}
