using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputData
{
    public Vector2 Dir { get; set; }

    public bool Jump { get; set; }
    public float JumpStrength { get; set; }
    public float JumpStartTime { get; set; }
    public float JumpEndTime { get; set; }

    public bool BatDash { get; set; }
    public float BatDashStartTime { get; set; }
    public float BatDashEndTime { get; set; }

}

public class InputController : MonoBehaviour
{
    // [SerializeField] private float strengthMultiplier = 1.6f;
    // [SerializeField] private float strengthMin = 0.8f;
    // [SerializeField] private float strengthMax = 10f;
    // [SerializeField] private float coyotyTime = 0.2f;
    [SerializeField] private float jumpTime = 0.5f;
    [SerializeField] private float coyotyTime = 0.2f;
    public static InputController Instance { get; private set; }
    public InputData InputData { get; private set; }

    private float jumpStartTime = 0;
    private float jumpEndTime = 0;
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
        if (InputData.Jump && Time.time - InputData.JumpStartTime > coyotyTime)
        {
            EndJump();
        }

        // if (InputData.Jump && (Time.time - InputData.JumpStartTime) >= jumpTime)
        // {
        //     EndJump();
        // }

        if (InputData.JumpStartTime > 0f)
            InputData.JumpStrength = jumpTime / (Time.time - InputData.JumpStartTime);
            // Debug.Log("Jump Value: " + (Time.time - InputData.JumpStartTime));
        if (InputData.JumpStartTime > 0f && Time.time - InputData.JumpStartTime >= jumpTime)
        {
            // Debug.Log(Time.time - InputData.JumpStartTime);
            // InputData.JumpStartTime = 0f;
            // InputData.JumpEndTime = 0f;
            StartJump();
        }

    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        InputData.Dir = _context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext _context)
    {
        if (_context.phase == InputActionPhase.Started)
            InputData.JumpStartTime = Time.time;
        else if (_context.phase == InputActionPhase.Canceled && InputData.Jump)
            StartJump();

    }
    public void StartJump()
    {
        if (InputData.Jump) return;

        // InputData.JumpStrength = (Time.time - InputData.JumpStartTime) / jumpTime;
        InputData.Jump = true;
        InputData.JumpEndTime = Time.time;
        // float jumpValue = Mathf.Clamp(InputData.JumpEndTime - InputData.JumpStartTime, 0, jumpTime);
        // <-------------------------------------------------- DAS MUSS IN JUMP BZW MOVING PASSIEREN -------------------->
        // InputData.JumpStrength = map((Time.time - InputData.JumpStartTime), 0, jumpTime, 0, 1);

        // InputData.JumpStartTime = 0f;
        // InputData.JumpStrength = Mathf.Clamp((Time.time - InputData.JumpStartTime) * strengthMultiplier, strengthMin, strengthMax);

    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    public void EndJump()
    {
        InputData.Jump = false;
        InputData.JumpStrength = 0f;
        InputData.JumpStartTime = 0;
        InputData.JumpEndTime = 0;
    }

    public void OnBatDash(InputAction.CallbackContext _context)
    {
        if (_context.phase == InputActionPhase.Started)
            BatDashStart();
        else if (_context.phase == InputActionPhase.Canceled)
            BatDashEnd();
    }

    public void BatDashStart()
    {
        InputData.BatDash = true;
        InputData.BatDashStartTime = Time.time;
    }

    public void BatDashEnd()
    {
        InputData.BatDash = false;
        InputData.BatDashEndTime = Time.time;
    }
}
