
//using System.Collections;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class Jump_Player : MonoBehaviour
//{
//    #region Vari�veis


//    [SerializeField] private float maxJumpForce = 10f;
//    [SerializeField] private float holdForce = 5f;



//    [SerializeField] private float groundCheck = 0.1f;
//    [SerializeField] private float wallCheckDistance = 0.2f;
//    [SerializeField] private float knockbackForce = 5f;
//    [SerializeField] private LayerMask groundLayer;

//    public InputActionReference JumpAction;
//    public InputActionReference TouchPositionAction;

//    private float charge;
//    private bool isGrounded;
//    private bool forceUnGround;
//    private bool isCharging;

//    private Vector2 touchPosition;
//    private Rigidbody2D rigidBody;
//    private SpriteRenderer spriteRenderer;
//    private BoxCollider2D boxCollider;

//    #endregion

//    private void Awake()
//    {
//        rigidBody = GetComponent<Rigidbody2D>();
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        boxCollider = GetComponent<BoxCollider2D>();

//        if (JumpAction != null)
//        {
//            JumpAction.action.performed += OnJumpPerformed;
//            JumpAction.action.canceled += OnJumpCanceled;
//            JumpAction.action.Enable();
//        }

//        if (TouchPositionAction != null)
//        {
//            TouchPositionAction.action.performed += OnTouchPosition;
//            TouchPositionAction.action.Enable();
//        }
//    }

//    private void OnDestroy()
//    {
//        if (JumpAction != null)
//        {
//            JumpAction.action.performed -= OnJumpPerformed;
//            JumpAction.action.canceled -= OnJumpCanceled;
//        }

//        if (TouchPositionAction != null)
//        {
//            TouchPositionAction.action.performed -= OnTouchPosition;
//        }
//    }

//    private void Update()
//    {
//        GroundCheck();

//        if (isCharging && isGrounded)
//        {
//            charge = Mathf.MoveTowards(charge, maxJumpForce, Time.deltaTime * holdForce);
//        }

//        WallCheckAndKnockback();
//    }

//    private void GroundCheck()
//    {
//        Debug.DrawRay(transform.position, Vector2.down * groundCheck, Color.red);

//        RaycastHit2D hit = Physics2D.BoxCast(
//            boxCollider.bounds.center,
//            boxCollider.bounds.size,
//            0f,
//            Vector2.down,
//            groundCheck,
//            groundLayer
//        );

//        isGrounded = hit && !forceUnGround;
//    }


//    private void WallCheckAndKnockback()
//    {
//        if (isGrounded) return;

//        Vector2 origin = transform.position;
//        Vector2 leftDirection = Vector2.left;
//        Vector2 rightDirection = Vector2.right;

//        RaycastHit2D hitLeft = Physics2D.Raycast(origin, leftDirection, wallCheckDistance, groundLayer);
//        RaycastHit2D hitRight = Physics2D.Raycast(origin, rightDirection, wallCheckDistance, groundLayer);

//        float horizontalVelocity = rigidBody.velocity.x;

//        if (hitLeft && horizontalVelocity < 0f)
//        {
//            Vector2 knockback = Vector2.right * Mathf.Abs(horizontalVelocity) * 1.2f;
//            rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);
//            rigidBody.AddForce(knockback / 2, ForceMode2D.Impulse);
//        }
//        else if (hitRight && horizontalVelocity > 0f)
//        {
//            Touch touch = Input.GetTouch(0);

//            if (touch.phase == TouchPhase.Began)
//            {
//                charge = 0f;
//            }
//            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
//            {
//                charge = Mathf.MoveTowards(charge, maxJumpForce, Time.deltaTime * holdForce);
//            }
//            else if (touch.phase == TouchPhase.Ended)
//            {
//                Vector2 knockback = Vector2.left * Mathf.Abs(horizontalVelocity) * 1.2f;
//                rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);
//                rigidBody.AddForce(knockback / 2, ForceMode2D.Impulse);

//            }

//            Debug.DrawRay(origin, leftDirection * wallCheckDistance, Color.red);
//            Debug.DrawRay(origin, rightDirection * wallCheckDistance, Color.blue);
//        }
//    }

//    private void OnJumpPerformed(InputAction.CallbackContext context)
//    {
//        if (!isGrounded) return;

//        isCharging = true;
//        charge = 0f;
//    }

//    private void OnJumpCanceled(InputAction.CallbackContext context)
//    {
//        if (!isGrounded || !isCharging) return;

//        Vector2 direction = touchPosition.x < Screen.width / 2f ? Vector2.left : Vector2.right;

//        Vector3 localScale = transform.localScale;
//        localScale.x = direction == Vector2.right ? -Mathf.Abs(localScale.x) : Mathf.Abs(localScale.x);
//        transform.localScale = localScale;

//        rigidBody.AddForce(Vector2.up * charge, ForceMode2D.Impulse);
//        rigidBody.AddForce(direction * (charge / 2f), ForceMode2D.Impulse);

//        Debug.Log($"Pulo liberado! For�a: {charge:F3} | Dire��o: {direction}");

//        StartCoroutine(ForceUnGroundTimer());
//        isCharging = false;
//    }

//    private void OnTouchPosition(InputAction.CallbackContext context)
//    {
//        touchPosition = context.ReadValue<Vector2>();
//    }

//    private IEnumerator ForceUnGroundTimer()
//    { 
//    yield return new WaitForSeconds(0.5f);
//    forceUnGround = false;
//    }
//}