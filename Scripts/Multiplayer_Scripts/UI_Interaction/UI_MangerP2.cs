using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class UI_MangerP2 : MonoBehaviour  
{
    Realtime realtime;

    private void Awake()
    {
        realtime = FindObjectOfType<Realtime>();
        realtime.didConnectToRoom += Connection;
       
    }

    private void Connection(Realtime real)
    {
       
        if (realtime.connected)
        {
            
            
            if (realtime.clientID<=0)
            {
                Debug.LogError($"client {realtime.clientID} disable hand ui component");
                //Invoke("DisableP1", 0.1f);
                

            }
            else if(realtime.clientID>0)
            {
                Debug.LogError("player 2 has control over UI");
            }
        }
    }

    void DisableP1()
    {
        var avatar = FindObjectOfType<RealtimeAvatar>();
        //var p2Component = avatar.rightHand.GetComponent<UIManager_P2_Component>();
        var lineRenderer = avatar.rightHand.GetComponent<LineRenderer>();
        var handInteraction = avatar.rightHand.GetComponent<Hand_UI_Multiplayer>();
        Destroy(handInteraction);
        //Destroy(lineRenderer);
        
    }

    
}
