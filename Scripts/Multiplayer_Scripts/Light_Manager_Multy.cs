using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Light_Manager_Multy : MonoBehaviour
{
//** CHECK OBJ GROUNDED **//

    //** PLAYER1 **//
     static public bool player1_sphereGrounded;
     static public bool player1_cylinderGrounded;
     static public bool player1_cubeGrounded;


    //** PLAYER2 **//
    static public bool player2_sphereGrounded;
    static public bool player2_cylinderGrounded;
    static public bool player2_cubeGrounded;


//** COUNTERS OBJ P1 & P2  **//

    //** PLAYER1 **//
    static public int player1_contSphere;
    static public int player1_contCylinder;
    static public int player1_contCube;

    //** PLAYER2 **//
    static public int player2_contSphere;
    static public int player2_contCylinder;
    static public int player2_contCube;

     public static bool taskCompleted;

  
    Color orangeColor = new Color32 (254 , 151, 0 , 1);

//** LED P1 & P2  **//
    GameObject ledP1;
    GameObject ledP2;
    List<MeshRenderer> lightsRenderersP1 = new List<MeshRenderer>();
    List<MeshRenderer> lightsRenderersP2 = new List<MeshRenderer>();
    Room1_LedComponent room1LedComponent;
    LedRoom2Component room2LedComponent;

  

    private void Awake() {
        
        taskCompleted=false;

        player1_sphereGrounded = false;
        player1_cylinderGrounded = false;
        player1_cubeGrounded = false;

        player2_sphereGrounded = false;
        player2_cylinderGrounded = false;
        player2_cubeGrounded = false;

        player1_contSphere = 0;
        player1_contCylinder = 0;
        player1_contCube = 0;

        player2_contSphere = 0;
        player2_contCylinder = 0;
        player2_contCube = 0;

        // put inside the list LightRenderers each led

       /* foreach (Transform child in this.transform){
            lightsRenderersP1.Add(child.GetComponent<MeshRenderer>());
        }
        */
        ledP2=GameObject.Find("LedP2");
        ledP1=GameObject.Find("LedP1");
        room1LedComponent = ledP1.gameObject.GetComponent<Room1_LedComponent>();
        room2LedComponent = ledP2.gameObject.GetComponent<LedRoom2Component>();

    
        
        
        AddToList(ledP1.transform,lightsRenderersP1);
        AddToList(ledP2.transform,lightsRenderersP2);

       //AddToList(this.transform,lightsRenderersP1);
        
    }

void AddToList(Transform ledParent, List<MeshRenderer> renderers){
    foreach(Transform child in ledParent){
        renderers.Add(child.GetComponent<MeshRenderer>());
        
    }
}
    void Update()
    {
        ManageColors();

        if (Input.GetKey(KeyCode.Alpha1))
        {
            player2_sphereGrounded = true;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            player2_cubeGrounded = true;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            player2_cylinderGrounded = true;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            player2_cylinderGrounded = false;
            player2_cubeGrounded = false;
            player2_sphereGrounded = false;
        }


        if (!taskCompleted)
      {
            ManageColors();
      }
    }

    

    void ChangeColorP1(Color32 colore){
        /* foreach (MeshRenderer childRender in lightsRenderersP1){
             childRender.material.SetColor("_EmissionColor", colore);
         }*/
        //RenderSettings.fogColor = colore;
        room1LedComponent.SetColorRoom1(colore);
    }

    void ChangeColorP2(Color32 colore){
        /*foreach (MeshRenderer childRender in lightsRenderersP2){
            childRender.material.SetColor("_EmissionColor", colore);
        }*/
        //RenderSettings.fogColor = colore;
        room2LedComponent.SetColorRoom2(colore);
    }

    void ManageColors(){

        //** PLAYER2 **//

        if((player2_sphereGrounded) && (!player2_cubeGrounded) && (!player2_cylinderGrounded)){

            ChangeColorP1(Color.yellow);
            //Debug.Log("GIALLO P1");

        }

        if((player2_cubeGrounded) && (!player2_sphereGrounded) && (!player2_cylinderGrounded)){
            
            ChangeColorP1(Color.red);

        }

        if((player2_cylinderGrounded) && (!player2_sphereGrounded) && (!player2_cubeGrounded)){

            ChangeColorP1(Color.blue);

        }

        if(!player2_sphereGrounded && !player2_cubeGrounded && !player2_cylinderGrounded){

            ChangeColorP1(Color.white);
        }

        if((player2_cubeGrounded) && (player2_sphereGrounded) && (player2_cylinderGrounded)){
            ChangeColorP1(Color.white);
        }

        
        
        
        //** PLAYER1 **//

        if((player1_sphereGrounded) && (!player1_cubeGrounded) && (!player1_cylinderGrounded)){

            ChangeColorP2(Color.yellow);
           // Debug.Log("GIALLO P2");

        }

        if((player1_cubeGrounded) && (!player1_sphereGrounded) && (!player1_cylinderGrounded)){
            
            ChangeColorP2(Color.red);

        }

        if((player1_cylinderGrounded) && (!player1_sphereGrounded) && (!player1_cubeGrounded)){

            ChangeColorP2(Color.blue);

        }

        if(!player1_sphereGrounded && !player1_cubeGrounded && !player1_cylinderGrounded){

            ChangeColorP2(Color.white);
        }

        if((player1_sphereGrounded) && (player1_cubeGrounded) && (player1_cylinderGrounded)){
            ChangeColorP2(Color.white);
        }

        if(player2_sphereGrounded && player2_cylinderGrounded && !player2_cubeGrounded)
        {
            ChangeColorP1(Color.green);
        }



//** MIXED COLOR
        if((player1_sphereGrounded) && (player1_cubeGrounded) && (!player1_cylinderGrounded)){
            
            ChangeColorP2(orangeColor);

        }

        if((player2_sphereGrounded) && (player2_cubeGrounded) && (!player2_cylinderGrounded) ){

            ChangeColorP1(orangeColor);

        }

        if((player1_cubeGrounded) && (player1_cylinderGrounded) && (!player1_sphereGrounded)){

            ChangeColorP2(Color.magenta);

        }

        if((player2_cubeGrounded) && (player2_cylinderGrounded) && (!player2_sphereGrounded)){

            ChangeColorP1(Color.magenta);

        }


//** VINCITA?
        if((player1_sphereGrounded) && (player1_cylinderGrounded) && (!player1_cubeGrounded)){

            ChangeColorP2(Color.green);
            taskCompleted = true;
        }

//** se all'interno dell'area di gioco di PLAYER2 si trovano sia la sfera che il cilindro, il gioco non finisce: il colore diventa bianco
        /*if((player2_sphereGrounded) && (player2_cylinderGrounded) && (!player2_cubeGrounded)){

            ChangeColorP1(Color.white);
        }*/



    }
}
