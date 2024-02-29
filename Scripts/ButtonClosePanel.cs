using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ButtonClosePanel : MonoBehaviour
{
    static public bool _closedPanel;
    bool fadeStarted;
    CanvasGroup c_group;

    public static UnityAction onStartPanelSingleClosed;

   
    
    private void Awake()
    {
        gameObject.SetActive(true);
        _closedPanel = false;
        
        if (GetComponent<CanvasGroup>() != null)
            c_group = GetComponent<CanvasGroup>();
        
        fadeStarted = false;
                   
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            // close the canvas
            if(_closedPanel == false){
                _closedPanel = true;
                
            }

        }
    }
    private void FixedUpdate()
    {
        if(_closedPanel && !fadeStarted)
        {
            // deve partire il fade 
            StartCoroutine(Fade());
            fadeStarted = true;
            
           

        }
    }


    public void StartFade()
    {
        
        _closedPanel = true;
        onStartPanelSingleClosed();
    }

    private IEnumerator Fade()
    {
        Debug.LogWarning("fade started");
        _closedPanel = true;
        
        while (c_group.alpha > 0)
        {
            yield return new WaitForSeconds(0.01f);
            c_group.alpha = c_group.alpha - Time.deltaTime;
        }
        if (c_group.alpha <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 0.5f);
            
        }
        


    }
}
