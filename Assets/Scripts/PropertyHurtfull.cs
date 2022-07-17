using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyHurtfull : MonoBehaviour
{
    public int damageCaused = 1;

    private Collider2D col;
    private float lastTouch;
    private Vector2 lastVelocity;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        col = GetComponent<Collider2D>();
        lastTouch = 0f;
        lastVelocity = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per physics tick
    private void FixedUpdate()
    {
        if (rb) lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ContactPoint2D contact = collision.GetContact(0);
            handleTouch(contact.normal);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Vector2 playerPosition = GameManager.getInstance().getPlayerRB().transform.position;
            Vector2 normal = (playerPosition - col.ClosestPoint(playerPosition)).normalized;
            handleTouch(normal);
        }
    }

    private void handleTouch(Vector2 contactNormal)
    {
        if (Time.time > lastTouch + 0.1f)
        {
            lastTouch = Time.time;
            GameManager gm = GameManager.getInstance();
            bool isPlayerDead = gm.givePlayerDamage(damageCaused);
            if (isPlayerDead) return;

            Vector2 relativeVelocity = GameManager.getInstance().getPlayerLastVelocity() - lastVelocity;
            Vector2 newVelocity = Vector2.Reflect(relativeVelocity, contactNormal);

            float speed = newVelocity.magnitude;
            if (speed < 6f) newVelocity *= 6f / speed;

            // Debug.Log("iVelocity: " + relativeVelocity);
            // Debug.Log("Normal: " + contactNormal);
            // Debug.Log("oVelocity: " + newVelocity);

            gm.setPlayerRagdolls();
            gm.getPlayerRB().velocity = newVelocity;
        }
    }
}
