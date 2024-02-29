using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class FirstUIButton_Component : RealtimeComponent<FirstButtonModel>
{
    bool startActivated;
    CanvasGroup canvas;
    bool closedPanel;

    private void Awake()
    {
        startActivated = false;
        closedPanel = false;
        canvas = GetComponent<CanvasGroup>();
    }
    protected override void OnRealtimeModelReplaced(FirstButtonModel previousModel, FirstButtonModel currentModel)
    {
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.startClickedDidChange -= StartButtonDidChange;
        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.startClicked = startActivated;

            // Update the collider enabled to match the new model
            UpdateBool();

            // Register for events so we'll know if the color changes later
            currentModel.startClickedDidChange += StartButtonDidChange;
        }
    }
    void StartButtonDidChange(FirstButtonModel model, bool value)
    {

        UpdateBool();
    }

    void UpdateBool()
    {
        startActivated = model.startClicked;
        Debug.Log($"start clicked {model.startClicked}");
    }
    public void StartClicked(bool state)
    {
        model.startClicked = state;

        if (state == true)
        {
            
            Debug.LogError("StartFade  sui pannelli start");
            //var roofColldier=FindObjectOfType<RoofRealtimeComponent>();
            //roofColldier.ColliderState(false);
            // start coroutine fade
            if (!closedPanel)
            {
                StartCoroutine(Fade());
            }

        }
        else
        {
            Debug.Log("primo pannello attivo");
        }
    }


    private IEnumerator Fade()
    {
        
        Debug.LogWarning("fade started");
        closedPanel = true;

        while (canvas.alpha > 0)
        {
            yield return new WaitForSeconds(0.01f);
            canvas.alpha = canvas.alpha - Time.deltaTime;
        }
        if (canvas.alpha <= 0)
        {
            gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            Realtime.Destroy(gameObject); // mi da errore perche' non posso distruggere uno scene view

        }



    }
}
