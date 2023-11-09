using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzzle : MonoBehaviour
{
    [SerializeField] Door connectedDoor;
    [SerializeField] ObjectSpawner objectSpawner;
    [SerializeField] LightPuzzle lightPuzzleReset;

    [SerializeField] List<LightPlate> lightPlates;
    bool solved;

    public void OnPlateTrigger()
    {
        foreach (LightPlate lightPlate in lightPlates)
        {
            if (!lightPlate.triggered)
            {
                UnSolve();
                return;
            }
        }
        Solve();
    }

    void Solve()
    {
        if (solved) return;

        if (connectedDoor != null)
            connectedDoor.opening = true;
        if (objectSpawner != null)
            objectSpawner.ResetObject();
        if (lightPuzzleReset != null)
            lightPuzzleReset.ResetPuzzle();

        solved = true;
    }
    void UnSolve()
    {
        if (!solved) return;
        if (connectedDoor != null)
            connectedDoor.opening = false;

        solved = false;
    }

    public void ResetPuzzle()
    {
        foreach (LightPlate plate in  lightPlates)
        {
            if (plate.startTriggered) plate.Trigger(false);
            else plate.UnTrigger(false);
        }    
    }
}