using System.Collections;
using UnityEngine;

public class Door_Movement : MonoBehaviour
{
    float yEndPos=1f;
    float yStartPos;
    bool isUp=false;

    public static bool isMoveSphere;

    [SerializeField] Animator animator_door;
    
    public static bool animation_started;

    bool animation_end;
    

    IEnumerator doorMove;
    private void Awake()
    {
        yStartPos = transform.position.y;

        animation_started = false;
        

        doorMove = DoorMovement();
    }
 
    private void LateUpdate() {
        
        if(ButtonClosePanel._closedPanel ){
            //StartCoroutine(DoorMovement());

            //Debug.LogError("PORTA PORTINA");

            if(!animation_end){
                //DoorAnimation();

                Invoke("DoorAnimation", 1f);
            }
            else{
                animator_door.SetBool("little_door", false);
            }
        }

        
    }

    private void Start() {
        isMoveSphere = false;
        animation_end = false;
    }

    void DoorAnimation(){
        if(!animation_end){
            animation_started = true;
            animator_door.SetBool("little_door", true);
            animation_end = true;
        }
        

        //audio_door.Play();
    }

 //*************** DELERTE IEnumerator ************* //
    private IEnumerator DoorMovement()
    {
        
        //yield return new WaitForSeconds(0.2f);
        while(transform.position.y < yEndPos)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            transform.Translate(Vector3.up * Time.deltaTime);
        }    
            
        yield return new WaitForSeconds(1f);
        isUp = true;
        

        while(transform.position.y>yStartPos && isUp)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            transform.Translate(Vector3.down * Time.deltaTime);
        }
        
    }

    //************************************************/ 
}
