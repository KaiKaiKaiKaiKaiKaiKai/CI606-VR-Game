using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FindBucket : Mission
{
    private GameObject floodWaterGO;
    private GameObject bucketGO;
    private XRGrabInteractable bucketInteractable;

    public FindBucket() {
        description = "Find a bucket to drain the water";
        nextMission = () => gameObject.AddComponent<DrainWater>();
    }

    public void Start()
    {
        floodWaterGO = GameObject.FindWithTag("MissionFloodWater");
        bucketGO = GameObject.FindWithTag("MissionBucket");
        bucketInteractable = bucketGO.GetComponent<XRGrabInteractable>();
        bucketInteractable.selectEntered.AddListener(OnObjectPickedUp);

        UpdateBucketColor(Color.red);
    }

    private void Update()
    {
        Transform floodWaterTransform = floodWaterGO.GetComponent<Transform>();
        floodWaterTransform.localScale += new Vector3(0f, 0.05f * Time.deltaTime, 0f);

        if (floodWaterTransform.localScale.y >= 2)
        {
            nextMission = () => gameObject.AddComponent<FloodFailed>();
            CompleteMission();
        };
    }

    private void UpdateBucketColor(Color color)
    {
        Transform childTransform = bucketGO.transform.Find("Sack_01");

        // Get the Renderer component
        Renderer renderer = childTransform.GetComponent<Renderer>();

        renderer.material.color = color;
    }

    private void OnObjectPickedUp(SelectEnterEventArgs arg0)
    {
        UpdateBucketColor(Color.gray);

        CompleteMission();
    }
}
