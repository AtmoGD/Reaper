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


    [Header("Reaper Stats")]
    [Header("Running")]
    [SerializeField] public float speed = 500f;
    // [SerializeField] public float maxSpeed = 500f;
    // [SerializeField] public float movementLerpSpeed = 10f;
    // [SerializeField] public float movementThreshold = 0.1f;
    [Header("Jumping")]
    // [SerializeField] public AnimationCurve jumpCurve;
    // [SerializeField] public float jumpSpeed = 2;
    // [SerializeField] public float maxJumpSpeed = 0.5f;
    // [SerializeField] public float jumpLerpSpeed = 10f;
    // [SerializeField] public float jumpTime = 0.5f;
    // [SerializeField] public float JumpTimeout = 0.1f;
    [SerializeField] public int jumpCount = 2;

    [Header("Falling")]
    // [SerializeField] public float coyotyTime = 0.5f;
    [SerializeField] public float gravity = 1f;
    // [SerializeField] public float fallLerpSpeed = 10f;
    // [SerializeField] public float maxFallSpeed = 0.5f;

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
        // print("Changed state to " + _newState.GetType().Name);
        this.CurrentState?.Enter();
    }

    public void MoveHorizontal(float _speed)
    {
        Vector2 newVelocity = rb.velocity + Vector2.right * _speed;
        // newVelocity.x = Mathf.Clamp(newVelocity.x, -maxSpeed, maxSpeed);

        this.Move(newVelocity);
    }

    public void Move(Vector2 _newVel) {
        // this.rb.velocity = Vector2.Lerp(this.rb.velocity, _newVel, movementLerpSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        if (this.CanJump)
        {
            this.JumpsLeft--;
            // this.JumpCooldown = this.JumpTimeout;
            // this.rb.AddForce(Vector2.up * this.jumpSpeed, ForceMode2D.Impulse);
        }
    }

    public void ApplyGravity()
    {
        Vector2 newVelocity = rb.velocity + Vector2.down * this.gravity;
        // newVelocity.y = Mathf.Clamp(newVelocity.y, -maxFallSpeed, maxFallSpeed);
        this.Move(newVelocity);
    }

    public void SetJumpCooldown()
    {
        // this.JumpCooldown = this.JumpTimeout;
    }

    public void UseJump()
    {
        if (this.JumpsLeft <= 0) return;

        this.JumpsLeft--;
        SetJumpCooldown();
        InputController.Instance.UseJump();
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
