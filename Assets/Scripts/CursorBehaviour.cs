using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehaviour : MonoBehaviour
{
    //A simple script to turn on/off cursor visibility and lockstate. Toggle with escape.

    private bool cursorEnabled;

    private void Start()
    {
        cursorEnabled = true;
        CursorTrigger();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CursorTrigger();
        }
    }

    private void CursorTrigger()
    {
        cursorEnabled = !cursorEnabled;
        Cursor.visible = cursorEnabled;
        if (cursorEnabled) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
    }
}
