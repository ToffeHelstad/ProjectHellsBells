using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerOneWheel : MonoBehaviour
{
    private PlayerChar playerChar;
    private InputAction movement;

    private void Awake()
    {
        playerChar = new PlayerChar();
    }

    private void OnEnable()
    {
        movement = playerChar.Player.Movement;
        movement.Enable();

        playerChar.Player.Jump.performed += DoJump;
        playerChar.Player.Jump.Enable();
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        Debug.Log("Jumping!");
    }

    private void OnDisable()
    {
        movement.Disable();
        playerChar.Player.Jump.Disable();
    }

    private void FixedUpdate()
    {
        Debug.Log("Movement Values" + movement.ReadValue<Vector2>());
    }
}
