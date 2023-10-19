using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PressurePlate : MonoBehaviour
{
    Transform plateCube;
    [SerializeField] Door connectedDoor;

    public bool triggered;

    void Start()
    {
        plateCube = transform.GetChild(0);
    }

    public void Trigger()
    {
        if (triggered) return;
        plateCube.transform.position -= new Vector3(0, .075f, 0);
        if (connectedDoor != null)
            connectedDoor.opening = true;
        triggered = true;
    }
    public void UnTrigger()
    {
        if (!triggered) return;
        plateCube.transform.position += new Vector3(0, .075f, 0);
        if (connectedDoor != null)
            connectedDoor.opening = false;
        triggered = false;
    }
}