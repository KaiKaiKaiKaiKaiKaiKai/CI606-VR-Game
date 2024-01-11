using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainWater : Mission
{
    public DrainWater() {
        this.description = "Use the bucket to remove the water";
    }

    public void Awake()
    {
        this.nextMission = gameObject.AddComponent<FloodComplete>();
    }
}
