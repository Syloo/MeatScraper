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
    private SoundManager sM;
    private Animator animator;
    private bool isDead;

    // Start is called before the first frame update
    private void Start()
    {
        isDead = false;
        animator = GetComponentInChildren<Animator>();
        sM = SoundManager.instance;
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
        if(!isDead)
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
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
        
    }

    // Enemy touched something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile") // Got hit by projectile
        {
            isDead = true;
            animator.SetBool("enemyDead", true);
            int randomID = Random.Range(1, 3);
            sM.PlaySound("EnemyDie_" + randomID);

            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
}
