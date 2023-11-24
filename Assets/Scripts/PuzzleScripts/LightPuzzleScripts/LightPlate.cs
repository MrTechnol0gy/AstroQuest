using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlate : MonoBehaviour
{
    Light puzzleLight;

    [HideInInspector] public bool triggered;
    LightPuzzle lightPuzzle;
    public bool startTriggered;

    [SerializeField] List<LightPlate> adjacentPlates;

    void Start()
    {
        triggered = false;
        lightPuzzle = GetComponentInParent<LightPuzzle>();
        puzzleLight = GetComponentInChildren<Light>();

        if (startTriggered)
        {
            Trigger(false);
        }
        else
        {
            puzzleLight.enabled = false;
            triggered = false;
        }
    }

    public void Trigger(bool callAdjacent)
    {
        if (triggered) return;

        triggered = true;
        puzzleLight.enabled = true;
        lightPuzzle.OnPlateTrigger();
        if (callAdjacent) TriggerAdjacentPlates();
    }
    public void UnTrigger(bool callAdjacent)
    {
        if (!triggered) return;

        triggered = false;
        puzzleLight.enabled = false;
        lightPuzzle.OnPlateTrigger();
        if (callAdjacent) TriggerAdjacentPlates();
    }

    void TriggerAdjacentPlates()
    {
        foreach (LightPlate plate in adjacentPlates)
        {
            if (plate.triggered) plate.UnTrigger(false);
            else plate.Trigger(false);
        }
    }
}