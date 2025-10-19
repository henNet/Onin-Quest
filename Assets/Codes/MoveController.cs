using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour
{
    public InputActionAsset inputActions;

    private InputAction moveAction;
    private InputAction jumpAction;

    private Vector2 moveInput;

    private Rigidbody2D rigidBody;
    [SerializeField] private float moveSpeed;

    private Animator animator;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundCheck;
    public bool isGrounded = false;
    public LayerMask layerGroundMask;

    private bool facingRight = true;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CollisionCheck();
        AnminationControls();
        FlipController();

        moveInput = moveAction.ReadValue<Vector2>();

        Jump();
    }

    private void AnminationControls()
    {
        animator.SetFloat("xVelocity", rigidBody.linearVelocityX);
        animator.SetFloat("yVelocity", rigidBody.linearVelocityY);
        animator.SetBool("IsGrounded", isGrounded);
    }

    void FixedUpdate()
    {
        Walk();
    }

    void Jump()
    {
        if (isGrounded && jumpAction.WasPressedThisFrame())
        {
            rigidBody.AddForceAtPosition(
                new Vector2(0f, 5f), Vector2.up, ForceMode2D.Impulse);
        }
    }

    void Walk()
    {
        // rigidBody.MovePosition(
        //     rigidBody.position + moveInput * moveSpeed * Time.deltaTime);

        rigidBody.linearVelocity =
            new Vector2(moveInput.x * moveSpeed, rigidBody.linearVelocityY);

    }

    private void FlipController()
    {
        if (rigidBody.linearVelocityX > 0 && !facingRight)
            Flip();
        else if (rigidBody.linearVelocityX < 0 && facingRight)
            Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void CollisionCheck()
    {
        isGrounded =
        Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, layerGroundMask) 
        ? true : false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
