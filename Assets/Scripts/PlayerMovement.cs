using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movementSpeed;
    public float movementSpeedY;
    
    private float velocityX;
    
    // Start is called before the first frame update
    void Start()
    {
    }
    
    // Update is called once per physics tick
    void FixedUpdate()
    {
        float velocityY = rb.velocity.y;
        if (Input.GetKeyDown(KeyCode.Space)) {
            velocityY += movementSpeedY;
        }
        rb.velocity = new Vector2(velocityX, velocityY);
    }
    
    // Update is called once per frame
    void Update()
    {
        velocityX = movementSpeed * Input.GetAxis("Horizontal");
        Debug.Log(Input.mousePosition);
    }
}
