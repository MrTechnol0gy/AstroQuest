using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    Transform plateCube;
    [SerializeField] Door connectedDoor;
    [SerializeField] ObjectSpawner objectSpawner;
    [SerializeField] LightPuzzle lightPuzzleReset;

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
        if (objectSpawner != null)
            objectSpawner.ResetObject();
        if (lightPuzzleReset != null)
            lightPuzzleReset.ResetPuzzle();

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