using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlateTrigger : MonoBehaviour
{
    LightPlate plate;

    void Start()
    {
        plate = GetComponentInParent<LightPlate>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("PuzzleObj"))
        {
            if (plate.triggered) plate.UnTrigger(true);
            else plate.Trigger(true);
        }
    }
}