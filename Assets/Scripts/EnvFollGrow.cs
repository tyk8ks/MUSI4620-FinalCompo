using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvFollGrow : MonoBehaviour
{
    /// The PD patch we are going to listen to.
    public LibPdInstance pdPatch;

    /// The Transform of the GameObject we are going to scale.
    public Transform scalableObject;

    /// The base scale of the object. It's a good idea to set this to the object's initial scale.
    public Vector3 baseScale = new Vector3(1f, 1f, 1f);

    /// The maximum additional scale applied to the object based on the amplitude.
    public float maxScaleFactor = 0.05f; // This controls how much the object will grow.

    /// We need to tell pdPatch which PD send object we want to listen to.
    void Start()
    {
        // Bind to the named send object.
        pdPatch.Bind("AmplitudeEnvelope");
    }

    /// Clean up after ourselves and unbind from the AmplitudeEnvelope object.
    void OnDestroy()
    {
        pdPatch.UnBind("AmplitudeEnvelope");
    }

    /// Our receive function. This will be called whenever a bound send object
    /// in our PD patch fires.
    public void FloatReceive(string sender, float value)
    {
        // This function will get called for *every* Float event sent by our
        // patch, so we need to make sure we're only acting on the
        //*AmplitudeEnvelope* event that we're actually interested in.
        if (sender == "AmplitudeEnvelope")
        {
            // Calculate the additional scale based on the amplitude.
            float scaleFactor = 1 + value * maxScaleFactor;

            // Apply the new scale to the object, based on its baseScale.
            scalableObject.localScale = new Vector3(baseScale.x * scaleFactor,
                                                    baseScale.y * scaleFactor,
                                                    baseScale.z * scaleFactor);
        }
    }
}
