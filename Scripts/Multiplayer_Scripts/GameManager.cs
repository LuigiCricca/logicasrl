using UnityEngine;
using Normal.Realtime;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    Realtime realtime;
    public Realtime.InstantiateOptions instantiateOptions = new Realtime.InstantiateOptions();
    // [HideInInspector] public bool spawnComplete;
    public bool spawnComplete;
    Collider roofCollider;
    RealtimeView r_view;
    RealtimeTransform r_transform;


   [SerializeField] RoofRealtimeComponent roofComponent;
   [SerializeField] Door_RealtimeComponent doorComponent;
   
    


    private void Awake()
    {
        if(instance!=null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        realtime = FindObjectOfType<Realtime>();

       // realtime.didConnectToRoom += SpawnSphere;
        roofCollider = GameObject.Find("Roof_Linked").GetComponent<Collider>();
        realtime.didConnectToRoom += SetOwnerGameManager;
        r_view = GetComponent<RealtimeView>();
        r_transform = GetComponent<RealtimeTransform>();

        SetInstantiateOptions();
        
        

    }

    void SetOwnerGameManager(Realtime real)
    {
        if (realtime.connected && realtime.clientID >= 1)
        {
            PlayerSpawn_Multy.id_player2 = realtime.clientID;
            Debug.LogError(PlayerSpawn_Multy.id_player2);
            
            Debug.LogWarning($"Sei player 2 client id: {realtime.clientID}");
            this.gameObject.SetActive(true);
            r_transform.RequestOwnership();
            r_view.SetOwnership(PlayerSpawn_Multy.id_player2);

            if (r_view.isOwnedLocallyInHierarchy && r_transform.isOwnedLocallyInHierarchy)
            {
                Invoke("SpawnCoroutine", 5f);

            }


        }
        else
        {
            doorComponent.ColliderState(false);
            this.gameObject.SetActive(false);
            Debug.Log($"Sei  player1 client id: {realtime.clientID}");
        }
    }

    void SpawnCoroutine()
    {
        StartCoroutine(SphereSpawnRealtime(realtime));
    }


    IEnumerator SphereSpawnRealtime(Realtime realtime){

        yield return new WaitForSeconds(2f);
        //Vector3 sphereP1_Pos = new Vector3(0, 9.5f, 16);
        Vector3 sphereP2_Pos = new Vector3(0, 10.5f, 1.75f);
        
        if(realtime.connected){

            
            Debug.Log("istanzio due sfere");

            
            if (FindObjectsOfType<RealtimeAvatar>().Length > 1){


                GameObject sphereP2 = Realtime.Instantiate("Sphere_P2", sphereP2_Pos,Quaternion.identity, instantiateOptions);
                //MeshRenderer meshP2 = sphereP2.GetComponent<MeshRenderer>();
                //meshP2.enabled = false;
                
                RealtimeTransform sphereP2_RTransform = sphereP2.GetComponent<RealtimeTransform>();
                RealtimeView sphereP2_RView = sphereP2.GetComponent<RealtimeView>();

                yield return new WaitForSeconds(1f);

                
                Debug.Log("Set Owner Player2 on 2nd sphere");

                sphereP2_RTransform.SetOwnership(PlayerSpawn_Multy.id_player2);
                sphereP2_RTransform.RequestOwnership();

                sphereP2_RView.SetOwnership(PlayerSpawn_Multy.id_player2);
                sphereP2_RView.RequestOwnership();

                //yield return new WaitForSeconds(1f);
                //sphereP2.transform.SetPositionAndRotation(sphereP2_Pos, Quaternion.identity);

                    
                //meshP2.enabled = true;
               


            }

            
            //roofCollider.enabled = false;

            Invoke("SpawnComplete", 1f);
            Debug.Log("finito Spawn sfere");

            
        }
    }

  
    void SpawnComplete()
    {
        
        spawnComplete = true;
        /* if(GameManagerP1.instance.spawnComplete && spawnComplete)
         {
             roofCollider.enabled = false;
         }*/
        roofComponent.ColliderState(false);
        doorComponent.ColliderState(true);



    }
    public void SetInstantiateOptions()
    {
        instantiateOptions.destroyWhenLastClientLeaves = true;
        instantiateOptions.ownedByClient = false;
        instantiateOptions.preventOwnershipTakeover = false; 
        instantiateOptions.destroyWhenOwnerLeaves = true;

        //-----------------------------------------------------------  
        instantiateOptions.useInstance = null;


    }

  


}
