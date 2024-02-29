using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class CheckPlayersGame : MonoBehaviour
{
    Realtime realtime;
    RealtimeTransform r_transform;
    RealtimeView r_view;
    RealtimeAvatar[] r_avatar;

    bool players = false;

    private void Awake()
    {
        realtime = GetComponent<Realtime>();
        r_view = GetComponent<RealtimeView>();
        r_transform = GetComponent<RealtimeTransform>();

        
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SearchPlayers", 180f, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        if (players)
        {
            #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }

    void SearchPlayers()
    {
        r_avatar = FindObjectsOfType<RealtimeAvatar>();

        if(r_avatar.Length < 2)
        {
            players = true;
            Debug.LogError("1 PLAYER");
        }
        else if(r_avatar.Length > 1)
        {
            players = false;
            Debug.LogError("2 PLAYER");
        }
    }


}
