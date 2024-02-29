using UnityEngine;

public class RoofCollider : MonoBehaviour
{
    bool disabled;
    [SerializeField] RoofRealtimeComponent roofRealtimel;
   
    void Start()
    {
        disabled = false; 
    }

    
    void Update()
    {

        //if(GameManagerP1.instance.spawnComplete && GameManager.instance.spawnComplete && !disabled) 
        {
            GetComponent<Collider>().enabled = false;
            disabled = true;
            Debug.LogWarning("Ho disattivato il collider del muro");

            roofRealtimel.ColliderState(false);
        }
        
    }
}
