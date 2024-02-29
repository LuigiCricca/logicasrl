using UnityEngine;
using Normal.Realtime;
using TMPro;


public class SphereMulty_Interact : MonoBehaviour
{

    RealtimeTransform _realtimeTransform;

    RealtimeView realtimeView;
   
    Rigidbody rb;

    bool ok = false;
    [SerializeField]TextMeshProUGUI text;


    private void OnEnable()
    {
        
      
        _realtimeTransform = GetComponent<RealtimeTransform>();
       
        realtimeView = GetComponent<RealtimeView>();
        Invoke("EnableOwnerTakeover", 1f);
        
       
        rb = GetComponent<Rigidbody>();
        rb.drag = 0.7f;
        
  
    }
    void EnableOwnerTakeover()
    {
        if (realtimeView.preventOwnershipTakeover == true)
        {
            realtimeView.preventOwnershipTakeover = false;
        }
        else { Debug.Log($"takeover abilitato su {gameObject.name}"); }
        
    }

    void OwnerDebugger()
    {
      
        if (_realtimeTransform.isOwnedLocallyInHierarchy || _realtimeTransform.isOwnedRemotelyInHierarchy)
        {
            //text.text = _realtimeTransform.ownerIDInHierarchy.ToString();
        }
    }

    private void Update()
    {

        //InvokeRepeating("OwnerDebugger", 30, 5);
        OwnerDebugger();

    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "AreaP1")
        {


            Light_Manager_Multy.player1_contSphere++;


            if (Light_Manager_Multy.player1_contSphere > 0)
            {
                Light_Manager_Multy.player1_sphereGrounded = true;
            }

        }

        if (other.gameObject.tag == "AreaP2")
        {


            Light_Manager_Multy.player2_contSphere++;
            //Light_Manager_Multy.sphereColor = true;

            // contatore oggetti sfera in scena all'interno dell'areaP1

            if(Light_Manager_Multy.player2_contSphere > 0){

                Light_Manager_Multy.player2_sphereGrounded = true;
            }

        }


    }

    private void OnCollisionExit(Collision other)
    {

        if (other.gameObject.tag == "AreaP1")
        {

            Light_Manager_Multy.player1_contSphere--;

            if (Light_Manager_Multy.player1_contSphere <= 0)
            {
                Light_Manager_Multy.player1_sphereGrounded = false;
            }
        }

        if (other.gameObject.tag == "AreaP2")
        {

            Light_Manager_Multy.player2_contSphere--;

            if (Light_Manager_Multy.player2_contSphere <= 0)
            {
                Light_Manager_Multy.player2_sphereGrounded = false;
            }
        }

    }

  

}



/* private void OnTriggerEnter(Collider other)
   {
       if(other.gameObject.tag == "RoomP1")
       {
           realtimeView.SetOwnership(PlayerSpawn_Multy.id_player1);
           _realtimeTransform.SetOwnership(PlayerSpawn_Multy.id_player1);
       }
       if (other.gameObject.tag == "RoomP2")
       {
           realtimeView.SetOwnership(1);
           _realtimeTransform.SetOwnership(1);
       }

       if(other.gameObject.tag == "LinkP1")
       {
           if (!ok)
           {
               ok = true;
               Debug.LogWarning("Tocato tube link p1");
               Invoke("SetOwnershipSphereP2", 3f);
           }


       }
       if (other.gameObject.tag == "LinkP2")
       {

           Debug.LogWarning("Tocato tube link p2");
           Invoke("SetOwnershipSphereP1", 5f);

       }
   }

   void MeshRenderManager()
   {
       this.gameObject.GetComponent<MeshRenderer>().enabled = true;
   }

   void SetOwnershipSphereP2()
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

       //_realtimeTransform.ClearOwnership();
       //realtimeView.ClearOwnership();

       realtimeView.SetOwnership(0);
       _realtimeTransform.SetOwnership(0);


       Debug.LogWarning("OwnerP1");
   }*/

