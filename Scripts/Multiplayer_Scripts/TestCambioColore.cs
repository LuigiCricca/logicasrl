using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;


public class TestCambioColore : MonoBehaviour
{
    //LedRealtimeComponent ledComponent;
    Room1_LedComponent ledRoom1_Component;
  
    private void Awake()
    {
        //ledComponent = GetComponent<LedRealtimeComponent>();
        ledRoom1_Component = GetComponent<Room1_LedComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //ledComponent.SetColor(Color.cyan);
            //Debug.Log("led stanza uno diventa ciano");
            ledRoom1_Component.SetColorRoom1(Color.green);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            //ledComponent.SetColor(Color.white);
            ledRoom1_Component.SetColorRoom1(Color.white);
        }

        
    }
}
