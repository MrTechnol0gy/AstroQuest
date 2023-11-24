using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractable : Interactable
{
    [SerializeField] Door connectedDoor;
    [SerializeField] ObjectSpawner objectSpawner;
    [SerializeField] LightPuzzle lightPuzzleReset;
    public override void Activate()
    {
        if (connectedDoor != null)
        {
            if (connectedDoor.opening) connectedDoor.opening = false;
            else connectedDoor.opening = true;
        }
        if (objectSpawner != null)
            objectSpawner.ResetObject();
        if (lightPuzzleReset != null)
            lightPuzzleReset.ResetPuzzle();
    }
}