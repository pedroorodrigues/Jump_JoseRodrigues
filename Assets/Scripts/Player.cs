using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    #region Vari�veis

    [Header("Configura��o de Pulo")]
    [SerializeField] private float maxJumpForce = 10f;
    [SerializeField] private float holdForce = 5f;
    [SerializeField] private float debugDistance;
    [SerializeField] private PhysicsMaterial2D physicsMaterial;

    [Header("Verifica��o de Ch�o")]
    [SerializeField] private float groundCheck = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask Wall;

    [Header("Sprites")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite jumpSprite;
    [SerializeField] private Sprite vineSprite;

    private JumpBar jumpBar;

    private float charge;
    private bool isGrounded;
    private bool forceUnGround;
    private bool forceJump;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private Animator animator;

    private float velocity;

    private bool isOnVine = false;
    private bool onLeftWall = true;

    #endregion

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        jumpBar = GetComponent<JumpBar>();
    }
    private void Start()
    {
        physicsMaterial.bounciness = 0;
    }

    private void Update()
    {
        GroundCheck();
        Jump();
        UpdateSprite();
    }

    private void GroundCheck()
    {
        Vector2 vector = new Vector2(transform.position.x + debugDistance, transform.position.y);
        Debug.DrawRay(vector, Vector2.down * groundCheck, Color.red);

        Vector2 vector2 = new Vector2(transform.position.x - debugDistance, transform.position.y);
        Debug.DrawRay(vector2, Vector2.down * groundCheck, Color.red);

        Vector2 vector3 = new Vector2(transform.position.x, transform.position.y);
        Debug.DrawRay(vector3, Vector2.down * groundCheck, Color.red);

        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        RaycastHit2D hit2 = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        RaycastHit2D hit3 = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);

        isGrounded = hit || hit2 || hit3;

        if (isGrounded)
        {
            physicsMaterial.bounciness = 0;
        }
        else
        {
            physicsMaterial.bounciness = 1;
        }
    }

    private void Jump()
    {
        if ((Input.touchCount > 0 && isGrounded) || (Input.touchCount > 0 && forceJump))
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                charge = Mathf.LerpAngle(charge, maxJumpForce, Time.deltaTime * holdForce);
                jumpBar?.Jump_Bar(charge / maxJumpForce);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                SetStivky(false);
                rigidBody.gravityScale = 1;

                float screenWidth = Screen.width;
                float touchPositionX = touch.position.x;

                Vector2 direction = touchPositionX < screenWidth / 2 ? Vector2.left : Vector2.right;

                Vector3 localScale = transform.localScale;
                localScale.x = direction == Vector2.right ? -Mathf.Abs(localScale.x) : Mathf.Abs(localScale.x);
                transform.localScale = localScale;

                rigidBody.AddForce(Vector2.up * charge, ForceMode2D.Impulse);
                rigidBody.AddForce(direction * (charge / 2), ForceMode2D.Impulse);

                charge = 0;
                jumpBar?.Jump_Bar(0f);
            }
        }
    }

    private void UpdateSprite()
    {
        if (isOnVine)
        {
            spriteRenderer.sprite = vineSprite;
            Vector3 scale = transform.localScale;
            scale.x = onLeftWall ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else if (!isGrounded)
        {
            spriteRenderer.sprite = jumpSprite;
        }
        else
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    public void SwitchVelocity()
    {
        Vector2 newVelocity = new Vector2(-rigidBody.velocity.x, rigidBody.velocity.y);
        rigidBody.velocity = newVelocity / 3;
        print("swich");
    }

    public void Stiky()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        rigidBody.gravityScale = 0.5f;
        rigidBody.constraints = RigidbodyConstraints2D.None;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        physicsMaterial.bounciness = 0;
    }

    public void SetStivky(bool stivky)
    {
        forceJump = stivky;
        if (!stivky)
        {
            rigidBody.gravityScale = 1;
            physicsMaterial.bounciness = 0;
        }
    }

    public void OnWind()
    {
        rigidBody.AddForce(Vector2.left * 0.05f, ForceMode2D.Impulse);
    }

    public void OnWindRight()
    {
        rigidBody.AddForce(Vector2.right * 0.05f, ForceMode2D.Impulse);
    }
}
