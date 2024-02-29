using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using System;
public class HandTrackingGrabber : OVRGrabber
{

    private OVRHand m_hand;
    private float pinchThreshold = 0.6f;

    public static  Action onTriggerSphere;


    protected override void Start()
    {
        base.Start();
        m_hand = GetComponent<OVRHand>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        CheckIndexPinch();
    }

    void CheckIndexPinch()
    {
        float pinchStrength = m_hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);

        if (!m_grabbedObj && pinchStrength > pinchThreshold && m_grabCandidates.Count > 0)
        {
            GrabBegin();
        }
        else if (m_grabbedObj && !(pinchStrength > pinchThreshold))
        {
            GrabEnd();
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="RedSphere")
        {
            Invoke("RedSphereEvent", 1f);
        }
    }
    */

    private void RedSphereEvent()
    {
        if (onTriggerSphere != null)
            onTriggerSphere();
    }
}



