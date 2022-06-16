using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Collider2D groundCollider;
    public Transform mainCamera;
    public Transform playerModel;
    public Rigidbody2D rb;
    public float movementSpeed;
    public float movementSpeedY;
    
    private bool isJumping;
    private float velocityX;
    
    // Start is called before the first frame update
    private void Start()
    {
        isJumping = false;
        velocityX = 0f;
    }

    // Update is called once per physics tick
    private void FixedUpdate()
    {
        // Character orientation
        if (velocityX != 0f)
        {
            Vector3 flippedScale = playerModel.localScale;
            flippedScale.x = Mathf.Sign(-velocityX) * Mathf.Abs(flippedScale.x);
            playerModel.localScale = flippedScale;
        }

        // Horizontal movement
        if (Mathf.Abs(rb.velocity.x) > movementSpeed) // influenced by explosion
        {
            if (velocityX * rb.velocity.x < 0f) // steering against direction of movement
            {
                velocityX = 0.9f * rb.velocity.x;
            }
            else
            {
                velocityX = rb.velocity.x;
            }
        }

        // Vertical movement
        float velocityY = rb.velocity.y;
        if (isJumping)
        {
            isJumping = false;
            if (groundCollider.IsTouchingLayers(Physics2D.AllLayers))
            {
                velocityY = movementSpeedY;
            }
        }

        rb.velocity = new Vector2(velocityX, velocityY);
        mainCamera.position = new Vector3(transform.position.x, transform.position.y, mainCamera.position.z);
    }

    // Update is called once per frame
    private void Update()
    {
        velocityX = movementSpeed * Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }
    }
}
