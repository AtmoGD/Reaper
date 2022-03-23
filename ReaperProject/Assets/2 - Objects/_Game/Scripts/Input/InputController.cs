using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputData
{
    public Vector2 Dir { get; set; }

    public bool Jump { get; set; }
    // public float JumpStrength { get; set; }
    public float JumpStartTime { get; set; }
    public float JumpEndTime { get; set; }
}

public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }
    public InputData InputData { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            InputData = new InputData();
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {

    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        InputData.Dir = _context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext _context)
    {
        if (_context.phase == InputActionPhase.Started)
            StartJump();
        else if (_context.phase == InputActionPhase.Canceled && InputData.Jump)
            EndJump();

    }
    public void StartJump()
    {
        if (InputData.Jump) return;

        InputData.Jump = true;
        InputData.JumpStartTime = Time.time;
    }

    public void EndJump()
    {
        InputData.Jump = false;
        InputData.JumpEndTime = Time.time;
    }

    public void UseJump() {
        InputData.Jump = false;
        // InputData.JumpStrength = 0f;
        InputData.JumpStartTime = 0;
        InputData.JumpEndTime = 0;
    }

    public void OnBatDash(InputAction.CallbackContext _context)
    {
        // if (_context.phase == InputActionPhase.Started)
        //     BatDashStart();
        // else if (_context.phase == InputActionPhase.Canceled)
        //     BatDashEnd();
    }
}
