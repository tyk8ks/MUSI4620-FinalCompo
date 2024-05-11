using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SimplePatchCtrl : MonoBehaviour
{
    public LibPdInstance patch;
    public float speed = 1.0f; // Speed of the scale playback
    public float heightFactor = 0.5f; // Factor to control how much the object moves up

    private float ramp;
    private float t;
    private int scaleIndex = 0; // Current index in the scale
    private int[] scale = new int[] {60, 64, 67, 64, 65, 67, 69, 67, 65, 64, 62, 60, 67, 67, 69, 64, 65, 69, 67, 65, 64, 64, 67}; // C major scale

    void Update()
    {
        t += Time.deltaTime;

        // Update scaleIndex based on the speed and time, resetting when it exceeds the scale length
        if (t >= 1 / speed)
        {
            t = 0; // Reset time since we're moving to the next note
            scaleIndex = (scaleIndex + 1) % scale.Length; // Move to the next note in the scale

            int pitch = scale[scaleIndex];
            patch.SendMidiNoteOn(0, pitch, 80); // Send the MIDI note

            // Optional: Make the object move up according to the scale index
            float height = scaleIndex * heightFactor;
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
        }
    }
}