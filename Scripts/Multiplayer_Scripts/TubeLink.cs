using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
public class TubeLink : MonoBehaviour
{
    bool sended_P1;
    bool sended_P2;

    GameObject obj_player1;
    GameObject obj_player2;

    [SerializeField] Transform tube_room1_pos;
    [SerializeField] Transform tube_room2_pos;
    Realtime realtime;
    int count = 0;

    private void Awake() {

        realtime = FindObjectOfType<Realtime>();
        //realtime.didConnectToRoom
        gameObject.SetActive(false);
        realtime.didConnectToRoom += SetOwnerOnTube;
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
         // manda al tubo stanza due

        if ((other.gameObject.tag == "Sphere") || (other.gameObject.tag == "Cube") || (other.gameObject.tag == "Cylinder"))
        {
            if (this.gameObject.name == "HoleLink1" && !sended_P1)
            {
                sended_P1 = true;

                Debug.Log("player 1 ha mandato l'oggetto al player 2:" + sended_P1);
                MoveObj();
            }
            if(this.gameObject.name == "HoleLink2" &&!sended_P2)
            {
                sended_P2 = true;
                Debug.Log("player 2 ha mandato l'oggetto al player 1: " + sended_P2);
                MoveObj();
            }
        }
    }

    void MoveObj(){

        if(sended_P1){

            sended_P1 = false;

            //instanzio oggetto pos TubeRoom2 ----> sposto l'oggetto 
            
            //Debug.Log("P1 ha creato l'oggetto");
        }

        if(sended_P2){

            sended_P2 = false;

            //instanzio oggetto pos TubeRoom1 ----> sposto l'oggetto 

           // Debug.Log("P2 Ha creato l'oggetto");
        }
    }

    void SetOwnerOnTube(Realtime real)
    {
       
        if (realtime.connected)
        {
            count++;
            if (realtime.clientID == 0 && FindObjectsOfType<RealtimeAvatar>().Length < 2)
            {
                if (gameObject.name.Equals("HoleLink1"))
                {
                    Debug.LogWarning("Sei player 1 controlla tube link 1");
                }
                else { Debug.Log("You son of a bitch"); }
               
                
            }
            else if(FindObjectsOfType<RealtimeAvatar>().Length == 2)
            {
                //if(this.gameObject)
            }
           
        }
    }
}
