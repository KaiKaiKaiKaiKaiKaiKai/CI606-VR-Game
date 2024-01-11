using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBucket : Mission
{
    private GameObject bucket;

    public FindBucket() {
        this.description = "Find a bucket to drain the water";
    }
    public void Awake()
    {
        this.nextMission = gameObject.AddComponent<DrainWater>();
    }

    public void Start()
    {
        bucket = GameObject.Find("Bucket");

        // Get the Renderer component (assuming it's a MeshRenderer)
        Renderer renderer = bucket.GetComponent<Renderer>();

        // Check if the Renderer component is not null
        if (renderer != null)
        {
             // Set the color to red
              renderer.material.color = Color.red;
        }
        else
        {
            Debug.LogError("Renderer component not found on the GameObject.");
        }
    }
}
