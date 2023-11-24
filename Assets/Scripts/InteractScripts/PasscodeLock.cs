using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PasscodeLock : FocusPuller
{
    [SerializeField] Door connectedDoor;
    [SerializeField] ObjectSpawner objectSpawner;
    [SerializeField] LightPuzzle lightPuzzleReset;

    [Tooltip("Long passwords may overflow")]
    [SerializeField] int password;
    string inputCode;

    [SerializeField] TMP_Text passcodeText;

    protected override void Init()
    {
        inputCode = 0.ToString();
        passcodeText.text = 0.ToString();
    }

    public void InputNum(int num)
    {
        if (inputCode == 0.ToString() || inputCode == "CORRECT" || inputCode == "INCORRECT")
        {
            inputCode = num.ToString();
        }
        else
        {
            inputCode += num.ToString();
        }
        passcodeText.text = inputCode;
    }
    public void Enter()
    {
        if (inputCode == password.ToString())
        {
            inputCode = "CORRECT";

            if (connectedDoor != null)
                connectedDoor.opening = true;
            if (objectSpawner != null)
                objectSpawner.ResetObject();
            if (lightPuzzleReset != null)
                lightPuzzleReset.ResetPuzzle();

            timer = 0;
            automaticExit = true;
        }
        else
        {
            inputCode = "INCORRECT";

            if (connectedDoor != null)
                connectedDoor.opening = false;
        }
        passcodeText.text = inputCode;
    }
    public void Clear()
    {
        inputCode = 0.ToString();
        passcodeText.text = 0.ToString();
    }
}