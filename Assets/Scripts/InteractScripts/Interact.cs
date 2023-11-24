using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    Interactable currentObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InteractZone"))
        {
            if (currentObj != null) currentObj.GetComponent<Outline>().enabled = false;
            currentObj = other.GetComponent<Interactable>();
            currentObj.GetComponent<Outline>().enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InteractZone"))
        {
            if (currentObj == other) currentObj = null;
            other.GetComponent<Outline>().enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            InteractWithObj();
    }

    void InteractWithObj()
    {
        if (currentObj != null)
            currentObj.Activate();
    }
}