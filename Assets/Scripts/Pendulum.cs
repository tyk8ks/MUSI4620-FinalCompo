using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] float frequency = 1f;
    [Range(0f,1f)]
    [SerializeField] float amplitude = 0.25f;
    float t;
    float prev_oscillation;
    float delta;
    float prev_delta;
    bool left_edge, right_edge, middle;

    // Update is called once per frame
    void FixedUpdate()
    {
        t += Time.deltaTime;
        //triangle function 
        float oscillation = ControlFunctions.Tri(t, frequency,0f) * amplitude ;
        
        //fake gravity
        float sign = Mathf.Sign(oscillation);
        //need to work with absolute value to avoid sqrt error
        oscillation = Mathf.Sqrt(Mathf.Abs(oscillation));
       //reapply sign
        oscillation *= sign;
        //added some smoothing to get rid of discontinuity at middle point
        oscillation = Mathf.Lerp(oscillation, prev_oscillation, 0.8f);
        //get the derivative of the motion
        delta = oscillation - prev_oscillation;
        
        //applly rotation
        transform.localRotation = Quaternion.Euler(0, 0, oscillation*180 );

        //stages (this would probably work better with a switch, I am just not that familiar with it...)
        if (Mathf.Sign(delta) != Mathf.Sign(prev_delta))
        {
            left_edge = right_edge = middle = false;
            if (delta > prev_delta)
                left_edge = true;
            else if (delta < prev_delta)
                right_edge = true;
            if (left_edge) Debug.Log("left");
            if (right_edge) Debug.Log("right");
            
        }
        else if (Mathf.Sign(oscillation) != Mathf.Sign(prev_oscillation))
        {
            left_edge = right_edge = middle = false;
            middle = true;
            if (middle) Debug.Log("center");
        }
            

        //update delta for derivative
        prev_delta = delta;
        //upate previous oscillaltion
        prev_oscillation = oscillation;
    }
}