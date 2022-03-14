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

    [SerializeField] public Transform topCheck;
    [SerializeField] public LayerMask topLayer;
    [SerializeField] public float topCheckRadius = 0.1f;

    [Header("Reaper Stats")]
    [Header("Moving")]
    [SerializeField] public float speed = 5f;

    [Header("Jumping")]
    [SerializeField] public float jumpHeight = 5f;
    [SerializeField] public float jumpTime = 2f;
    [SerializeField] public float jumpMovementSpeed = 2f;
    [SerializeField] public AnimationCurve jumpCurve;
    [SerializeField] public int jumpAmount = 2;

    [Header("Falling")]
    [SerializeField] public float fallSpeed = 2.5f;

    [Header("Dash")]
    [SerializeField] public float dashMaxCastTime = 2.5f;
    [SerializeField] public float dashDistance = 5f;
    [SerializeField] public float dashSpeed = 5f;
    [SerializeField] public AnimationCurve dashCurve;
    [SerializeField] public float dashMovementSpeed = 2f;

    public InputData Inputs { get; private set; }
    public ReaperState CurrentState { get; private set; }

    public ReaperIdle IdleState { get; private set; }
    public ReaperGetHit GetHitState { get; private set; }
    public ReaperStunned StunnedState { get; private set; }
    public ReaperDead DeadState { get; private set; }
    public ReaperMoving MovingState { get; private set; }
    public ReaperJump JumpingState { get; private set; }
    public ReaperFalling FallingState { get; private set; }
    public ReaperBatAttack BatAttackState { get; private set; }
    public ReaperBatCast BatDashCastState { get; private set; }
    public ReaperBatDash BatDashState { get; private set; }
    public ReaperBatExposion BatExposionState { get; private set; }
    public ReaperScytheAttack ScytheAttackState { get; private set; }
    public ReaperScytheCast ScytheCasteState { get; private set; }
    public ReaperScytheDown ScytheDownState { get; private set; }
    public ReaperSkullAttack SkullAttackState { get; private set; }
    public ReaperSkullCast SkullCastState { get; private set; }


    public int JumpsLeft { get; private set; }
    public bool CanJump { get { return JumpsLeft > 0; } }

    public bool OnGround { 
        get {
            bool onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            if(onGround)
                ResetJump();

            return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }
    }

    public bool OnTop {
        get {
            return Physics2D.OverlapCircle(topCheck.position, topCheckRadius, topLayer);
        }
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();

        JumpsLeft = jumpAmount;

        IdleState = new ReaperIdle(this);
        GetHitState = new ReaperGetHit(this);
        StunnedState = new ReaperStunned(this);
        DeadState = new ReaperDead(this);
        MovingState = new ReaperMoving(this);
        JumpingState = new ReaperJump(this);
        FallingState = new ReaperFalling(this);
        BatAttackState = new ReaperBatAttack(this);
        BatDashCastState = new ReaperBatCast(this);
        BatDashState = new ReaperBatDash(this);
        BatExposionState = new ReaperBatExposion(this);
        ScytheAttackState = new ReaperScytheAttack(this);
        ScytheCasteState = new ReaperScytheCast(this);
        ScytheDownState = new ReaperScytheDown(this);
        SkullAttackState = new ReaperSkullAttack(this);
        SkullCastState = new ReaperSkullCast(this);

        CurrentState = IdleState;
        CurrentState.Enter();
    }
    private void Update() {
        Inputs = InputController.Instance.InputData;
        CurrentState?.FrameUpdate();

        print(CurrentState.GetType().Name);
    }
    private void FixedUpdate() {
        CurrentState?.PhysicsUpdate();
    }

    public void ChangeState(ReaperState _newState)
    {
        CurrentState?.Exit();
        CurrentState = _newState;
        CurrentState?.Enter();
    }

    public void UseJump() {
        if(JumpsLeft > 0)
            JumpsLeft--;
    }

    public void ResetJump()
    {
        JumpsLeft = jumpAmount;
    }
}
