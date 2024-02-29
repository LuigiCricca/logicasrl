using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class LinkP2_Component : RealtimeComponent<LinkP2_Model>
{
    Collider tubeP2Collider;
    private void Awake()
    {
        tubeP2Collider = GetComponent<Collider>();
    }
    protected override void OnRealtimeModelReplaced(LinkP2_Model previousModel, LinkP2_Model currentModel)
    {
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.linkP2ActiveDidChange -= LinkP2DidChange;
        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.linkP2Active = tubeP2Collider.enabled;

            // Update the collider enabled to match the new model
            UpdateCollider();

            // Register for events so we'll know if the color changes later
            currentModel.linkP2ActiveDidChange += LinkP2DidChange;
        }
    }
    void LinkP2DidChange(LinkP2_Model model, bool value)
    {

        UpdateCollider();
    }

    void UpdateCollider()
    {
        tubeP2Collider.enabled = model.linkP2Active;
        Debug.LogWarning($"Tube P2 collider: {model.linkP2Active}");
    }
    public void ColliderState(bool state)
    {
        model.linkP2Active = state;
    }

}
