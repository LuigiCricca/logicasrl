using UnityEngine;
using Normal.Realtime;

public class SetOwnerP1 : MonoBehaviour
{

    RealtimeTransform _realtimeTransform;
    RealtimeView _realtimeView;
    Realtime realtime;

    bool isPlayer2;
    bool isPlayer1;
    public bool player1Connected;

    bool owned;

    private void Awake()
    {
        _realtimeTransform = GetComponent<RealtimeTransform>();
        
        _realtimeView = GetComponent<RealtimeView>();
        
        realtime = FindObjectOfType<Realtime>();

        //realtime.didConnectToRoom += MyProperty;
        //rb.isKinematic = true;

        owned = false;



    }
   
    void Update()
    {
       // Debug.Log($"Connected players: {FindObjectsOfType<RealtimeAvatar>().Length}");
       
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
       // if (FindObjectsOfType<RealtimeAvatar>().Length == 0{
            if (realtime.connected)
            {
            player1Connected = true;
                _realtimeView.SetOwnership(0);
                _realtimeTransform.SetOwnership(0);


            }
        //}
       
    }
      
}
