using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputData
{
    public Vector2 Dir { get; set; }
    public bool Jump { get; set; }
    public bool Bats { get; set; }
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

    public void OnBats(InputAction.CallbackContext _context)
    {
        if (_context.phase == InputActionPhase.Started)
            InputData.Bats = true;
        else if (_context.phase == InputActionPhase.Canceled)
            InputData.Bats = false;
    }


    public void OnJump(InputAction.CallbackContext _context)
    {
        if (_context.phase == InputActionPhase.Started)
            StartJump();
        else if (_context.phase == InputActionPhase.Canceled && InputData.Jump)
            UseJump();

    }
    public void StartJump()
    {
        InputData.Jump = true;
    }

    public void UseJump()
    {
        InputData.Jump = false;
    }

    public void UseBats()
    {
        InputData.Bats = false;
    }
}
