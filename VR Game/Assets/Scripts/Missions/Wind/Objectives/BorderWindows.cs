using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class BorderWindows : Objective
{
    private List<GameObject> planks;
    private GameObject tornado;
    private GameObject windows;
    private Windows windowsScript;

    public BorderWindows()
    {
        description = "Border up the windows with the planks";
    }

    public void Start()
    {
        planks = GameObject.FindGameObjectsWithTag("MissionPlank").ToList();
        tornado = GameObject.FindWithTag("MissionTornado");
        windows = GameObject.FindWithTag("MissionWindows");

        windowsScript = windows.GetComponent<Windows>();
        windowsScript.OnCollisionOccurred += HandlePlankCollision;
    }

    private void Update()
    {
        Transform tornadoTransform = tornado.GetComponent<Transform>();
        tornadoTransform.localPosition += new Vector3(0f, 0f, -10f * Time.deltaTime);

        if (tornadoTransform.localPosition.z <= 20)
        {
            bool success = false;

            Debug.Log(planks.Count);
            
            if (planks.Count == 0)
            {
                StopTornado();
                success = true;
            }

            CompleteObjective(success);
        };
    }

    private void HandlePlankCollision(GameObject otherObject)
    {
        if (!planks.Contains(otherObject)) return;

        planks.Remove(otherObject);

        XRGeneralGrabTransformer plankTransformer = otherObject.GetComponent<XRGeneralGrabTransformer>();
        XRGrabInteractable plankInteractable = otherObject.GetComponent<XRGrabInteractable>();
        Rigidbody plankRigidBody = otherObject.GetComponent<Rigidbody>();

        Destroy(plankTransformer);
        Destroy(plankInteractable);
        Destroy(plankRigidBody);
    }

    private void StopTornado()
    {
        ParticleSystem particleSystem = tornado.GetComponent<ParticleSystem>();

        ParticleSystem.MainModule main = particleSystem.main;
        main.loop = false;
    }
}
