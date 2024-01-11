using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Get the Renderer component (assuming it's a MeshRenderer)
        Renderer renderer = GetComponent<Renderer>();

        // Check if the Renderer component is not null
        if (renderer != null)
        {
            if (GameManager.Instance.weatherData != null) {
                // Set the color to red
                renderer.material.color = Color.red;
            }
        }
        else
        {
            Debug.LogError("Renderer component not found on the GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
