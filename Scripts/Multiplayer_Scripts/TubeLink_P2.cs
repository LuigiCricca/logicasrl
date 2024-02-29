using UnityEngine;
using Normal.Realtime;

using System.Collections;

public class TubeLink_P2 : MonoBehaviour
{
    bool ok = false;

    Realtime realtime;
    RealtimeTransform r_transform;
    RealtimeView r_view;
    [SerializeField] Transform tubeLink_P1;
    LinkP1_Component p1Tube;

    bool link_on = false;

    Vector3 direction = new Vector3(0, 1, -0.7f);
    private void Awake()
    {
        realtime = FindObjectOfType<Realtime>();
        p1Tube = tubeLink_P1.GetComponent<LinkP1_Component>();
        realtime.didConnectToRoom += ManageOwnership;
        //tubeP1_Collider = tubeLink_P1.GetComponent<Collider>();
        
    }

    void ManageOwnership(Realtime real)
    {
       
            r_transform = GetComponent<RealtimeTransform>();
            r_view = GetComponent<RealtimeView>();
        


        if (realtime.connected && FindObjectsOfType<RealtimeAvatar>().Length >= 2)
        {
            Debug.LogWarning($"Your client id is: {realtime.clientID}");
            r_transform.SetOwnership(PlayerSpawn_Multy.id_player2);
            r_view.SetOwnership(PlayerSpawn_Multy.id_player2);
            //r_transform.RequestOwnership();
            //r_view.RequestOwnership();
            Debug.Log($"Tube room 2 owned: {r_transform.isOwnedLocallyInHierarchy}");
        }
    }

    private void Update()
    {
        if(FindObjectsOfType<RealtimeAvatar>().Length > 1)
        {
            if (!ok)
            {
                Invoke("SetOwnerP2", 5f);
              //  realtime.didConnectToRoom += ManageOwnership;
                ok = true;
               // Debug.Log(r_transform.ownerIDInHierarchy);
            }
            
            
        }
           
    }

    void SetOwnerP2()
    {
        r_transform.SetOwnership(PlayerSpawn_Multy.id_player2);
        r_view.SetOwnership(PlayerSpawn_Multy.id_player2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!link_on)
        {
            link_on = true;
            StartCoroutine(CheckLink(other));
           

        }
        
    }

    private IEnumerator CheckLink(Collider other)
    {


        if (other.TryGetComponent<OVRGrabbable>(out OVRGrabbable grabbable) && !grabbable.isGrabbed)
        {
            //p1Tube.ColliderState(false);

            if ((other.gameObject.tag == "Sphere") || (other.gameObject.tag == "Cube") || (other.gameObject.tag == "Pyram"))
            {


                Debug.LogWarning($"oggetto Inserito in {this.gameObject.name}");
                if (other.gameObject.TryGetComponent<RealtimeTransform>(out RealtimeTransform transform_obj))
                {
                    RealtimeView view_obj = other.gameObject.GetComponent<RealtimeView>();   //Debug.Log($"Oggetto controllato? {transform_obj.isOwnedLocallyInHierarchy}\n owner: {transform_obj.ownerIDInHierarchy}");
                    view_obj.preventOwnershipTakeover = false;

                   // if (transform_obj.isOwnedLocallyInHierarchy)
                   // {
                        //
                        p1Tube.ColliderState(false);
                        //

                        if (other.TryGetComponent<Transform>(out Transform trans) && other.TryGetComponent<MeshRenderer>(out MeshRenderer renderer))
                        {
                            yield return new WaitForSeconds(0.2f);
                            renderer.enabled = false;

                            yield return new WaitForSeconds(1);
                            trans.position = tubeLink_P1.position;

                            renderer.enabled = true;

                            Debug.LogWarning("sposta l'oggetto");
                            // clear ownership
                            if (other.TryGetComponent<Rigidbody>(out Rigidbody rb))
                            {
                                rb.AddForce(direction * 3f, ForceMode.Impulse);

                            }

                            yield return new WaitForSeconds(1f);

                            transform_obj.SetOwnership(PlayerSpawn_Multy.id_player1);
                            view_obj.SetOwnership(PlayerSpawn_Multy.id_player1);
                            Debug.LogWarning($"Oggetto inserito controllato da: {transform_obj.ownerIDInHierarchy}");

                            yield return new WaitForSeconds(3);
                            ResetTubeCollider();

                        }
                    //}

                }
                else { Debug.Log("obj non ha realtime trans"); }


            }
        }
        else
        {
            Debug.Log("L'oggetto e' grabbato");
        }
    }


    void ResetTubeCollider()
    {
        //tubeP1_Collider.enabled = true;
        p1Tube.ColliderState(true);
        link_on = false;

        
    }
}
