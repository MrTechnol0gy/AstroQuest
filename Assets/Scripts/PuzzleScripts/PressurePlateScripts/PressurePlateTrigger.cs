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
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("PuzzleObj")) plate.Trigger();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("PuzzleObj")) plate.UnTrigger();
    }
}