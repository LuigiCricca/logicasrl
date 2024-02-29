using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;




public class ProcessJson : MonoBehaviour
{
    bool invalidActivation;
    bool internetError;
    public static bool okClicked;
    bool closed;
    [SerializeField] GameObject UIPanel;
    string sourceURL = "http://www.gittaglab.com/other/licenze/-/raw/master/logic_game.json";
   
    string serverIP = "217.112.95.188";
    
    Ping ping;
    

    Image uiImage;
    [SerializeField]Sprite[] uiSprites;
    
    private void Awake()
    {
        okClicked = false;
        closed = false;
            UIPanel.gameObject.SetActive(true);
        
        
        if(UIPanel.TryGetComponent(out Image image))
        {
            uiImage = image;
            
        }

        ping = new Ping(serverIP);
    }
    void Start()
    {
        UIPanel.SetActive(false);
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            // Debug.LogError("internet ok");
            
            //ping = new Ping(serverIP);
            //StartCoroutine(GetJsonData(sourceURL, OnFail, ApiAction));

        }
        else
        {
            internetError = true;
            InvalidApiCall();
            
        }
        
    }

    private void Update() // -- > posso toglierla e far gestire tutto dall'hand UI Interaction
    {
        if (okClicked && !closed)
        {
            closed = true;
            Invoke("WaitBeforeClosing", 1f);
        }

        if (ping!=null)
        {
            if (ping.isDone)
            {
                Debug.Log(ping.time);
                StartCoroutine(GetJsonData(sourceURL, OnFail, ApiAction));
                ping = null;
            }
            else
            {
                Invoke("CheckConnection", 2f);
            }

            

        }

        
        


    }

    private IEnumerator GetJsonData (string url,UnityAction<bool> callBakOnFail, UnityAction<string> callBakOnSuccess)
    {
       
        UnityWebRequest webRequest = new UnityWebRequest (url);

            using (UnityWebRequest webData = UnityWebRequest.Get(url))
            {
                yield return webData.SendWebRequest();

                if (webData.result == UnityWebRequest.Result.ConnectionError || webData.result == UnityWebRequest.Result.ProtocolError) // errori con il server
                {
                
                    callBakOnFail(false);

                }
               
                else
                {
                    try
                    {
                        LicenseData resourceFile = JsonUtility.FromJson<LicenseData>(webData.downloadHandler.text);

                        if (webData.isDone)
                        {
                          
                            if (resourceFile != null)
                            {
                                if (resourceFile.active)
                                {
                                    // faipartire il gioco
                                    callBakOnSuccess("riuscito");
                                    if (SceneManager.LoadSceneAsync(1).isDone)
                                    {
                                        SceneManager.LoadScene("Main_Menu");

                                    }

                                }
                                else
                                {
                                    if (!resourceFile.active)
                                    {
                                        callBakOnFail(false);

                                    }
                        
                                }
                            }

                        }
                    }

                    catch (System.ArgumentException e)
                    {
                        Debug.Log(e);
                        callBakOnFail(false);

                    }


                }
            
      }
     
    }
    void ApiAction(string result)
    {
        Debug.Log(result);
    }
    private void OnFail(bool foundFile)
    {
        if (!foundFile)
        {
            invalidActivation = true;
            InvalidApiCall();
        }
    }
    void InvalidApiCall()
    {
        UIPanel.SetActive(true);
        if (invalidActivation)
        {
            uiImage.sprite = uiSprites[1];
        }
        if (internetError)
        {
            uiImage.sprite = uiSprites[0];
        }

        //Invoke("WaitBeforeClosing",time);
    }
    void WaitBeforeClosing()
    {
        if (okClicked)
        {
            //System.Diagnostics.Process.GetCurrentProcess().Kill();
            Debug.LogWarning("Chiudi l'app");
            Application.Quit();
        }

    }

    void CheckConnection()
    {
        if (ping.time < 0)
        {
            internetError = true;
            InvalidApiCall();
        }
    }

}
