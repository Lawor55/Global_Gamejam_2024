using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputManager input;

    private void Awake()
    {

        input = new PlayerInputManager();
        input.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = input.Player.Move.ReadValue<Vector2>();

        
        return inputVector;
    }

}
