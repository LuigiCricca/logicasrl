using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class GameManagerP1 : MonoBehaviour
{
    public static GameManagerP1 instance;

    Realtime realtime;
    Realtime.InstantiateOptions instantiateOptions = new Realtime.InstantiateOptions();

    //[HideInInspector] public bool spawnComplete;

   
    RealtimeView r_view;
    RealtimeTransform r_transform;


    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        realtime = FindObjectOfType<Realtime>();

        realtime.didConnectToRoom += SetOwnerGameManager;
        r_view = GetComponent<RealtimeView>();
        r_transform = GetComponent<RealtimeTransform>();

        
    }


    void SetOwnerGameManager(Realtime real)
    {
        if (realtime.connected && realtime.clientID == 0)
        {
            PlayerSpawn_Multy.id_player1 = realtime.clientID;
            Debug.LogError(PlayerSpawn_Multy.id_player1);

            Debug.LogWarning($"Sei player 1 client id: {realtime.clientID}");
            this.gameObject.SetActive(true);
            r_transform.RequestOwnership();
            r_view.SetOwnership(PlayerSpawn_Multy.id_player1);

            if (r_view.isOwnedLocallyInHierarchy && r_transform.isOwnedLocallyInHierarchy)
            {
                Invoke("SpawnCoroutine", 5f);

            }


        }
        else
        {
            this.gameObject.SetActive(false);
            Debug.Log($"Sei  player2 client id: {realtime.clientID}");
        }
    }


    void SpawnCoroutine()
    {
        StartCoroutine(SphereSpawnRealtime(realtime));
    }


    IEnumerator SphereSpawnRealtime(Realtime realtime)
    {

        Vector3 sphereP1_Pos = new Vector3(0, 10.5f, 16);
        
        if (realtime.connected)
        {
            Debug.LogError(PlayerSpawn_Multy.id_player1);
            Debug.Log("istanzio due sfere");

            if (FindObjectsOfType<RealtimeAvatar>().Length >= 1)
            {
                GameObject sphereP1 = Realtime.Instantiate("Sphere_P1", sphereP1_Pos,Quaternion.identity, GameManager.instance.instantiateOptions);
               // MeshRenderer meshP1 = sphereP1.GetComponent<MeshRenderer>();
               // meshP1.enabled = false;
                
                RealtimeTransform sphereP1_RTransform = sphereP1.GetComponent<RealtimeTransform>();
                RealtimeView sphereP1_RView = sphereP1.GetComponent<RealtimeView>();


                yield return new WaitForSeconds(1f);

                sphereP1_RTransform.RequestOwnership();
                sphereP1_RView.RequestOwnership();


                Debug.Log("Set Owner Player1 on first sphere");

                //yield return new WaitForSeconds(0.5f);
                //sphereP1.transform.SetPositionAndRotation(sphereP1_Pos, Quaternion.identity);

              
                ;


                //yield return new WaitForSeconds(0.5f);

                //meshP1.enabled = true;
                
                
            }
 
            Debug.Log("SpawnComplete");


        }
    }

    


 


}
