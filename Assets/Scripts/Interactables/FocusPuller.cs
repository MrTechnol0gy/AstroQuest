using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPuller : Interactable
{
    FocusPull focusPull;
    bool focusedOn;
    Canvas canvas;

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        focusedOn = false;
        focusPull = FindObjectOfType<FocusPull>();
        Init();
    }

    protected virtual void Init(){}

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && focusedOn) Deactivate();
    }

    public override void Activate()
    {
        focusPull.Focus();
        focusedOn = true;
    }

    public void Deactivate()
    {
        focusPull.StopFocus();
        focusedOn = false;
    }
}