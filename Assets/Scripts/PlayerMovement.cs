using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float breakAcceleration;
    public float jumpVelocity;
    public float movementSpeed;
    public float ragdollDuration = 0.2f;

    public Collider2D groundCollider;
    public Transform mainCamera;
    public Transform playerModel;

    public float isJumping;
    public float isRagdolling;
    
    private bool isJumpPressed;
    private Rigidbody2D rb;
    private float velocityX;

    public Animator animator;
    public SpriteRenderer characterSprite;
    
    // Start is called before the first frame update
    private void Start()
    {
        isJumpPressed = false;
        isJumping = 0f;
        isRagdolling = 0f;
        rb = GetComponent<Rigidbody2D>();
        velocityX = 0f;

        GameManager.getInstance().setPlayer(this);
    }

    // Update is called once per physics tick
    private void FixedUpdate()
    {
        bool isGrounded = groundCollider.IsTouchingLayers(Physics2D.AllLayers);

        // Character orientation
        if (velocityX != 0f)
        {

            animator.SetBool("isWalking", true);
            Vector3 flippedScale = playerModel.localScale;
            flippedScale.x = Mathf.Sign(-velocityX) * Mathf.Abs(flippedScale.x);
            playerModel.localScale = flippedScale;
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        if (velocityX < 0)
        {
            characterSprite.flipX = false;
        }
        else if(velocityX > 0)
        {
            characterSprite.flipX = true;
        }


        // Horizontal movement
        if (isRagdolling >= 1f)
        {
            isRagdolling += Time.fixedDeltaTime;
            if (isRagdolling >= 1f + ragdollDuration && isGrounded)
            {
                isRagdolling = 0f;
            }
        }
        if (isRagdolling >= 1f) // Influenced by explosion
        {
            if (Mathf.Abs(rb.velocity.x) > movementSpeed)
            {
                if (velocityX * rb.velocity.x < 0f) // Steering against direction of movement
                {
                    velocityX = Mathf.Sign(rb.velocity.x) * (Mathf.Abs(rb.velocity.x) - Time.fixedDeltaTime * breakAcceleration);
                }
                else // Let it ragdoll
                {
                    velocityX = rb.velocity.x;
                }
            }
            else if (velocityX == 0f || isRagdolling < 1f + ragdollDuration) // Let it ragdoll if no explicit input
            {
                velocityX = rb.velocity.x;
            }
        }

        // Vertical movement
        float velocityY = rb.velocity.y;
        if (isJumping > 0f)
        {
            animator.SetBool("isJumping", true);
            isJumping -= Time.fixedDeltaTime;
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
        if (isJumpPressed)
        {
            isJumpPressed = false;
            if (isGrounded)
            {
                isJumping = 0.5f;
                velocityY = jumpVelocity;
            }
        }

        rb.velocity = new Vector2(velocityX, velocityY);
        mainCamera.position = new Vector3(transform.position.x, transform.position.y, mainCamera.position.z);
    }

    // Update is called once per frame
    private void Update()
    {
        velocityX = movementSpeed * Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            isJumpPressed = true;
        }
    }
}
