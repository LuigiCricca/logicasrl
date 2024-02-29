using UnityEngine;
using Normal.Realtime;
using System.Collections;


public class TubeSwapMulty : MonoBehaviour
{
    bool spawned;
    [SerializeField] GameObject[] interactables = new GameObject[3];
    Vector3 spawner_Pos;
    Vector3 direction = new Vector3(0, 1, -0.7f);

    AudioSource audio_source;

    RealtimeView realtimeView;
    Realtime realtime;
    Realtime.InstantiateOptions instantiateOptions=new Realtime.InstantiateOptions();

    private void Awake()
    {
        spawner_Pos = transform.position;
        audio_source = GetComponent<AudioSource>();
        realtime = FindObjectOfType<Realtime>();
        SetInstantiateOptions();

        realtime.didConnectToRoom += SetTubeOwnership;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (realtimeView != null &&  !spawned && realtimeView.isOwnedLocallyInHierarchy) // se realtimeview non e' controllata in locale da player 1 non cercare di istanziare
        {
            if (other.gameObject.TryGetComponent(out OVRGrabbable m_grabbable))
            {
                if (!m_grabbable.isGrabbed) // se l'oggetto lanciato non e' grabbato 
                {


                    if (!spawned)
                    {
                        Debug.Log("Spawna oggetto");
                        spawned = true;


                        SpawnObj(other);
                        Invoke("ResetSpawn", 1f);

                    }

                }
                else
                {
                    Debug.Log("L'oggetto e' grabbato");
                }
            }
        }

        else
        { Debug.Log("solo il player 2 puo' usare tube swap"); }


    }


   
   void SpawnObj(Collider other)
    {
        //Debug.Log("coroturtine partita");
        audio_source.Play();
        string prefabName = "";
        
        if (other.gameObject.CompareTag("Sphere"))
        {
            
            prefabName = "Cube";
            //yield return new WaitForSeconds(0.2f);
            //other.gameObject.SetActive(false);
            
        }
        else if(other.gameObject.CompareTag("Cube")) 
        {
            prefabName = "Pyram";
        }
        else if (other.gameObject.CompareTag("Pyram"))
        {
            prefabName = "Sphere";
        }
        other.gameObject.SetActive(false);
        
        GameObject swappedObj = Realtime.Instantiate(prefabName,transform.position,Quaternion.identity,instantiateOptions);

        //swappedObj.transform.position = spawner_Pos;
        
        //swappedObj.GetComponent<MeshRenderer>().enabled = false;
       
        if(swappedObj.TryGetComponent<RealtimeTransform>(out RealtimeTransform transform_obj)){

            if (!transform_obj.isOwnedLocallyInHierarchy)
            {
                Debug.LogWarning("L'oggetto non e' di tua proprieta'");
                transform_obj.RequestOwnership();
            }
            else if(transform_obj.isOwnedLocallyInHierarchy)
            {
                transform_obj.RequestOwnership();

                Debug.LogWarning("L'oggetto e' tuo, applica F'");
                if (swappedObj.TryGetComponent<Rigidbody>(out Rigidbody rb)){
                    rb.AddForce(direction *4f  , ForceMode.Impulse);
                }

            }
           
        }
        else { Debug.Log("obj non ha realtime trans"); }

   
        Realtime.Destroy(other.gameObject);


   }

    void SetInstantiateOptions()
    {
        instantiateOptions.destroyWhenLastClientLeaves = true;
        instantiateOptions.ownedByClient = true;
        instantiateOptions.preventOwnershipTakeover = false;
        instantiateOptions.destroyWhenOwnerLeaves = true;
        
      //-----------------------------------------------------------  
        instantiateOptions.useInstance = null;
        

    } 

    void ResetSpawn()
    {
        spawned = false;
       
    }
void SetTubeOwnership(Realtime real)
    {
        if (realtimeView == null && realtime.clientID == PlayerSpawn_Multy.id_player2) // && findObjectsOfType<RealtimeAvatar>().Lenght
        {
            //Debug.LogWarning("Ownership del tubo al player 2");
            realtimeView = GetComponent<RealtimeView>();
            //realtimeView.SetOwnership(1);
            realtimeView.RequestOwnership();
        }
        else
        {
            Debug.Log("Non sono player 2 e non controllo il tubo swap");
        }
        

    }
}

