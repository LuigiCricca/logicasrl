using UnityEngine;

public class Audio_Door1 : MonoBehaviour
{
    [SerializeField] AudioSource audio_door1;
   
    void Update()
    {
        if(BigDoor_Movement.animation_started){
            
            BigDoor_Movement.animation_started = false;
            audio_door1.Play();

           
            

            //Invoke("StopAudio", 2.5f);
        }
        
    }

    void StopAudio(){
        audio_door1.Stop();
    }




}
