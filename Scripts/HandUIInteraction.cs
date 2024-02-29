using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;


public class HandUiInteraction : MonoBehaviour
{
    [SerializeField] protected float defaultLength = 3.0f;

    private LineRenderer lineRenderer;
    [SerializeField] LayerMask uiLayer;
    bool isOnUi;
    bool pressed;
    public static event Action onUIClick;
    RaycastHit hit;
    bool started;





    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        isOnUi = false;
        pressed = false;
        started = false;
    }

    // funzione per gestire tutti i bottoni se li sto puntando
    private void Update()
    {


        if (Physics.Raycast(transform.position, transform.forward, out hit, defaultLength, uiLayer))
        {


            isOnUi = true;

            UpdateLength();
            string buttonHitted = hit.collider.gameObject.tag;
            Debug.Log(buttonHitted);

        }
        else
        {
            isOnUi = false;
            lineRenderer.enabled = false;

        }
        if (isOnUi && OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0 && !pressed)
        {

            
            pressed = true;
            //onUIClick?.Invoke();
           if (onUIClick != null)
           {
                onUIClick();
           }
            Invoke("ResetPressed", 2f);

            //string buttonHitted = hit.collider.tag;
         
            if (hit.collider.CompareTag("Retry"))
            {
                RetryButton();
            }
            if (hit.collider.CompareTag("Quit"))
            {
                Quit();
            }
            if (hit.collider.CompareTag("Single"))
            {
                OpenSinglePlayer();
            }
            if (hit.collider.CompareTag("Multi"))
            {
                Debug.LogWarning("OpenMultiplayerScene");
            }


        }
    }

    private void UpdateLength()
    {

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, GetEnd());
    }

    protected virtual Vector3 GetEnd()
    {
        return CalculateEnd(defaultLength);
    }

    protected Vector3 CalculateEnd(float length)
    {
        return transform.position + (transform.forward * length);
    }

    private void RetryButton() // la coroutine deve partire per ogni canvas.
    {
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        StartCoroutine(RestartScene());



    }

    void Quit()
    {
        Debug.Log("Quit");
        SceneManager.LoadScene("Main_Menu");
    }

    void ResetPressed()
    {
        pressed = false;
    }

    // coroutine unica per cambiare scena passando il parametro
    private IEnumerator RestartScene()
    {
        if (!started)
        {
            yield return new WaitForSeconds(1f);
            // devo fare apparire un canvas fade davanti alla faccia
            if (SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex).isDone)
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                Debug.Log("Retry");
            }

        }

    }


    void OpenSinglePlayer()
    {
        SceneManager.LoadScene(1);
    }
   
}
