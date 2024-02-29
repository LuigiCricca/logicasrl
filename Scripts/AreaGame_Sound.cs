using UnityEngine;

public class AreaGame_Sound : MonoBehaviour
{
    [SerializeField] AudioSource audio_source; 
    // Start is called before the first frame update
   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sphere") || collision.gameObject.CompareTag("Cube") || collision.gameObject.CompareTag("Pyram")) {

            audio_source.Play();
        }
    }
}
