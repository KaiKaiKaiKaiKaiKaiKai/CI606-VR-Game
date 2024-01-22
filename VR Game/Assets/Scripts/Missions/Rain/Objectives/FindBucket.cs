using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FindBucket : Objective
{
    private GameObject floodWater;
    private GameObject bucket;
    private GameObject rain;
    private XRGrabInteractable bucketInteractable;

    public FindBucket() {
        description = "Find a bucket to drain the water";
        nextObjective = () => gameObject.AddComponent<DrainWater>();
    }

    public void Start()
    {
        floodWater = GameObject.FindWithTag("MissionFloodWater");
        bucket = GameObject.FindWithTag("MissionBucket");
        rain = GameObject.FindWithTag("MissionRain");

        bucketInteractable = bucket.GetComponent<XRGrabInteractable>();
        bucketInteractable.selectEntered.AddListener(OnObjectPickedUp);

        UpdateBucketColor(Color.red);
    }

    private void Update()
    {
        Transform floodWaterTransform = floodWater.GetComponent<Transform>();
        floodWaterTransform.localScale += new Vector3(0f, 0.05f * Time.deltaTime, 0f);

        if (floodWaterTransform.localScale.y >= 2)
        {
            CompleteObjective(false);
        };
    }

    private void UpdateBucketColor(Color color)
    {
        Transform childTransform = bucket.transform.Find("Sack_01");

        // Get the Renderer component
        Renderer renderer = childTransform.GetComponent<Renderer>();

        renderer.material.color = color;
    }

    private void OnObjectPickedUp(SelectEnterEventArgs arg0)
    {
        UpdateBucketColor(Color.gray);

        CompleteObjective(true);
    }
}
