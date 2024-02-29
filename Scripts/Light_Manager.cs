using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Light_Manager : MonoBehaviour
{    
   // MeshRenderer m_renderer;
    public static bool isSphereGrounded;
    public static bool isCube1Grounded;
    public static bool isCube2Grounded;
    public static bool areCubesImpiled;
    public static bool isPyramGrounded;
    public static bool isCubeTouched;
    public static bool isCubeGrounded;

    public static int contSphere;
    public static int contCube;
    public static int contPyram;

    public static bool taskCompleted;
    bool vibrationDone;

    Color defaultColor= Color.white;

    Color32 orangeColor = new Color32( 254 , 161 , 0, 1 );
    
    List<MeshRenderer> lightsRenderers = new List<MeshRenderer>();
    AudioSource audio_source;

    
   
    void Awake ()
    {
        
        isSphereGrounded = false;
        isCube1Grounded = false;

        areCubesImpiled = false;
        isPyramGrounded = false;
        isCubeTouched = false;
        taskCompleted=false;
        vibrationDone=false;
        audio_source = GetComponent<AudioSource>();

        contCube = 0;
        contPyram = 0;
        contSphere = 0;
      

        foreach (Transform child in this.transform)
        {
            lightsRenderers.Add(child.GetComponent<MeshRenderer>());
        }
      
    }

    private void Start() {

        
    }
    




    private void FixedUpdate()
    {
      if(!taskCompleted)
      {
            ManageColors();

      }
        else
        {

            if(!vibrationDone)
            {
                audio_source.Play();

                OVRInput.SetControllerVibration(0.2f, 0.2f, OVRInput.Controller.RTouch);
                OVRInput.SetControllerVibration(0.2f, 0.2f, OVRInput.Controller.LTouch);
                vibrationDone = true;
            }
            else
            {
                Invoke("EndVibration", 0.5f);
            }

        }
    }

    private void EndVibration()
    {
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
    }

    public void ChangeColor(Color32 colore)
    {
        foreach (MeshRenderer childRenderer in lightsRenderers)
        {
            childRenderer.material.SetColor("_EmissionColor", colore);

        }
        RenderSettings.fogColor = colore;

    }

    private void ManageColors()
    {
        if ((isSphereGrounded) && (!isCubeGrounded) && (!isPyramGrounded))
        {
            //Debug.LogWarning("GIALLO");
            ChangeColor(Color.yellow);
            
            //Debug.LogWarning("CONT SFERA: " + contSphere);

        }

        if ((isCubeGrounded) && (!isSphereGrounded) && (!isPyramGrounded))
        {
            ChangeColor(Color.red);
            //Debug.Log("Coloro di rosso");
            

           // Debug.LogWarning("CONT CUBO: " + contCube);


        }



        if ((isPyramGrounded) && (!isSphereGrounded) && (!isCubeGrounded))
        {
            ChangeColor(Color.blue);
            

            //Debug.LogWarning("CONT PYRAM: " + contPyram);


        }

        if ((isCubeGrounded) && (isPyramGrounded) && (!isSphereGrounded))
        {
            ChangeColor(Color.magenta);

        }




        if (!isCubeGrounded && !isPyramGrounded && !isSphereGrounded)
        {
            ChangeColor(defaultColor);

        }


        if ((isCubeGrounded) && (isPyramGrounded) && (isSphereGrounded))
        {
            ChangeColor(Color.white);

        }

        if ((isCubeGrounded) && (isSphereGrounded) && (!isPyramGrounded))
        {
            ChangeColor(orangeColor);



        }

        if ((isSphereGrounded) && (isPyramGrounded) && (!isCubeGrounded))
        {
            ChangeColor(Color.green);

            taskCompleted = true;
        }
    }

    
}
