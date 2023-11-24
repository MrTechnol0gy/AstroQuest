using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FocusPull : MonoBehaviour
{
    ThirdPersonController controller;

    private void Start()
    {
        controller = GetComponent<ThirdPersonController>();
    }

    public void Focus()
    {
        controller.paused = true;
    }
    public void StopFocus()
    {
        controller.paused = false;
    }
}