using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class TubeLinkP2_New : MonoBehaviour
{
    [SerializeField] Transform tubeLink_P1;
    LinkP1_Component p1Tube;
    bool link_on = false;

    Vector3 forceDir = new Vector3(0, 1, -0.7f);
    Realtime realtime;
    RealtimeView realView;
    RealtimeTransform realTransform;


    private void Awake()
    {
        p1Tube = tubeLink_P1.GetComponent<LinkP1_Component>();
        link_on = false;
        realtime = FindObjectOfType<Realtime>();
        realtime.didConnectToRoom += ManageOwnership;
    }

    void ManageOwnership(Realtime real)
    {
        realTransform = GetComponent<RealtimeTransform>();
        realView = GetComponent<RealtimeView>();

        if (realtime.connected && realtime.clientID>=1)
        {

            realTransform.SetOwnership(PlayerSpawn_Multy.id_player2);
            realView.SetOwnership(PlayerSpawn_Multy.id_player2);
            Debug.Log($"Tube room 2 owned: ---- {realTransform.ownerIDInHierarchy}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!link_on && !realTransform.isOwnedLocallyInHierarchy)
        {
            link_on = true;

            if ((other.gameObject.tag == "Sphere") || (other.gameObject.tag == "Cube") || (other.gameObject.tag == "Pyram"))
            {
                StartCoroutine(CheckLink(other));
            }
            else
            {
                Debug.LogWarning("No obj found");
            }
        }
        else if ((other.gameObject.tag == "Sphere") || (other.gameObject.tag == "Cube") || (other.gameObject.tag == "Pyram"))
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.enabled = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
        }

    }

    private IEnumerator CheckLink (Collider other)
    {
        Debug.LogError("Coroutine p2 TUBE");
        string prefabName = other.gameObject.tag;
        other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        other.enabled = false;
        other.GetComponent<Rigidbody>().isKinematic = true;

        if (other.TryGetComponent<OVRGrabbable>(out OVRGrabbable grabbable) && !grabbable.isGrabbed)
        {
            p1Tube.ColliderState(false);

            GameObject linkObj = Realtime.Instantiate(prefabName, tubeLink_P1.position, Quaternion.identity, SetInstantiateOptions());
            linkObj.GetComponent<MeshRenderer>().enabled = false;

            yield return new WaitForSeconds(0.5f);

            RealtimeView viewObj = linkObj.GetComponent<RealtimeView>();
            RealtimeTransform transObj = linkObj.GetComponent<RealtimeTransform>();
            Rigidbody rbObj = linkObj.GetComponent<Rigidbody>();
            viewObj.SetOwnership(PlayerSpawn_Multy.id_player1);
            transObj.SetOwnership(PlayerSpawn_Multy.id_player1);
            
            yield return new WaitForSeconds(0.5f);
            Debug.LogWarning($"obj controllato da:{transObj.ownerIDInHierarchy}");

            linkObj.GetComponent<MeshRenderer>().enabled = true;
            rbObj.AddForce(forceDir * 3 , ForceMode.Impulse);

            yield return new WaitForSeconds(1f);
            Realtime.Destroy(other.gameObject);

            yield return new WaitForSeconds(3f);
            ResetCollider();
            
        }
        else
        {
            Debug.LogWarning("Oggetto grabbato");
        }

    }

    void ResetCollider()
    {
        Debug.LogWarning("Riattiva il collider tubo 2");
        p1Tube.ColliderState(true);
        link_on = false; 
        
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
}



