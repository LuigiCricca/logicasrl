using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class Cubes_Interactions : MonoBehaviour
{
    Transform cube1;

    Transform cube2;
    bool areImpiled;
    [SerializeField] GameObject spherePrefab;
    [SerializeField] GameObject pyramPrefab;
    GameObject sphereInstance;
    GameObject pyramInstance;
    
    public static bool interaction;
    GameObject[] gameObjects = new GameObject[2];
    Vector3 spawnPos;
    List<OVRGrabbable> m_grabbable = new List<OVRGrabbable>();

    bool alreadyInteract;

    [SerializeField] LayerMask groundMask;

    private void Awake()
    {
        spawnPos = GameObject.Find("SpawnPoint").GetComponent<Transform>().position;
    }

    void Start()
    {
        cube1 = transform.GetChild(0).transform;
        cube2 = transform.GetChild(1).transform;

        sphereInstance = spherePrefab;
        pyramInstance = pyramPrefab;


        gameObjects[0] = sphereInstance;
        gameObjects[1] = pyramInstance;

        interaction = false;
        alreadyInteract = false;

        Light_Manager.isCube1Grounded = false;
        Light_Manager.isCube2Grounded = false;
       

        for (int i=0;i<this.transform.childCount;i++)
         {
            m_grabbable.Add(transform.GetChild(i).GetComponent<OVRGrabbable>());
            Debug.Log(m_grabbable[i].gameObject.name);
         }

        
        
         
         
    }

    
    void Update()
    {
        //spawnPos = cube1.transform.position;

       // UpdateSpawnPos();
       //
       //Debug.Log(spawnPos);

        for (int i = 0; i < transform.childCount; i++)
        {
            if (!alreadyInteract)
            {
                Light_Manager.isCube1Grounded = Physics.Raycast(cube1.position, Vector3.down, 0.2f, groundMask);
                Light_Manager.isCube2Grounded = Physics.Raycast(cube2.position, Vector3.down, 0.2f, groundMask);
                

            }


            if ((Light_Manager.isCube1Grounded || Light_Manager.isCube2Grounded) && !interaction && !m_grabbable[0].isGrabbed && !m_grabbable[1].isGrabbed) // sistemare il grab in modo che non si possano connettere
            {

                RaycastHit hit;
                areImpiled = Physics.Raycast(transform.GetChild(i).position, Vector3.up, out hit, 0.2f);
                if (areImpiled && hit.transform.tag == "Cube" && !interaction)
                {
                    interaction = true;

                }
            }


        }
        //---------------------------------------------------
        //  ISTANZIA PIRAMIDE E SFERA
        InstantiatePrefabs();

    }

    private void InstantiatePrefabs()
    {
        if (interaction && !alreadyInteract)
        {
            interaction = false;
            alreadyInteract = true;

            Invoke("CreateLastPrefabs",0.05f);

            Destroy(gameObject, 0.2f);
            Light_Manager.isCube1Grounded = false;
            Light_Manager.isCube2Grounded = false;


        }
    }

    private void CreateLastPrefabs()
    {
        float r_offset = Random.Range(-0.2f,-1);
        for (int i = 0; i < gameObjects.Length; i++)
        {
            GameObject prova;
            prova = Instantiate(gameObjects[i]);
            // gameObjects[i].transform.position = new Vector3(1, 1, 1);
            if (i == 1)
            {
                // gameObjects[i].transform.position = new Vector3(spawnPos.x + 0.1f, spawnPos.y+0.2f, spawnPos.z + 0.1f);
                spawnPos = new Vector3(spawnPos.x + 0.7f, spawnPos.y + 0.2f, spawnPos.z + 0.7f);
                prova.transform.position = spawnPos;
                Rigidbody rb = prova.gameObject.GetComponent<Rigidbody>();
                rb.drag = 1;
            }
            else
            {
                //gameObjects[i].transform.position = new Vector3(spawnPos.x + 0.1f, spawnPos.y+0.2f, spawnPos.z + 0.1f);
                spawnPos = new Vector3(spawnPos.x - r_offset, spawnPos.y + 0.2f, spawnPos.z - r_offset);
                prova.transform.position = spawnPos;
            }

        }
    }

  /*  private void UpdateSpawnPos()
    {
        //this.transform.position = spawnPos;
        if (Light_Manager.isCube1Grounded && !Light_Manager.isCube2Grounded)
        {
            spawnPos = cube1.position;
        }
        else if (Light_Manager.isCube2Grounded && !Light_Manager.isCube1Grounded)
        {
            spawnPos = cube2.position;
        }
    }*/

   
    
   
}
