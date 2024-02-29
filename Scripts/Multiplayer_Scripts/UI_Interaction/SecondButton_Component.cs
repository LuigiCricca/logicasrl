using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class SecondButton_Component : RealtimeComponent<SecondButtonModel>
{
    bool exitActivated;
    bool retryActivated;
    CanvasGroup canvas;
    private void Awake()
    {
        exitActivated = false;
        retryActivated = false;
        canvas = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
