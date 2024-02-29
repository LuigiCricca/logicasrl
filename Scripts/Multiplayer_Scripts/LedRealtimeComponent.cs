using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class LedRealtimeComponent : RealtimeComponent<SingleLedModel>
{
    MeshRenderer m_renderer;
    //Realtime _realtime;
    private void Awake()
    {
        m_renderer = GetComponent<MeshRenderer>();
        
        //_realtime = FindObjectOfType<Realtime>();
    }
   
    protected override void OnRealtimeModelReplaced(SingleLedModel previousModel, SingleLedModel currentModel)
    {
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.ledColorDidChange -= ColorDidChange;
            
        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.ledColor = m_renderer.material.color;

            // Update the mesh render to match the new model
            UpdateMeshRendererColor();

            // Register for events so we'll know if the color changes later
            currentModel.ledColorDidChange += ColorDidChange;
        }
    }

    void ColorDidChange(SingleLedModel model, Color value)
    {
        UpdateMeshRendererColor();
    }


    private void UpdateMeshRendererColor()
    {
        m_renderer.material.SetColor("_EmissionColor", model.ledColor);
    }

    public void SetColor(Color color)
    {
        // Set the color on the model
        // This will fire the colorChanged event on the model, which will update the renderer for both the local player and all remote players.
        model.ledColor = color;
    }

    private void ResetColor()
    {
        model.ledColor = Color.white;
    }
}
