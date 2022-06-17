using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float breakAcceleration; // >= 0 and < movementSpeed
    public Collider2D groundCollider;
    public float isRagdolling;
    public Transform mainCamera;
    public Transform playerModel;
    public float movementDrag;
    public float movementSpeed;
    public float jumpVelocity;
    
    private bool isJumping;
    private Rigidbody2D rb;
    private float velocityX;
    
    // Start is called before the first frame update
    private void Start()
    {
        isJumping = false;
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
            Vector3 flippedScale = playerModel.localScale;
            flippedScale.x = Mathf.Sign(-velocityX) * Mathf.Abs(flippedScale.x);
            playerModel.localScale = flippedScale;
        }


        // Horizontal movement
        if (isRagdolling >= 1f)
        {
            isRagdolling += Time.fixedDeltaTime;
            if (isRagdolling >= 1.2f && isGrounded)
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
            else if (velocityX == 0f || isRagdolling < 1.2f) // Let it ragdoll if no explicit input
            {
                velocityX = rb.velocity.x;
            }
        }

        // Vertical movement
        float velocityY = rb.velocity.y;
        if (isJumping)
        {
            isJumping = false;
            if (isGrounded)
            {
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
            isJumping = true;
        }
    }
}
