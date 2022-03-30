using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperController : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool debug = false;
    [SerializeField] private TMPro.TMP_Text debugText;


    [Header("Inputs")]
    [SerializeField] public Rigidbody rb;
    [SerializeField] public Animator animator;
    [SerializeField] public GameObject model;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public float groundCheckRadius = 0.1f;


    [Header("Reaper Stats")]
    [Header("Idle")]
    [SerializeField] public float rotateBackAfter = 2f;
    [SerializeField] public float rotatingBackSpeed = 0.1f;

    [Header("Running")]
    [SerializeField] public float speed = 5f;
    [SerializeField] public float maxSpeed = 500f;
    [SerializeField] public float rotatingSpeed = 10f;
    [SerializeField] public float stopSpeed = 0.1f;

    [Header("Jumping")]
    [SerializeField] public float jumpSpeed = 1;
    [SerializeField] public float maxJumpSpeed = 5f;
    [SerializeField] public int jumpCount = 2;
    [SerializeField] public float jumpTime = 0.1f;
    [SerializeField] public float jumpTimeMin = 0.06f;

    [Header("Falling")]
    [SerializeField] public float fallSpeed = 1f;
    [SerializeField] public float maxFallSpeed = 5f;

    [Header("Bats")]
    [SerializeField] public float batSpeed = 30f;
    [SerializeField] public float maxBatSpeed = 10f;
    [SerializeField] public float batTime = 3f;



    public InputData Inputs { get; private set; }
    public ReaperState CurrentState { get; private set; }

    public ReaperIdle IdleState { get; private set; }
    public ReaperRun RunState { get; private set; }
    public ReaperJump JumpState { get; private set; }
    public ReaperFall FallState { get; private set; }
    public ReaperBatDash BatState { get; private set; }



    public int JumpsLeft { get; private set; }
    public bool CanJump
    {
        get
        {
            return this.JumpsLeft > 0;
        }
    }


    public bool OnGround
    {
        get
        {
            return Physics.CheckSphere(this.groundCheck.position, this.groundCheckRadius, this.groundLayer);
        }
    }

    private void Start()
    {
        this.rb = GetComponent<Rigidbody>();

        this.JumpsLeft = jumpCount;

        CreateStates();

        this.ChangeState(this.IdleState);
    }

    private void CreateStates()
    {
        this.IdleState = new ReaperIdle(this);
        this.RunState = new ReaperRun(this);
        this.JumpState = new ReaperJump(this);
        this.FallState = new ReaperFall(this);
        this.BatState = new ReaperBatDash(this);
    }

    private void Update()
    {

#if UNITY_EDITOR
        if (this.debug)
        {
            this.debugText.text = CurrentState.ToString();
        }
#endif

        Inputs = InputController.Instance.InputData;

        this.CurrentState?.FrameUpdate();

        // RotateModel();
    }

    private void FixedUpdate()
    {
        this.CurrentState?.PhysicsUpdate();
    }

    public void RotateModel(float _speed = 10f, bool _rotateBack = false)
    {
        if (_rotateBack)
        {
            model.transform.forward = Vector3.Slerp(model.transform.forward, -Vector3.forward, Time.deltaTime * _speed);
        }
        else
        {
            model.transform.forward = Vector3.Slerp(model.transform.forward, this.rb.transform.right * this.rb.velocity.x, Time.deltaTime * _speed);
        }
    }
    
    public void ChangeState(ReaperState _newState)
    {
        this.CurrentState?.Exit();
        this.CurrentState = _newState;
        this.CurrentState?.Enter();
    }

    public void MoveHorizontal(float _speed)
    {
        Vector3 newVelocity = rb.velocity + Vector3.right * _speed;
        newVelocity.x = Mathf.Clamp(newVelocity.x, -maxSpeed, maxSpeed);
        rb.velocity = newVelocity;
    }

    public void MoveVertical(float _speed)
    {
        Vector3 newVelocity = rb.velocity + Vector3.up * _speed;
        newVelocity.y = Mathf.Clamp(newVelocity.y, -fallSpeed, maxJumpSpeed);
        rb.velocity = newVelocity;
    }

    public void ResetVertical()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }

    public void SetTargetVelocityHorizontal(float _velocity, float _speed = 10f)
    {
        Vector3 newVelocity = rb.velocity;
        newVelocity.x = Mathf.Lerp(newVelocity.x, _velocity, _speed);
        rb.velocity = newVelocity;
    }

    public void SetTargetVelocityVertical(float _velocity, float _speed = 10f)
    {
        Vector3 newVelocity = rb.velocity;
        newVelocity.y = Mathf.Lerp(newVelocity.y, _velocity, _speed);
        rb.velocity = newVelocity;
    }

    public void UseJump()
    {
        if (this.JumpsLeft <= 0) return;

        this.JumpsLeft--;
    }

    public void ResetJump()
    {
        this.JumpsLeft = jumpCount;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
#endif
}
