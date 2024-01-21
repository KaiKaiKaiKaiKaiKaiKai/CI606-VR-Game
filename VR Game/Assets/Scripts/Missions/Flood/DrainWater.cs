using UnityEngine;

public class DrainWater : Objective
{
    private GameObject floodWaterGO;
    private GameObject bucketGO;
    private FloodWater floodWaterScript;

    public DrainWater()
    {
        description = "Scoop the water out with the bucket";
    }

    public void Start()
    {
        floodWaterGO = GameObject.FindWithTag("MissionFloodWater");
        bucketGO = GameObject.FindWithTag("MissionBucket");
        floodWaterScript = floodWaterGO.GetComponent<FloodWater>();
        floodWaterScript.OnCollisionOccurred += HandleBucketCollision;
    }

    private void Update()
    {
        Transform floodWaterTransform = floodWaterGO.GetComponent<Transform>();
        floodWaterTransform.localScale += new Vector3(0f, 0.05f * Time.deltaTime, 0f);

        if (floodWaterTransform.localScale.y >= 2) {
            floodWaterScript.OnCollisionOccurred -= HandleBucketCollision;

            CompleteObjective(false);
        }
    }

    private void HandleBucketCollision(GameObject otherObject)
    {
        // Do something with the collided GameObject (e.g., check its tag)
        if  (otherObject != bucketGO) return;

        Transform floodWaterTransform = floodWaterGO.GetComponent<Transform>();

        // Reduce the Y scale by 0.5f when a trigger event is received
        floodWaterTransform.localScale = new Vector3(
            floodWaterTransform.localScale.x,
            floodWaterTransform.localScale.y - (float)0.1,
            floodWaterTransform.localScale.z
        );

        if (floodWaterTransform.localScale.y <= 0) {
            floodWaterScript.OnCollisionOccurred -= HandleBucketCollision;
            
            CompleteObjective(true);
        }
    }
}
