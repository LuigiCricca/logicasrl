using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSetup_HandController : MonoBehaviour
{
    [SerializeField] GameObject Controller_PrefabRight;
    [SerializeField] GameObject HandTracking_PrefabRight;
    [SerializeField] GameObject Controller_PrefabLeft;
    [SerializeField] GameObject HandTracking_PrefabLeft;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRPlugin.GetHandTrackingEnabled() == true)
        {
            HandTracking_PrefabRight.SetActive(true);
            HandTracking_PrefabLeft.SetActive(true);

            Controller_PrefabRight.SetActive(false);
            Controller_PrefabLeft.SetActive(false);

        }
        else
        {
            HandTracking_PrefabRight.SetActive(false);
            HandTracking_PrefabLeft.SetActive(false);

            Controller_PrefabRight.SetActive(true);
            Controller_PrefabLeft.SetActive(true);

        }


    }
}
