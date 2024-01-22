using UnityEngine;

public class DrainWater : Objective
{
    private GameObject floodWater;
    private GameObject bucket;
    private GameObject rain;
    private FloodWater floodWaterScript;

    public DrainWater()
    {
        description = "Scoop the water out with the bucket";
    }

    public void Start()
    {
        floodWater = GameObject.FindWithTag("MissionFloodWater");
        bucket = GameObject.FindWithTag("MissionBucket");
        rain = GameObject.FindWithTag("MissionRain");

        floodWaterScript = floodWater.GetComponent<FloodWater>();
        floodWaterScript.OnCollisionOccurred += HandleBucketCollision;
    }

    private void Update()
    {
        Transform floodWaterTransform = floodWater.GetComponent<Transform>();
        floodWaterTransform.localScale += new Vector3(0f, 0.05f * Time.deltaTime, 0f);

        if (floodWaterTransform.localScale.y >= 2) {
            floodWaterScript.OnCollisionOccurred -= HandleBucketCollision;

            CompleteObjective(false);
        }
    }

    private void HandleBucketCollision(GameObject otherObject)
    {
        // Do something with the collided GameObject (e.g., check its tag)
        if  (otherObject != bucket) return;

        Transform floodWaterTransform = floodWater.GetComponent<Transform>();

        // Reduce the Y scale by 0.5f when a trigger event is received
        floodWaterTransform.localScale = new Vector3(
            floodWaterTransform.localScale.x,
            floodWaterTransform.localScale.y - (float)0.1,
            floodWaterTransform.localScale.z
        );

        if (floodWaterTransform.localScale.y <= 0) {
            floodWaterScript.OnCollisionOccurred -= HandleBucketCollision;

            StopRain();
            CompleteObjective(true);
        }
    }

    private void StopRain()
    {
        Transform childTransform = rain.transform.Find("RainFallParticleSystem");
        ParticleSystem particleSystem = childTransform.GetComponent<ParticleSystem>();

        ParticleSystem.MainModule main = particleSystem.main;
        main.loop = false;
    }
}
