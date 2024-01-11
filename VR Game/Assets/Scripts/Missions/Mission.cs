using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public Mission nextMission;
    public string prefix = "Objective: ";
    public string description;
    public bool completed = false;

    public Mission() { }
}
