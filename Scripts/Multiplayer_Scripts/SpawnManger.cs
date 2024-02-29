using UnityEngine;
using Normal.Realtime;

public class SpawnManger : MonoBehaviour
{
    static public bool isPlayer2;
    Realtime realtimeRef;
    //RealtimeAvatar[] playersInScene;
    [SerializeField] Vector3 spawnP1;
    [SerializeField] Vector3 spawnP2;
    RealtimeAvatarManager avatarManager;
    private void Awake()
    {
        //isPlayer2 = false;
        realtimeRef = FindObjectOfType<Realtime>();
        realtimeRef.gameObject.GetComponent<RealtimeAvatarManager>();
        realtimeRef.didConnectToRoom += ManagePlayers;
          
    }

    void ManagePlayers(Realtime real)
    {
      
        
        if (FindObjectsOfType<RealtimeAvatar>().Length==1)
        {
            Debug.LogWarning("SpawnPlayer1");
            isPlayer2 = false;
            Quaternion local = new Quaternion(0, 180, 0, 0);
            //avatarManager.
            //transform.SetPositionAndRotation(spawnP1, local);
           
        }
        else if (FindObjectsOfType<RealtimeAvatar>().Length == 2)
        {
            Debug.LogWarning("Spawn Player 2");
            isPlayer2 = true;
            Quaternion local = new Quaternion(0, -180, 0, 0);
            FindObjectOfType<RealtimeAvatar>().gameObject.transform.SetPositionAndRotation(spawnP2, local);
        }
    }


}
