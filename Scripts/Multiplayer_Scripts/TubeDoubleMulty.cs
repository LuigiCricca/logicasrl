using System.Collections;

using UnityEngine;
using Normal.Realtime;

public class TubeDoubleMulty : MonoBehaviour
{
    //[SerializeField] GameObject[] interactables = new GameObject[3];

    AudioSource audio_source;

    RealtimeTransform r_transform;
    RealtimeView realtimeView;
    Realtime realtime;

    bool spawned;
    bool ok = false;

    GameObject droppedObj;

    
    Vector3 direction = new Vector3(0, 1, +0.7f);

    


    private void Awake()
    {

        audio_source = GetComponent<AudioSource>();
        realtime = FindObjectOfType<Realtime>();
        SetInstantiateOptions();

       // realtime.didConnectToRoom += SetTubeOwnership;

        

    }

    void Update()
    {

        if (realtime.connected && !ok)
        {
            ok = true;
            Invoke("SetOwnerP1", 5f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (realtimeView != null && !spawned && realtimeView.isOwnedLocallyInHierarchy) // se realtimeview non e' controllata in locale da player 1 non cercare di istanziare
        {
            if (other.gameObject.TryGetComponent(out OVRGrabbable m_grabbable))
            {
                if (!m_grabbable.isGrabbed) // se l'oggetto lanciato non e' grabbato 
                {
                    droppedObj = other.gameObject; // oggetto entrato nel trigger

                    if (!spawned)
                    {
                        spawned = true;

                        StartCoroutine(SpawnObjects(other));

                        Invoke("ResetSpawn", 5f);

                        //Invoke("DestroyObj", 5f);
                        
                    }
                }
            }
        }
    }

    private IEnumerator SpawnObjects(Collider other)
    {
 
        audio_source.Play();

        string prefabName = "";

        if (other.gameObject.CompareTag("Sphere"))
        {
            prefabName = "Sphere";
        }
        if (other.gameObject.CompareTag("Cube"))
        {
            prefabName = "Cube";
        }
        if (other.gameObject.CompareTag("Pyram"))
        {
            prefabName= "Pyram";
        }

        yield return new WaitForSeconds(0.1f);
        other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        other.gameObject.GetComponent<Collider>().enabled = false;
        other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(0.1f);

        GameObject firstInstance= Realtime.Instantiate(prefabName, transform.position, Quaternion.identity, SetInstantiateOptions());
        firstInstance.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        firstInstance.GetComponent<RealtimeTransform>().SetOwnership(PlayerSpawn_Multy.id_player1);
        firstInstance.GetComponent<RealtimeView>().SetOwnership(PlayerSpawn_Multy.id_player1);
        yield return new WaitForSeconds(0.5f);
        firstInstance.GetComponent<MeshRenderer>().enabled = true;
        firstInstance.GetComponent<Rigidbody>().AddForce(direction * 3f, ForceMode.Impulse);

        yield return new WaitForSeconds(0.5f);
        GameObject secondInstance = Realtime.Instantiate(prefabName, transform.position, Quaternion.identity, SetInstantiateOptions());
        secondInstance.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        secondInstance.GetComponent<RealtimeTransform>().SetOwnership(PlayerSpawn_Multy.id_player1);
        secondInstance.GetComponent<RealtimeView>().SetOwnership(PlayerSpawn_Multy.id_player1);
        yield return new WaitForSeconds(0.5f);
        secondInstance.GetComponent<MeshRenderer>().enabled = true;
        secondInstance.GetComponent<Rigidbody>().AddForce(direction * 3f, ForceMode.Impulse);

        yield return new WaitForSeconds(1f);
        Realtime.Destroy(other.gameObject);



        // Realtime.Destroy(other.gameObject);
    }


    void ResetSpawn()
    {
        spawned = false;

    }

    Realtime.InstantiateOptions SetInstantiateOptions()
    {
        Realtime.InstantiateOptions instantiateOptions = new Realtime.InstantiateOptions();
        instantiateOptions.destroyWhenLastClientLeaves = true;
        instantiateOptions.destroyWhenOwnerLeaves = true;
        instantiateOptions.ownedByClient = false;
        instantiateOptions.preventOwnershipTakeover = false;


        return instantiateOptions;
    }

    void SetTubeOwnership(Realtime real)
    {
        if (realtimeView == null && realtime.clientID == 0) // && findObjectsOfType<RealtimeAvatar>().Lenght
        {
           
            realtimeView = GetComponent<RealtimeView>();
            r_transform = GetComponent<RealtimeTransform>();
            //realtimeView.SetOwnership(1);
            realtimeView.RequestOwnership();
            r_transform.RequestOwnership();
            Debug.LogWarning($"Ownership del TUBO DOUBLE a: >>>> {r_transform.ownerIDInHierarchy} ");

        }
        else
        {
            Debug.Log("Non sono player 1 e non controllo il tubo double");
        }

    }

    void SetOwnerP1()
    {
        r_transform = GetComponent<RealtimeTransform>();
        realtimeView = GetComponent<RealtimeView>();
        if (r_transform != null && realtimeView!=null)
        {
            r_transform.SetOwnership(PlayerSpawn_Multy.id_player1);
            realtimeView.SetOwnership(PlayerSpawn_Multy.id_player1);
        }
        // r_transform.SetOwnership(0);
        //realtimeView.SetOwnership(0);
    }

    void DestroyObj()
    {
        Debug.LogError("Destroy obj in tube double ");
        Realtime.Destroy(droppedObj);
    }

}
