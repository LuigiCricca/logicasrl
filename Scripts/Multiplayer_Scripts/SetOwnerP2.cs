using UnityEngine;
using Normal.Realtime;

public class SetOwnerP2 : MonoBehaviour
{

    RealtimeTransform _realtimeTransform;
    RealtimeView _realtimeView;
    Realtime realtime;

 
    bool owned;

    private void Awake() //---> potrebbe essere che questi vadano spostati in start oppure in onenable
    {
        _realtimeTransform = GetComponent<RealtimeTransform>();
        
        _realtimeView = GetComponent<RealtimeView>();
        
        realtime = FindObjectOfType<Realtime>();

        //realtime.didConnectToRoom += MyProperty;

        owned = false;
        //rb.isKinematic = true;


    }

    private void Update()
    {
        if(owned == false){
            if(FindObjectsOfType<RealtimeAvatar>().Length == 2){

                //player1Connected = true;
                _realtimeView.SetOwnership(1);
                _realtimeTransform.SetOwnership(1);
                owned = true;
            }

        }
        
    }


    void MyProperty(Realtime real)
    {
        // if (FindObjectsOfType<RealtimeAvatar>().Length >=1 {

        if (realtime.connected) 
        {
            if (FindObjectOfType<SetOwnerP1>().player1Connected)
            {
                //player1Connected = true;
                _realtimeView.SetOwnership(1);
                _realtimeTransform.SetOwnership(1);
                owned = true;
            }
           
                
        }
    }

    void SetPropertyFalse()
    {
        if (!owned) {
            MyProperty(realtime);
        }
        
        
    }
      
}
