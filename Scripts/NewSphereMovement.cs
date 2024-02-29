using UnityEngine;
using UnityEngine.Events;
public class NewSphereMovement : MonoBehaviour
{

    Rigidbody rb;

    public static bool disableWall;

    bool isGrounded;
    public static UnityAction onOKClicked;

  
    AudioSource audio_source;
   



    private void OnEnable()
    {
        ButtonClosePanel.onStartPanelSingleClosed += MoveFirstSphere;
    }
    private void OnDisable()
    {
        ButtonClosePanel.onStartPanelSingleClosed -= MoveFirstSphere;
    }
    private void Awake() {

        rb = GetComponent<Rigidbody>();
        audio_source = GetComponent<AudioSource>();
        rb.drag = 0.7f;
       
    }

    private void Start() {
        
        Invoke("ActivateAudio", 0.1f);
    }

    void ActivateAudio()
    {
        audio_source.enabled = true;
    }

 void MoveFirstSphere()
    {
        Invoke("MoveObject", 2f);
    }


    /** COLLISION AND TRIGGER DETECTION **/


    private void OnCollisionEnter(Collision collision)
    {
        rb.angularDrag = 0.7f;
        if (collision.gameObject.tag == "Ground")
        {
            audio_source.Play();

            
            Light_Manager.contSphere++;
            isGrounded = true;
            if (isGrounded)
            {
                Light_Manager.isSphereGrounded = true;
            }
            Vibrate();
        }
        else
        {
            //Debug.LogWarning("suono fuori");
            audio_source.Play();

        }

    }


    private void OnCollisionExit (Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground")){
            isGrounded = false;

            Light_Manager.contSphere--;

            if(Light_Manager.contSphere == 0)
            {
                if (!isGrounded)
                {
                    Light_Manager.isSphereGrounded = false;
                }
            }
           
        }
    }


    private void OnCollisionStay(Collision collision) {
        if(collision.gameObject.CompareTag("Ground")){
            Light_Manager.isSphereGrounded = true;
        }
       
        
    }

    void MoveObject(){
        
        disableWall = true;
        Debug.Log("Muovi la sfera");
            if(Door_Movement.isMoveSphere == false){
                rb.AddForce(-Vector3.forward *12f, ForceMode.Impulse);
                Door_Movement.isMoveSphere = true;
            if (onOKClicked != null)
            {
                onOKClicked();
            }

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
