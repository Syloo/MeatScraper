using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float walkDistance = 2f;
    public Transform enemyModel;

    private bool isWalkingForwards;
    private Rigidbody2D rb;
    private Vector2 startingPoint;

    // Start is called before the first frame update
    private void Start()
    {
        isWalkingForwards = true;
        rb = GetComponent<Rigidbody2D>();
        startingPoint = transform.position;
    }

    // Update is called once per physics tick
    private void FixedUpdate()
    {
        float velocityX;
        if (isWalkingForwards)
        {
            velocityX = movementSpeed;
            if (transform.position.x > startingPoint.x + walkDistance)
            {
                isWalkingForwards = false;

                Vector3 flippedScale = enemyModel.localScale;
                flippedScale.x = -flippedScale.x;
                enemyModel.localScale = flippedScale;
            }
        }
        else
        {
            velocityX = -movementSpeed;
            if (transform.position.x < startingPoint.x - walkDistance)
            {
                isWalkingForwards = true;

                Vector3 flippedScale = enemyModel.localScale;
                flippedScale.x = -flippedScale.x;
                enemyModel.localScale = flippedScale;
            }
        }

        rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }

    // Enemy touched something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) // Got hit by projectile
        {
            Debug.Log("Hit");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
