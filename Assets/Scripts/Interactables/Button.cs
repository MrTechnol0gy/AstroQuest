using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable
{
    [SerializeField] Door connectedDoor;
    [SerializeField] ObjectSpawner objectSpawner;
    public override void Activate()
    {
        if (connectedDoor != null)
        {
            if (connectedDoor.opening) connectedDoor.opening = false;
            else connectedDoor.opening = true;
        }
        if (objectSpawner != null)
            objectSpawner.ResetObject();
    }
}