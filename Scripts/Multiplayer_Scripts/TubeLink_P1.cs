using UnityEngine;
using Normal.Realtime;
using System.Collections;


public class TubeLink_P1 : MonoBehaviour
{
    Realtime realtime;
    RealtimeTransform r_transform;
    RealtimeView r_view;
    [SerializeField] Transform tubeLink_P2;
    //Collider tubeP2_Collider;
    LinkP2_Component p2Tube;

    Vector3 direction = new Vector3(0, 1, +0.7f);

    bool link_on = false;
    

    // Start is called before the first frame update
    private void Awake()
    {
        realtime = FindObjectOfType<Realtime>();
        //tubeP2_Collider = tubeLink_P2.GetComponent<Collider>();
        p2Tube = tubeLink_P2.GetComponent<LinkP2_Component>();
        realtime.didConnectToRoom += ManageOwnership;
       
    }

    void ManageOwnership(Realtime real)
    {
        r_transform = GetComponent<RealtimeTransform>();
        r_view = GetComponent<RealtimeView>();

        if (realtime.connected && FindObjectsOfType<RealtimeAvatar>().Length < 2)
        {

            r_transform.SetOwnership(PlayerSpawn_Multy.id_player1);
            r_view.SetOwnership(PlayerSpawn_Multy.id_player1);
            Debug.Log($"Tube room 1 owned: {r_transform.isOwnedLocallyInHierarchy}");
        }
    }


    private void OnTriggerEnter(Collider other) // se non funziona provare con coroutine, verificare che effettivamente l'oggetto non sia ownato
    {
        if (!link_on)
        {
            
            //CheckLink(other);
            link_on = true;
            StartCoroutine(CheckLink(other));

        }

    }
    
    private IEnumerator CheckLink(Collider other)
    {

        if (other.TryGetComponent<OVRGrabbable>(out OVRGrabbable grabbable) && !grabbable.isGrabbed)
        {
            //p2Tube.ColliderState(false);

            if ((other.gameObject.tag == "Sphere") || (other.gameObject.tag == "Cube") || (other.gameObject.tag == "Pyram"))
            {
                

                Debug.LogWarning($"oggetto Inserito in {this.gameObject.name}");
                if (other.gameObject.TryGetComponent<RealtimeTransform>(out RealtimeTransform transform_obj))
                {
                 RealtimeView view_obj=other.gameObject.GetComponent<RealtimeView>();   //Debug.Log($"Oggetto controllato? {transform_obj.isOwnedLocallyInHierarchy}\n owner: {transform_obj.ownerIDInHierarchy}");
                  view_obj.preventOwnershipTakeover=false;

                    //if (transform_obj.isOwnedLocallyInHierarchy)
                    //{
                        //
                        p2Tube.ColliderState(false);
                        //

                        if (other.TryGetComponent<Transform>(out Transform trans) && other.TryGetComponent<MeshRenderer>(out MeshRenderer renderer))
                        {
                            yield return new WaitForSeconds(0.2f);
                            renderer.enabled = false;

                            yield return new WaitForSeconds(1);
                            trans.position = tubeLink_P2.position;

                            renderer.enabled = true;

                            Debug.LogWarning("sposta l'oggetto");
                            // clear ownership
                            if (other.TryGetComponent<Rigidbody>(out Rigidbody rb))
                            {
                                rb.AddForce(direction * 3f, ForceMode.Impulse);

                            }

                            yield return new WaitForSeconds(1f);

                            transform_obj.SetOwnership(PlayerSpawn_Multy.id_player2);
                            view_obj.SetOwnership(PlayerSpawn_Multy.id_player2);
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
        //tubeP2_Collider.enabled = true;
        p2Tube.ColliderState(true);
        link_on = false;
    }


}
