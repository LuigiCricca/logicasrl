using System.Collections;
using UnityEngine;

public class EndCanvas : MonoBehaviour
{
     Transform playerCamera;
    bool isStarted = false;
    CanvasGroup c_group;
  
    private void Awake()
    {
        ColliderManagement(false, 4);

        playerCamera = GameObject.Find("CenterEyeAnchor").GetComponent<Transform>();
        

        c_group = GetComponent<CanvasGroup>();
        c_group.alpha = 0;
        gameObject.layer = 4;

        isStarted = false;



    }


    void Update()
    {
        //SDebug.Log(playerCamera.position);
        
        if (Light_Manager_Multy.taskCompleted)
        {
            
            ShowCanvas();
            transform.LookAt(playerCamera);

        }

/*
        if (Light_Manager.taskCompleted)
        {
            
            ShowCanvas();
            transform.LookAt(playerCamera);

        }

        */
    }

    void ShowCanvas()
    {
        if(!isStarted)
        {
            StartCoroutine(Fade());
            isStarted = true;
        }
    }
    private void ColliderManagement(bool state, int layer)
    {
        GetComponent<Collider>().enabled = state;
        gameObject.layer = layer;
        foreach (Transform child in this.transform)
        {

            child.gameObject.layer = layer;
            if (child.GetComponent<Collider>() != null)
                child.GetComponent<Collider>().enabled = state;
        }
    }

    private IEnumerator Fade()
    {
        Debug.LogWarning("fade started");
        gameObject.SetActive(true);
        

        while (c_group.alpha < 1)
        {
            yield return new WaitForSeconds(0.03f);
            c_group.alpha = c_group.alpha + Time.deltaTime;
        }
        if(c_group.alpha>=1)
        {
            ColliderManagement(true, 5);
        }
      
    }

    
}
