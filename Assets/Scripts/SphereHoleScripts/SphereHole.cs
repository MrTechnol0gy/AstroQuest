using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class SphereHole : MonoBehaviour
{
    [SerializeField] Door connectedDoor;
    [SerializeField] ObjectSpawner objectSpawner;

    public bool triggered;

    public void Trigger()
    {
        if (triggered) return;

        if (connectedDoor != null)
            connectedDoor.opening = true;
        if (objectSpawner != null)
            objectSpawner.ResetObject();

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