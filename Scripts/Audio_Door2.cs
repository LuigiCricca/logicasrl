using UnityEngine;

public class Audio_Door2 : MonoBehaviour
{
    [SerializeField] AudioSource audio_door2;
   

    // Update is called once per frame
    void Update()
    {
        if(Door_Movement.animation_started){
            
            Door_Movement.animation_started = false;
            audio_door2.Play();

            

            //Invoke("StopAudio", 2.5f);
        }
    }

    void StopAudio(){
        audio_door2.Stop();
    }
}
