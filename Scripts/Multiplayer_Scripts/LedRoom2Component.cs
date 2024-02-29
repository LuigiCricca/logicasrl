using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
public class LedRoom2Component : RealtimeComponent<Led_Room2_Model>
{
    MeshRenderer[] ledRenderers_Room2;
    private void Awake()
    {
        ledRenderers_Room2 = new MeshRenderer[transform.childCount];
       // Debug.Log(ledRenderers_Room2.Length);
        for (int i = 0; i < ledRenderers_Room2.Length; i++)
        {
            if (transform.GetChild(i).GetComponent<MeshRenderer>() != null)
            {
                ledRenderers_Room2[i] = transform.GetChild(i).GetComponent<MeshRenderer>();

            }

        }
    }
     protected override void OnRealtimeModelReplaced(Led_Room2_Model previousModel, Led_Room2_Model currentModel)
     {
         if (previousModel != null)
         {
             // Unregister from events
             previousModel.ledP2ColorsDidChange -= ColorDidChange;
             SetColorRoom2(Color.white);

         }

         if (currentModel != null)
         {
             // If this is a model that has no data set on it, populate it with the current mesh renderer color.
             if (currentModel.isFreshModel)
             {
                 foreach (MeshRenderer m_renderer in ledRenderers_Room2)
                 {
                     currentModel.ledP2Colors = m_renderer.material.color;
                 }
             }


             // Update the mesh render to match the new model
             UpdateMeshRendererColor();

             // Register for events so we'll know if the color changes later
             currentModel.ledP2ColorsDidChange += ColorDidChange;
         }
     }
    void ColorDidChange(Led_Room2_Model model, Color value)
    {
        UpdateMeshRendererColor();
    }
    private void UpdateMeshRendererColor()
    {
        foreach (MeshRenderer m_renderer in ledRenderers_Room2)
        {
            m_renderer.material.SetColor("_EmissionColor", model.ledP2Colors);
        }
    }
    public void SetColorRoom2(Color color)
    {
        // Set the color on the model
        // This will fire the colorChanged event on the model, which will update the renderer for both the local player and all remote players.
        model.ledP2Colors = color;

    }
}
