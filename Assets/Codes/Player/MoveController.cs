using UnityEngine;

public class MoveController : MonoBehaviour
{
    private InputController input;

    private Vector2 moveInput;

    private Rigidbody2D rigidBody;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private Animator animator;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundCheck;
    public bool isGrounded = false;
    public LayerMask layerGroundMask;

    private bool facingRight = true;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        input = GetComponent<InputController>();
    }

    void Update()
    {
        CollisionCheck();
        AnminationControls();
        FlipController();
        // FlipMouseController();

        // moveInput = input.GetMoveAction();
        moveInput = input.GetMoveActionOld();

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
        if (isGrounded && input.GetJumpAction())
        {
            rigidBody.AddForceAtPosition(
                new Vector2(0f, jumpForce), Vector2.up, ForceMode2D.Impulse);
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

    private void FlipMouseController()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x > transform.position.x && !facingRight)
            Flip();
        else if (mousePos.x < transform.position.x && facingRight)
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

    public void StartDeath()
    {
        animator.SetTrigger("Dead");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
