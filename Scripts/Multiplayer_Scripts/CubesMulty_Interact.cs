using UnityEngine;
using Normal.Realtime;
using TMPro;

public class CubesMulty_Interact : MonoBehaviour
{
    RealtimeTransform _realtimeTransform;
    RealtimeView realtimeView;


    bool ok = false;
    [SerializeField] TextMeshProUGUI text;



  

    private void OnEnable()
    {

        _realtimeTransform = GetComponent<RealtimeTransform>();

        realtimeView = GetComponent<RealtimeView>();
       
        Invoke("EnableOwnerTakeover", 1f);


    }
    void EnableOwnerTakeover()
    {
        if (realtimeView.preventOwnershipTakeover == true)
        {
            realtimeView.preventOwnershipTakeover = false;
        }

        else if (_realtimeTransform.isOwnedLocallyInHierarchy || _realtimeTransform.isOwnedRemotelyInHierarchy)
        {
            text.text = _realtimeTransform.ownerIDInHierarchy.ToString();
        }

        else { Debug.LogError("takeover abilitato"); }


    }

    private void LateUpdate()
    {
        
            if (_realtimeTransform.isOwnedLocallyInHierarchy || _realtimeTransform.isOwnedRemotelyInHierarchy)
            {
               // text.text = _realtimeTransform.ownerIDInHierarchy.ToString();
            }
        
    }


    private void OnCollisionEnter(Collision other) {
        
        if(other.gameObject.tag == "AreaP1"){

          //  realtimeView.SetOwnership(PlayerSpawn_Multy.id_player1);
          //  _realtimeTransform.SetOwnership(PlayerSpawn_Multy.id_player1);

            //Light_Manager_Multy.sphereColor = true;

            // contatore oggetti sfera in scena all'interno dell'areaP1

            Light_Manager_Multy.player1_contCube++;

            if(Light_Manager_Multy.player1_contCube > 0){
                Light_Manager_Multy.player1_cubeGrounded = true;
            }
            
            

            
        }

        if(other.gameObject.tag == "AreaP2"){

          //  realtimeView.SetOwnership(1);
         //   _realtimeTransform.SetOwnership(1);
            //cambia colore Light_Manager_Multy

            //Light_Manager_Multy.sphereColor = true;

            // contatore oggetti sfera in scena all'interno dell'areaP1

            Light_Manager_Multy.player2_contCube++;
            

            if(Light_Manager_Multy.player2_contCube > 0){
                 Light_Manager_Multy.player2_cubeGrounded = true;
            }


            

            
        }


    }

    private void OnCollisionExit(Collision other) {
        
        if(other.gameObject.tag == "AreaP1"){

            Light_Manager_Multy.player1_contCube--;

            if(Light_Manager_Multy.player1_contCube == 0){
               Light_Manager_Multy.player1_cubeGrounded = false;
            }
        }

        if(other.gameObject.tag == "AreaP2"){

            Light_Manager_Multy.player2_contCube--;

            if(Light_Manager_Multy.player2_contCube == 0){
                Light_Manager_Multy.player2_cubeGrounded = false;
            }
        }
    }
   

}


/*
   private void OnTriggerEnter(Collider other)
   {
       if (other.gameObject.tag == "RoomP1")
       {
           realtimeView.SetOwnership(PlayerSpawn_Multy.id_player1);
           _realtimeTransform.SetOwnership(PlayerSpawn_Multy.id_player1);
       }
       if (other.gameObject.tag == "RoomP2")
       {
           realtimeView.SetOwnership(1);
           _realtimeTransform.SetOwnership(1);
       }
       if (other.gameObject.tag == "LinkP1")
       {
           if (!ok)
           {
               ok = true;
               Invoke("SetOwnershipSphereP2", 2f);
           }


       }
       if (other.gameObject.tag == "LinkP2")
       {
           Invoke("SetOwnershipSphereP1", 2f);

       }
}

   /*void SetOwnershipSphereP2()
   {
       _realtimeTransform = GetComponent<RealtimeTransform>();
       realtimeView = GetComponent<RealtimeView>();

       //_realtimeTransform.ClearOwnership();
       //realtimeView.ClearOwnership();


       _realtimeTransform.SetOwnership(1);
       realtimeView.SetOwnership(1);
       //  realtimeView.RequestOwnership();


       // _realtimeTransform.RequestOwnership();

       Debug.LogWarning("OwnerP2");
       ok = false;
   }

   void SetOwnershipSphereP1()
   {
       _realtimeTransform = GetComponent<RealtimeTransform>();
       realtimeView = GetComponent<RealtimeView>();

       // _realtimeTransform.ClearOwnership();
       // realtimeView.ClearOwnership();

       realtimeView.SetOwnership(0);
       _realtimeTransform.SetOwnership(0);


       Debug.LogWarning("OwnerP1");
   }
*/