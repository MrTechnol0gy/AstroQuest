using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPuller : Interactable
{
    FocusPull focusPull;
    bool focusedOn;
    Canvas canvas;

    protected bool automaticExit;
    protected float timer;

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
        focusedOn = false;
        focusPull = FindObjectOfType<FocusPull>();
        Init();
    }

    protected virtual void Init(){}

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && focusedOn) Deactivate();

        // timer
        if (automaticExit)
        {
            timer += Time.deltaTime;
            if (timer >= .666f)
            {
                automaticExit = false;
                Deactivate();
            }
        }
    }

    public override void Activate()
    {
        focusPull.Focus();
        focusedOn = true;
        canvas.enabled = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Deactivate()
    {
        focusPull.StopFocus();
        focusedOn = false;
        canvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}