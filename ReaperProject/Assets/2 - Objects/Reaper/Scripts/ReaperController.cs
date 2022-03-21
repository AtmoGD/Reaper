using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperController : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Animator animator;

    [SerializeField] public Transform groundCheck;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public float groundCheckRadius = 0.1f;

    // [SerializeField] public Transform topCheck;
    // [SerializeField] public LayerMask topLayer;
    // [SerializeField] public float topCheckRadius = 0.1f;

    [SerializeField] public float movementThreshold = 0.1f;

    [Header("Reaper Stats")]
    [SerializeField] public float lerpMultiplier = 1000f;
    [Header("Running")]
    // [SerializeField] public float accelerationLerpSpeed = 5f;
    // [SerializeField] public float decelerationLerpSpeed = 5f;

    [SerializeField] public AnimationCurve accelerationCurve;
    [SerializeField] public float accelerationMultiplier = 500f;
    // [SerializeField] public float maxSpeed = 5000f;

    [Header("Jumping")]
    [SerializeField] public AnimationCurve jumpCurve;
    [SerializeField] public float jumpForce = 2;
    [SerializeField] public float jumpTime = 0.5f;
    [SerializeField] public float JumpTimeout = 0.1f;
    [SerializeField] public int jumpCount = 2;

    [Header("Falling")]
    [SerializeField] public AnimationCurve fallCurve;
    [SerializeField] public float fallSpeed = 2.5f;
    [SerializeField] public float coyotyTime = 0.5f;

    public InputData Inputs { get; private set; }
    public ReaperState CurrentState { get; private set; }

    public ReaperIdle IdleState { get; private set; }
    public ReaperMoving MovingState { get; private set; }
    public ReaperJump JumpingState { get; private set; }
    public ReaperFalling FallingState { get; private set; }


    public int JumpsLeft { get; private set; }
    public bool CanJump
    {
        get
        {
            return this.JumpsLeft > 0 && JumpCooldown <= 0f;
        }
    }


    public float JumpCooldown { get; private set; }
    public bool OnGround
    {
        get
        {
            bool onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            if (onGround && JumpCooldown < 0f)
                ResetJump();

            return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }
    }

    // public bool OnTop
    // {
    //     get
    //     {
    //         return Physics2D.OverlapCircle(topCheck.position, topCheckRadius, topLayer);
    //     }
    // }

    private void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();

        this.JumpsLeft = jumpCount;

        CreateStates();

        this.ChangeState(this.IdleState);
    }

    private void CreateStates()
    {
        this.IdleState = new ReaperIdle(this);
        this.MovingState = new ReaperMoving(this);
        this.JumpingState = new ReaperJump(this);
        this.FallingState = new ReaperFalling(this);
    }

    private void Update()
    {
        JumpCooldown -= Time.deltaTime;
        Inputs = InputController.Instance.InputData;
        this.CurrentState?.FrameUpdate();
    }

    private void FixedUpdate()
    {
        this.CurrentState?.PhysicsUpdate();
    }

    public void ChangeState(ReaperState _newState)
    {
        this.CurrentState?.Exit();
        this.CurrentState = _newState;
        this.CurrentState?.Enter();
    }

    public void SetJumpCooldown()
    {
        this.JumpCooldown = this.JumpTimeout;
    }

    public void UseJump()
    {
        if (this.JumpsLeft > 0) {
            this.JumpsLeft--;
            this.JumpCooldown = this.JumpTimeout;
            // InputController.Instance.EndJump();
            Debug.Log("Used jump. Jumps left: " + this.JumpsLeft);
        }
    }

    public void ResetJump()
    {
        // if (JumpCooldown <= 0f)
        this.JumpsLeft = jumpCount;
        Debug.Log("Reset Jump. Jumps left: " + this.JumpsLeft);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        // Gizmos.color = Color.blue;
        // Gizmos.DrawWireSphere(topCheck.position, topCheckRadius);
    }
}
