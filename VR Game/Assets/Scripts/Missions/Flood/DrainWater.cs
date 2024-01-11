using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainWater : Mission
{
    public DrainWater() {
        this.description = "Use the bucket to remove the water";
        this.nextMission = () => gameObject.AddComponent<FloodComplete>();
    }
}
