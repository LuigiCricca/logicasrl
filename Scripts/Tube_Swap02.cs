using UnityEngine;


public class Tube_Swap02 : MonoBehaviour
{
    bool spawned;
    [SerializeField] GameObject[] interactables = new GameObject[3];
    Vector3 spawner_Pos;
    Vector3 direction = new Vector3(0, 1, -0.7f);
    int index = 0;
    AudioSource audio_source;

    private void Awake()
    {
        spawner_Pos = GetComponentInChildren<Transform>().position;
        audio_source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out OVRGrabbable m_grabbable))
        {
            if (!m_grabbable.isGrabbed) // se l'oggetto lanciato non e' grabbato 
            {
                

                if (!spawned)
                {
                    spawned = true;
                    Invoke("ResetSpawn", 0.5f);
                    SpawnObj(other);
                }
                

            }
        }
    }

    private void SpawnObj(Collider other)
    {

        audio_source.Play();

        if (other.gameObject.CompareTag("Sphere"))
        {
            index = 1;
        }
        else if (other.gameObject.CompareTag("Cube"))
        {
            index = 2;
        }
        else if(other.gameObject.CompareTag("Pyram"))
        {
            index = 0;
        }

        GameObject swappedObj = Instantiate(interactables[index], spawner_Pos, Quaternion.identity);
       
        Destroy(other.gameObject);

        if (swappedObj.TryGetComponent(out Rigidbody rb))
        {
            

            Rigidbody i_rb = rb;

            i_rb.AddForce(direction * 3f, ForceMode.Impulse);

        }
    }

    void ResetSpawn()
    {
        spawned = false;
        //spawnObject = false;
    }

   /* void PlayClip(AudioClip audioClip)
    {
        audio_source.clip = audioClip;
        audio_source.Play();
    }*/
}
