using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class RoofRealtimeComponent : RealtimeComponent< Roof_Model >
   
{
     Collider roofColl;
    
    private void Awake()
    {
         roofColl = GetComponent<Collider>();
        
    }
   
    protected override void OnRealtimeModelReplaced(Roof_Model previousModel, Roof_Model currentModel)
    {
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.roofEnabledDidChange -= EnabledDidChange;
        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.roofEnabled = roofColl.enabled;

            // Update the collider enabled to match the new model
            UpdateCollider();

            // Register for events so we'll know if the color changes later
            currentModel.roofEnabledDidChange += EnabledDidChange;
        }
    }

    void EnabledDidChange(Roof_Model model,bool value)
    {
        //Debug.LogWarning("did change fired");
        UpdateCollider();
    }
    void UpdateCollider()
    {
        roofColl.enabled = model.roofEnabled;
        Debug.Log($"Roof collider{model.roofEnabled}");
    }
    public void ColliderState(bool state)
    {
        model.roofEnabled = state;
    }
}
