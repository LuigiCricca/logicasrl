using UnityEngine;
using Normal.Realtime;
using OculusSampleFramework;
using UnityEngine.SceneManagement;

public class PlayerSpawn_Multy : MonoBehaviour
{
    Realtime realtimeManager;
    OVRPlayerController playerController;
    [SerializeField] Transform spawnP2;
    
    Vector3 p1;

    public static int id_player1;
    public static int id_player2;
    
    bool wait;
    bool reenableMov = false;


    private void Awake()
    {
        // se non mi trovo nella scena multrigiocatore distruggi la component
        if (!SceneManager.GetActiveScene().name.Equals("MultyScene"))
        {
            Destroy(this);
          
        }
        else // sono nella scena multiplayer. Blocco il movimento del giocatgore
        {
            
            reenableMov = false;
            realtimeManager = FindObjectOfType<Realtime>();

            playerController = FindObjectOfType<OVRPlayerController>();
            playerController.EnableLinearMovement = false;
            playerController.EnableRotation = false;

            realtimeManager.didDisconnectFromRoom += QuitApplication;
        }
       
        

    }

    private void Start()
    {
        
        FadeBeforeConnecting();
    }


    private void Update()
    {
        
        if (realtimeManager.connected) { 
            if (!wait)
            {
                
                Invoke("UpdatePlayersPosition", 2f);
               
                wait = true;
            }
        }
    
        // se non ci sono due player ancora, disabilita il movimento dei player
        if (FindObjectsOfType<RealtimeAvatar>().Length >= 2 && !reenableMov)
        {
            Debug.LogWarning("2 players connected, you can move");
            reenableMov = true;
            OVRPlayerController[] players = FindObjectsOfType<OVRPlayerController>();
            foreach (OVRPlayerController controller in players)
            {
                controller.EnableLinearMovement = true;
                controller.EnableRotation = true;
            }
        }
      
 
    }



    private void FadeBeforeConnecting()
    {
        if (FindObjectsOfType<RealtimeAvatar>().Length <= 0)
        {

            Debug.LogError("CIAO");
            OVRScreenFade.instance.SetExplicitFade(1f);

            Invoke("StopFade", 5f);
        }
  
    }
    void StopFade()
    {
        OVRScreenFade.instance.SetExplicitFade(0f);
        OVRScreenFade.instance.FadeIn();

    }
    void UpdatePlayersPosition()
    {
       
            if (realtimeManager.clientID == 0)
            {
                p1 = new Vector3(0f, 2.3f, -20);
                Quaternion local = new Quaternion(0, 180, 0, 0);
                transform.position = p1;
                transform.rotation = local;
            OVRScreenFade.instance.Fade(1, 0);

                id_player1 = realtimeManager.clientID;
            //avatar.localPlayer
                Debug.LogWarning("You are player 1");


            }

            if (realtimeManager.clientID >= 1)
            {
                Quaternion local = new Quaternion(0, 0, 0, 0);
                transform.position = spawnP2.position;
                transform.rotation = local;
                //avatar.localPlayer.root = spawnP2;
                id_player2 = realtimeManager.clientID;

                Debug.LogWarning("You are player 2");
                //Debug.LogError(id_player2);
            }

      
        
    }

 

    void QuitApplication(Realtime realtime)
    {
        Application.Quit();
    }

}

