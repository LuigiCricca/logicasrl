using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Room1_LedComponent : RealtimeComponent<Led_Room1_Model>
{
    MeshRenderer[] ledRenderers_Room1;
    


    private void Awake()
    {
        ledRenderers_Room1 = new MeshRenderer[transform.childCount];
        //Debug.Log(ledRenderers_Room1.Length);
        for (int i = 0; i < ledRenderers_Room1.Length; i++)
        {
            if (transform.GetChild(i).GetComponent<MeshRenderer>() != null)
            {
                ledRenderers_Room1[i] = transform.GetChild(i).GetComponent<MeshRenderer>();
               
            }

        }

        
    }


    protected override void OnRealtimeModelReplaced(Led_Room1_Model previousModel, Led_Room1_Model currentModel)
    {
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.ledColorsDidChange -= ColorDidChange;
            SetColorRoom1(Color.white);

        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
            {
                foreach(MeshRenderer m_renderer in ledRenderers_Room1)
                {
                    currentModel.ledColors = m_renderer.material.color;
                }
            }
                

            // Update the mesh render to match the new model
            UpdateMeshRendererColor();

            // Register for events so we'll know if the color changes later
            currentModel.ledColorsDidChange += ColorDidChange;
        }
    }
    void ColorDidChange(Led_Room1_Model model, Color value)
    {
        UpdateMeshRendererColor();
    }
    private void UpdateMeshRendererColor()
    {
        foreach (MeshRenderer m_renderer in ledRenderers_Room1)
        {
            m_renderer.material.SetColor("_EmissionColor", model.ledColors);
        }
    }
    public void SetColorRoom1(Color color)
    {
        // Set the color on the model
        // This will fire the colorChanged event on the model, which will update the renderer for both the local player and all remote players.
        model.ledColors = color;
       
    }
}
