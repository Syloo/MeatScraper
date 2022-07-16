using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float walkDistance = 2f;
    public Transform enemyModel;

    private bool isWalkingForwards;
    private Vector3 lastPosition;
    private Rigidbody2D rb;
    private Vector2 startingPoint;

    // Start is called before the first frame update
    private void Start()
    {
        isWalkingForwards = true;
        lastPosition = Vector3.zero;
        rb = GetComponent<Rigidbody2D>();
        startingPoint = transform.position;
    }

    private void changeDirection()
    {
        isWalkingForwards = !isWalkingForwards;

        Vector3 flippedScale = enemyModel.localScale;
        flippedScale.x = -flippedScale.x;
        enemyModel.localScale = flippedScale;
    }

    // Update is called once per physics tick
    private void FixedUpdate()
    {
        // Change direction when stuck
        float distanceTraveled = (transform.position - lastPosition).sqrMagnitude;
        if (distanceTraveled < 0.0001f) changeDirection();
        lastPosition = transform.position;

        // Move
        float velocityX;
        if (isWalkingForwards)
        {
            if (transform.position.x > startingPoint.x + walkDistance) changeDirection();
        }
        else
        {
            if (transform.position.x < startingPoint.x - walkDistance) changeDirection();
        }
        velocityX = (isWalkingForwards ? movementSpeed : -movementSpeed);

        rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }

    // Enemy touched something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile") // Got hit by projectile
        {
            Destroy(gameObject);
        }
    }
}
