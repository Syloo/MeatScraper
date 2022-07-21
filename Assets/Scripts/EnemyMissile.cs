using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public float speed = 1f;
    public float turningSpeed = 40f;

    public GameObject explosionEffect;

    private bool hasTriggered;
    private Transform playerTransform;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        hasTriggered = false;
        playerTransform = GameManager.getInstance().getPlayerRB().transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per physics tick
    private void FixedUpdate()
    {
        Vector3 toPlayer = (playerTransform.position - transform.position).normalized;
        float turnSignal = Vector3.Dot(toPlayer, transform.up); // Negative is clockwise turn
        
        if (Mathf.Abs(turnSignal) > Time.fixedDeltaTime * turningSpeed)
        {
            turnSignal *= (Time.fixedDeltaTime * turningSpeed) / Mathf.Abs(turnSignal);
        }

        transform.Rotate(new Vector3(0f, 0f, turnSignal));
        rb.velocity = speed * transform.right;
    }

    // Projectile did hit something
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boss" || (other.isTrigger && other.tag != "Projectile") || hasTriggered) return; // Ignore collisions with boss and triggers
        hasTriggered = true;

        if (other.tag == "Player") GameManager.getInstance().givePlayerDamage(1);

        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
