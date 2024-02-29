
using UnityEngine;

public class NewPyram_Interact : MonoBehaviour
{
    Rigidbody rb;
    
    bool isGrounded;

    public static bool tubeActivePyram;
    AudioSource audio_source;


    private void Awake() {
        rb = GetComponent<Rigidbody>();
        audio_source = GetComponent<AudioSource>();

    }

    private void Start() {
       // Light_Manager.isPyramGrounded = false;
    }
       
    private void OnCollisionEnter (Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            audio_source.Play();

            Light_Manager.contPyram++;
            isGrounded = true;
            if (isGrounded)
            {
                Light_Manager.isPyramGrounded = true;
            }
            //Debug.Log("Sono a terra");

            Vibrate();
        }
        else if(collision.gameObject.tag != "Ground"){
            audio_source.Play();
        }
        
    }

    private void OnCollisionExit (Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground")){
            isGrounded = false;
            Light_Manager.contPyram--;

            if(Light_Manager.contPyram == 0)
            {
                if (!isGrounded)
                {
                    Light_Manager.isPyramGrounded = false;

                }
            }
            
            
        }
        
    }

    private void OnCollisionStay(Collision other) {
        if(other.gameObject.CompareTag("Ground")){
           // Light_Manager.isPyramGrounded = true;
        }
    }

    private void Vibrate()
    {
        OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.LTouch);
        Invoke("EndVibration", 0.5f);
    }
    private void EndVibration()
    {
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
    }


}
