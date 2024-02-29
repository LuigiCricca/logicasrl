using UnityEngine;

public class NewCube_Interact : MonoBehaviour
{
    Rigidbody rb;

    static public bool tubeActiveCube;

    bool isGrounded;
    AudioSource audio_source;
    

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        audio_source = GetComponent<AudioSource>();
        
    }

    private void Start() {
       // Light_Manager.isCubeGrounded = false;
    }

    private void OnCollisionEnter (Collision collision)
    {

//        Debug.Log(collision.gameObject.name);

        if(collision.gameObject.CompareTag("Ground") )
        {
            audio_source.Play();

            // audio_source.clip = clip_area_sound;
            //audio_source.Play(); //** AUDIO **//


            Light_Manager.contCube++;
            //Debug.Log("Sono a terra");
            isGrounded = true;
            if (isGrounded)
            {
                Light_Manager.isCubeGrounded = true;
            }

            Vibrate();
        }
        else
        {
            audio_source.Play();
        }

      
    }

    private void Vibrate()
    {
        OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.LTouch);
        Invoke("EndVibration",0.5f);
    }
    private void EndVibration()
    {
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
    }

    private void OnCollisionExit (Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground")){
            isGrounded = false;
            Light_Manager.contCube--;

            if(Light_Manager.contCube == 0)
            {
                if (!isGrounded)
                {
                    Light_Manager.isCubeGrounded = false;
                    
                }
            }
        }
        
    }

    private void OnCollisionStay(Collision other) {
        if(other.gameObject.CompareTag("Ground")){
            Light_Manager.isCubeGrounded = true;
            
        }
        Debug.Log(other.collider.name);
    }


   

    

}
