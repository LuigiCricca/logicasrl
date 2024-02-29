using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollider : MonoBehaviour
{

    Collider wall;

    private void Awake() {
        wall = GetComponent<Collider>();
        wall.enabled = false;
    }
    private void OnEnable()
    {
        NewSphereMovement.onOKClicked += ActivateCollider;
    }
    private void OnDisable()
    {
        NewSphereMovement.onOKClicked -= ActivateCollider;
    }
    void ActivateCollider()
    {
        Invoke("ColliderState", 1f);
    }
    void ColliderState()
    {
        wall.enabled = true;
    }

}
