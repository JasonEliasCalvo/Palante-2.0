using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    BaseState currentState;

    public IdleState _idle;
    public WalkState _walk;
    public JumpState _jump;
    public FallState _fall;

    public Animator anim;
    public Rigidbody rigid;
    public float horizontal;
    public float vertical;

    public KeyCode jumpKey = KeyCode.Space;

    public float speedMovement;
    public float jumpForce;

    [SerializeField] private float speedRotation;
    public bool isGrounded;

    [SerializeField] LayerMask layer;
    [SerializeField] private Transform pivotOrigin;
    [SerializeField] private Vector3 detectecZone;

    bool movementState;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();

        _idle = new IdleState(this);
        _walk = new WalkState(this);
        _jump = new JumpState(this);
        _fall = new FallState(this);
        ChangeState(_idle);

        GameManager.instance.eventGameStart += ActiveMovement;
        GameManager.instance.eventGameEnd += DeactivateMovement;
    }

    void Update()
    {
        AnimationsParemeter();
        if (movementState)
        {
            currentState.UpdateState();
            Inputs();
        }
    }

    private void Inputs()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (movementState)
        {
            currentState.FixedUpdateState();
        }

        DetectedGround();
    }

    private void DetectedGround()
    {
        Collider[] collide = Physics.OverlapBox(pivotOrigin.position, detectecZone, pivotOrigin.rotation, layer);
        isGrounded = collide.Length > 0;
    }

    public void ChangeState(BaseState nextState)
    {
        currentState = nextState;
        currentState.EnterState();
    }

    private void AnimationsParemeter()
    {
        anim.SetFloat("Altitude", rigid.velocity.y);
        anim.SetFloat("Movement", vertical);
        anim.SetFloat("Movement", horizontal);
        anim.SetBool("IsGrounded", isGrounded);
    }

    public void ActiveMovement()
    {
        movementState = true;
    }

    public void DeactivateMovement()
    {
        movementState = false;
        horizontal = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(pivotOrigin.position, detectecZone); ;
    }
}
