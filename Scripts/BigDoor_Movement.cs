using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDoor_Movement : MonoBehaviour
{
    [SerializeField] Animator door_animator;
    public static bool animation_started;
    Vector3 endPos;

    bool animation_end;

    private void Awake() {
        animation_started = false;
        animation_end = false;
    }

    private void Start() {
        animation_started = false;
        animation_end = false;

        //** BOOL COLOR RESET

        
    }
    
    void Update()
    {
        if(Light_Manager.taskCompleted){
            if (!animation_end)
            {
                animation_end = true;
                BigDoorAnimation();
            }
            
        }

        

        
    }

    void BigDoorAnimation(){

        animation_started = true;

        door_animator.SetBool("big_door", true);

        Invoke("SetPositionDoor", 2f);
        
    }

    void SetPositionDoor(){
      //door_animator.SetBool("big_door", false);
        endPos = new Vector3(-0.01064718f, 7.09f, 7.037003f);
        transform.Translate(endPos);
    }
}
