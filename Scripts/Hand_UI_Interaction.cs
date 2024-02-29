using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (LineRenderer))]
public class Hand_UI_Interaction : MonoBehaviour
{   
    // lunghezza max del line renderer
    [SerializeField] protected float defaultLength = 3.0f;

    private LineRenderer lineRenderer;

    // il layer su cui si trova la ui
    [SerializeField] LayerMask uiLayer;

    // bool per capire se mi trovo sulla ui dal raycast
    protected bool isOnUi;
    //se ho gia' interagito con la ui premendo right index trigger
    protected  bool uiPressed;
   
    protected  RaycastHit hit;
    // usato per gestire le coroutine di cambio scena
    bool started;
    // holder che tiene i bottoni per la selezione modalita' di gioco singola o multigiocatore
    GameObject playModeButton;
    // pulsante avvia nel;la schermata del main menu' che deve far aprire il playmode button
    GameObject startButton;
    // stringa che memorizza il nome del bottone colpito dal raycast viene usata in check button name
    protected  string buttonHitted;
    //button colpito a cui faccio eseguire l'invoke
    protected Button hitButton;
  // audio source di ogni bottone
    AudioSource audio_source;


    //public static event Action onUIClick;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        isOnUi = false;
        uiPressed = false;
        started = false;

        if(GameObject.Find("PlayMode_Button")!=null && GameObject.Find("Start_Button")!=null)
        {
            playModeButton = GameObject.Find("PlayMode_Button");
            startButton = GameObject.Find("Start_Button");
            startButton.SetActive(true);
            playModeButton.SetActive(true);
        }
        if(GameObject.Find("Start_Button") != null)
        {
            startButton = GameObject.Find("Start_Button");
        }

             
        
    }
    private void OnEnable()
    {
        started = false;
    }
    private void Start()
    {
        if (playModeButton!=null)
        {
            playModeButton.SetActive(false);
        }
        //OVRScreenFade.instance.FadeIn();
    }
    
    private void Update()
    {
        if(SceneManager.GetActiveScene().name.Equals("MultyScene")) // se questo script e' ancora presente quando apro la scena multigiocatore distruggilo.
        {
            Destroy(this);
        }
        if(CheckForRaycast())
        {
            CheckPressedButton();
        }
       
        
    }
    //------------------------------------------------------------------------
    // 1) fa partire il raggio dalla mano abilita e disabilita il line renderer solo se sono sulla ui se isOnUI true allora controlla che bottone viene premuto
    protected bool CheckForRaycast() // abilita il raycast solo sulla ui 
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
    //----------------------------------------------------------------------------------------------------------
    //2) controlla che bottone viene premuto leggendo il nome del pulsante in cui il raggio tocca
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
                hitButton.onClick.Invoke(); // ----------------------------->> CheckButonName() tutti i bottone hanno nell'invoke di eseguire la funzione check button name

                // se il game object colpito ha un audio source, fai partire il suono associato
                if (hit.collider.gameObject.GetComponent<AudioSource>() != null)
                {
                    audio_source = hit.collider.gameObject.GetComponent<AudioSource>();
                    audio_source.Play();
                }


            }
          

        }
    }
//------------------------------------------------------------------------
// 2b) resetta il bool per poter riutilizzare la ui
    void ResetUIPressed()
    {
        Debug.Log("__________RESETTA UI PRESSED____________");
        uiPressed = false;
    }
//------------------------------------------------------------------------
//3) ESEGUITO DALL'INVOKE DEL BUTON COLPITO DAL RAGGIO, CONTIENE TUTTE LE FUNZIONI PER TUTTI I PULSANTI CHE CI SONO
    public void CheckButtonName() 
    {
        //audioSource.play();
        if (buttonHitted.Equals("Start_Button")) //BOTTONE AVVIA in scena MAIN MENU
        {
            StartButton();

        }
        if (buttonHitted.Equals("Single_Button")) // BOTTONE "SINGLEPLAYER" che si apre dopo aver cliccato su start button
        {
            OpenSinglePlayer();
            
        }
        if (buttonHitted.Equals("Multi_Button")) // BOTTONE "SINGLEPLAYER" che si apre dopo aver cliccato su start button
        {
            OpenMultiPlayer();

        }
        if (buttonHitted.Equals("First_Button")) // BOTTONE OK PANNELLO colopra stanza di verde in scena SINGLEPLAYER
        {
            // ButtonClosePanel script=hitButton.gameObject.GetComponent<ButtonClosePanel>()
            if (hitButton.gameObject.TryGetComponent(out ButtonClosePanel script))
            {
                script.StartFade();

            }
        }
        if (buttonHitted.Equals("Retry_Interaction")) // BOTTONE RIPROVA scena SINGLEPLAYER
        {
            Debug.Log("-------Retry---------");
            RetryButton();
        }
        if (buttonHitted.Equals("Quit_Interaction")) // BOTTONE ESCI scena singleplayer
        {
            Debug.Log("-------QUIT---------");
            BackToMainMenu();
        }
        if (buttonHitted.Equals("OK_Button")) // BOTTONE SCENA LOADING DATA --> esce solo se ci sono errori di connessione o versione
        {
            ProcessJson.okClicked = true;
        }
        if (buttonHitted.Equals("Exit_Button")) // BOTTONE ESCI SCENA MAIN MENU
        {
            Invoke("QuitApp", 0.7f);
        }

    }



    //----------------------------------------------------------
    // funzione del pulsante avvia nella scena "Main menu"
    public void OpenSinglePlayer() 
    {
        //OVRScreenFade.instance.FadeOut();
        SceneManager.LoadScene("Single_Player");
 
    }
    //-----------------------------------------------------------
    // funzione per aprire multiplayer
    void OpenMultiPlayer()
    {
        //OVRScreenFade.instance.FadeOut();
        SceneManager.LoadScene("MultyScene");
    }
    //----------------------------------------------------------
    // BOTTONE ESCI SCENA MAIN MENU

    void QuitApp()
    {
        Application.Quit();
        Debug.Log("chiudi app");
    }
    //----------------------------------------------------
    // BOTTONE AVVIA MAIN MENU
    public void StartButton() // pulsante avvia
    {
        startButton.SetActive(false);
        playModeButton.SetActive(true);
         

    }
    private void RetryButton() // la coroutine deve partire per ogni canvas.
    {
       // OVRScreenFade.instance.FadeOut();
        StartCoroutine(RestartScene());
    }
    // coroutine unica per cambiare scena passando il parametro e disativando l'alpha del canvas group ---> ottimizzare e applicarlo a tutti.
    private IEnumerator RestartScene()
    {
        if (!started)
        {
            started = true;
            OVRScreenFade.instance.FadeOut();

            yield return new WaitForSeconds(2f);

            if (SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex).isDone)
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                Debug.Log("Retry");
            }

        }

    }


    void BackToMainMenu()
    {
        Debug.Log("Back To Main Menu");
        OVRScreenFade.instance.FadeOut();
        SceneManager.LoadScene("Main_Menu");
    }

    

}


