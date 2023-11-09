using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereHole : MonoBehaviour
{
    [SerializeField] Door connectedDoor;
    [SerializeField] ObjectSpawner objectSpawner;
    [SerializeField] LightPuzzle lightPuzzleReset;

    public bool triggered;

    public void Trigger()
    {
        if (triggered) return;

        if (connectedDoor != null)
            connectedDoor.opening = true;
        if (objectSpawner != null)
            objectSpawner.ResetObject();
        if (lightPuzzleReset != null)
            lightPuzzleReset.ResetPuzzle();

        triggered = true;
    }
    public void UnTrigger()
    {
        if (!triggered) return;
        if (connectedDoor != null)
            connectedDoor.opening = false;
        triggered = false;
    }
}