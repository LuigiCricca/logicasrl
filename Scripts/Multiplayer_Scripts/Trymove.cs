using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Trymove : MonoBehaviour
{
   
    // Update is called once per frame
    


        Rigidbody rb;
        MeshRenderer mesh;
        RealtimeTransform rTransform;
        [SerializeField] Transform tube;

        RealtimeView realtimeView;
        Realtime realtime;


        private void Awake()
        {

            realtime = FindObjectOfType<Realtime>();

        
            realtime.didConnectToRoom += Prova;


        }


        // Update is called once per frame
        void Update() // mantain ownership while sleeping altrimenti ogni volta che faccio uno spostamento devo settare l'ownership
        {
           if (Input.GetKeyDown(KeyCode.P))
           {
                
                Debug.Log("Muovi oggetto");
            if (rTransform != null)
            {
                rTransform.RequestOwnership();
                transform.position = tube.position;
                
                

            }
           }
        if (Input.GetKey(KeyCode.M))
        {
            transform.position = Vector3.zero;
            Invoke("ClearAll", 1f);
            

        }
           
          
        
       
      
    
        }

    void ClearAll()
    {
        rTransform.ClearOwnership();
    }
        void Prova(Realtime real)
        {
            if (realtime.connected)
            {
                rb = GetComponent<Rigidbody>();
                mesh = GetComponent<MeshRenderer>();
                rTransform = GetComponent<RealtimeTransform>();
                rTransform.RequestOwnership();
            }

        }


}




