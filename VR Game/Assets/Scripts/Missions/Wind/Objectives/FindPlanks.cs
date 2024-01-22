using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FindPlanks : Objective
{
    private GameObject[] planks;
    private GameObject tornado;

    public FindPlanks()
    {
        description = "Find planks to border windows";
        nextObjective = () => gameObject.AddComponent<BorderWindows>();
    }

    public void Start()
    {
        planks = GameObject.FindGameObjectsWithTag("MissionPlank");
        tornado = GameObject.FindWithTag("MissionTornado");

        foreach (GameObject plank in planks)
        {
            XRGrabInteractable plankInteractable = plank.GetComponent<XRGrabInteractable>();
            plankInteractable.selectEntered.AddListener(OnObjectPickedUp);
            
            UpdatePlankColor(Color.red, plank);
        }
    }

    private void Update()
    {
        Transform tornadoTransform = tornado.GetComponent<Transform>();
        tornadoTransform.localPosition += new Vector3(0f, 0f, -10f * Time.deltaTime);

        if (tornadoTransform.localPosition.z <= 20)
        {
            CompleteObjective(false);
        };
    }

    private void UpdatePlankColor(Color color, GameObject plank)
    {
        // Get the Renderer component
        Renderer renderer = plank.GetComponent<Renderer>();

        renderer.material.color = color;
    }

    private void OnObjectPickedUp(SelectEnterEventArgs arg0)
    {
        foreach (GameObject plank in planks)
        {
            UpdatePlankColor(new Color(0.7019608f, 0.5411765f, 0.3176471f), plank);
        }

        CompleteObjective(true);
    }
}
