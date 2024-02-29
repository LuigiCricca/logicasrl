using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Normal.Realtime;

public class Hand_UI_Multiplayer : MonoBehaviour
{
    float defaultLength = 6f;

    LineRenderer lineRenderer;
    

  
    [SerializeField] LayerMask uiLayer;

    bool isOnUi;
    bool uiPressed;
    bool started;
    protected string buttonHitted;
    RaycastHit hit;
    Button hitButton;

    FirstUIButton_Component[] firstComponents;
    //lo stesso per la component dell'altro bottone

    private void Awake()
    {
        isOnUi = false;
        uiPressed = false;
        started = false;
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        firstComponents = FindObjectsOfType<FirstUIButton_Component>();
        Debug.Log(firstComponents.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckForRaycast())
        {
            CheckPressedButton();
        }
    }

    private bool CheckForRaycast() // abilita il raycast solo sulla ui 
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, defaultLength, uiLayer))
        {

            isOnUi = true;

            UpdateLength();


        }
        else
        {
            isOnUi = false;
            lineRenderer.enabled = false;

        }
        return isOnUi;

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

//---------------------------------------------------------------------------------------------------------------

    protected virtual void CheckPressedButton() // controlla che bottone viene cliccato dall'utente
    {
        if (CheckForRaycast() && OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0 && !uiPressed)
        {
            Debug.Log("Check Pressed Button");
            uiPressed = true;

            Invoke("ResetUIPressed", 1.5f);


            // se l'oggetto colpito dal raycast ha una component button allora memorizza il nome nella stringa e esegui la funzione associata nella callback del onclick
            if (hit.collider.gameObject.GetComponent<Button>() != null)
            {
                buttonHitted = hit.collider.gameObject.name;
                hitButton = hit.collider.gameObject.GetComponent<Button>();
                //hitButton.onClick.Invoke(); // ----------------------------->> CheckButonName() tutti i bottone hanno nell'invoke di eseguire la funzione check button name
                if(buttonHitted.Equals("First_Button_P1") || buttonHitted.Equals("First_Button_P2"))
                {
                    foreach(FirstUIButton_Component component in firstComponents)
                    {
                        component.StartClicked(true);
                    }
                }
            }


        }
    }
    void ResetUIPressed()
    {
        Debug.Log("__________RESETTA UI PRESSED____________");
        uiPressed = false;
    }
}
