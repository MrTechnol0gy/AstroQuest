using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateTrigger : MonoBehaviour
{
    PressurePlate plate;

    void Start()
    {
        plate = GetComponentInParent<PressurePlate>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlateTrigger")) plate.Trigger();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlateTrigger")) plate.UnTrigger();
    }
}