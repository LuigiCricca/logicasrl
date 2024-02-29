using UnityEngine;
using Normal.Realtime;      

public class Door_RealtimeComponent : RealtimeComponent<Door_Model>
{
    Collider doorCollider;

    private void Awake()
    {
        doorCollider = GetComponent<Collider>();
      

    }
    protected override void OnRealtimeModelReplaced(Door_Model previousModel, Door_Model currentModel)
    {
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.doorActiveDidChange -= EnabledDidChange;
        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.doorActive = doorCollider.enabled;

            // Update the collider enabled to match the new model
            UpdateCollider();

            // Register for events so we'll know if the color changes later
            currentModel.doorActiveDidChange += EnabledDidChange;
        }
    }
    void EnabledDidChange(Door_Model model, bool value)
    {
       
        UpdateCollider();
    }

    void UpdateCollider()
    {
        doorCollider.enabled = model.doorActive;
        Debug.Log($"Door collider{model.doorActive}");
    }
    public void ColliderState(bool state)
    {
        model.doorActive = state;
    }

}
