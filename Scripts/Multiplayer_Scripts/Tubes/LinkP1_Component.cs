using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class LinkP1_Component : RealtimeComponent<LinkP1_Model> 
{
    Collider tubeP1Collider;
    private void Awake()
    {
        tubeP1Collider = GetComponent<Collider>();
    }
    protected override void OnRealtimeModelReplaced(LinkP1_Model previousModel, LinkP1_Model currentModel)
    {
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.linkP1ActiveDidChange -= LinkP1DidChange;
        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.linkP1Active = tubeP1Collider.enabled;

            // Update the collider enabled to match the new model
            UpdateCollider();

            // Register for events so we'll know if the color changes later
            currentModel.linkP1ActiveDidChange += LinkP1DidChange;
        }
    }
    void LinkP1DidChange(LinkP1_Model model, bool value)
    {

        UpdateCollider();
    }

    void UpdateCollider()
    {
        tubeP1Collider.enabled = model.linkP1Active;
        Debug.Log($"tube p1 collider {model.linkP1Active}");
    }
    public void ColliderState(bool state)
    {
        model.linkP1Active = state;
    }
}
