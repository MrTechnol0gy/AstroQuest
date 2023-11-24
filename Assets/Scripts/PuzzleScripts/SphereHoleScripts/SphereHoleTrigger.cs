using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereHoleTrigger : MonoBehaviour
{
    SphereHole plate;

    void Start()
    {
        plate = GetComponentInParent<SphereHole>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PuzzleSphere")) plate.Trigger();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PuzzleSphere") || other.gameObject.layer == LayerMask.NameToLayer("PuzzleObj")) plate.UnTrigger();
    }
}