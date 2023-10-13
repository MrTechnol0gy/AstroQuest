using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DoorTrigger : MonoBehaviour
{
    Door door;

    void Start()
    {
        door = GetComponentInParent<Door>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) door.opening = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) door.opening = false;
    }
}