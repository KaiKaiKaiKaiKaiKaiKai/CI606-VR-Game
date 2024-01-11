using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FindBucket : Mission
{
    private GameObject bucketGO;
    private XRGrabInteractable bucketInteractable;

    public FindBucket() {
        this.description = "Find a bucket to drain the water";
        this.nextMission = () => gameObject.AddComponent<DrainWater>();
    }

    public void Start()
    {
        this.bucketGO = GameObject.FindWithTag("MissionBucket");
        this.bucketInteractable = bucketGO.GetComponent<XRGrabInteractable>();
        this.bucketInteractable.selectEntered.AddListener(OnObjectPickedUp);

        this.UpdateBucketColor(Color.red);
    }

    private void UpdateBucketColor(Color color)
    {
        // Get the Renderer component
        Renderer renderer = this.bucketGO.GetComponent<Renderer>();

        renderer.material.color = color;
    }

    private void OnObjectPickedUp(SelectEnterEventArgs arg0)
    {
        this.UpdateBucketColor(Color.gray);

        this.CompleteMission();
    }
}
