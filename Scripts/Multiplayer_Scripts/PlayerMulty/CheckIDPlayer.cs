using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Normal.Realtime;

public class CheckIDPlayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    Realtime realtime;
    RealtimeView r_view;
    RealtimeTransform r_transform;

    private void Awake()
    {
        realtime = GetComponent<Realtime>();

        text = GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (realtime.connected)
        {
           // text.text = realtime.clientID.ToString();
        }

    }

    void QuitApplication(Realtime real)
    {
        Debug.LogError("Un giocatore si e' disconnesso");

    }
}
