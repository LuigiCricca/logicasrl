using UnityEngine;
using System.Collections;


public class Tube_Double : MonoBehaviour
{

    
    GameObject droppedObj;
    Vector3 spawner_Pos;
    Vector3 direction = new Vector3(0, 1, -0.7f);

    bool spawned;
    
    // bool spawnObject;
    AudioSource audio_source;

    


    private void Awake()
    {
        //fadeCoroutine = Fade();
        spawner_Pos = GetComponentInChildren<Transform>().position;
        audio_source = GetComponent<AudioSource>();
        spawned = false;
        


    }

    // quando un oggetto con iltag interactable entranel trigger cerca se e' grabbato
    //se non lo e' 

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.TryGetComponent(out OVRGrabbable m_grabbable))
        {
            if (!m_grabbable.isGrabbed) // se l'oggetto lanciato non e' grabbato 
            {
                droppedObj = other.gameObject; // l'oggetto droppato e' quello entrato nel trigger

               
                if (!spawned) // se non ha spawnato le copie
                {
                    spawned = true;
                    
                    Invoke("ResetSpawn", 2f);

                    // qui devo lasciare un po' di tempo dopo la distruzione de
                    // se obj_destroyed non e' true

                    //SpawnObjects(other);
                    StartCoroutine(SpawnObjects(other));
                }

            }
        }
    }

    private IEnumerator SpawnObjects(Collider other)
    {
       
        audio_source.Play();


            GameObject instances = Instantiate(droppedObj, spawner_Pos, Quaternion.identity); // recuerare l'istanza del prefab da un prefab manager
        other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        other.gameObject.GetComponent<Collider>().enabled = false;
        other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //Destroy(other.gameObject,0.0001f);
        if (instances.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(direction * 3f, ForceMode.Impulse);
           
        }
        yield return new WaitForSeconds(0.5f);
        GameObject instance2 = Instantiate(instances, spawner_Pos, Quaternion.identity);

           if( instance2.TryGetComponent(out Rigidbody rb2))
           {

                rb2.AddForce(direction * 3f , ForceMode.Impulse);
            

           }

    }

   
    void ResetSpawn()
    {
        spawned = false;
        //spawnObject = false;
    }
}