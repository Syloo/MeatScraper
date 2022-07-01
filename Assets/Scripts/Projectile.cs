using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float explosionForce = 1500f;
    public float explosionFullForceRadius = 1f; // Has to be greater than zero and lower than explosionRadius
    public float explosionRadius = 10f;
    public bool isPlayerVelocityIgnored = true;
    public bool isForceMoreDistributed = true;
    public float speed = 30f;

    public GameObject explosionEffect;

    private Rigidbody2D playerRB;


    // Start is called before the first frame update
    private void Start()
    {
        playerRB = GameManager.getInstance().getPlayerRB();
        GetComponent<Rigidbody2D>().velocity = playerRB.velocity + speed * (Vector2) transform.right;
    }

    // Projectile did hit something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3) return; // Ignore collisions with player

        Collider2D playerCollider = GameManager.getInstance().getPlayerCollider();
        Vector2 projectilePos = transform.position;

        Vector2 toPlayer = playerRB.position - projectilePos;
        float distance = (playerCollider.ClosestPoint(transform.position) - projectilePos).magnitude;
        distance = Mathf.Max(explosionFullForceRadius, distance);

        if (distance <= explosionRadius)
        {
            float force;
            if (isForceMoreDistributed)
            {
                // Makes resulting forces equal for the two modes within explosionFullForceRadius
                float normalizingFactor = 1f / Mathf.Sqrt(explosionRadius - explosionFullForceRadius);

                force = normalizingFactor * explosionForce * Mathf.Sqrt(explosionRadius - distance);
            }
            else
            {
                force = explosionForce / distance;
            }
            // Debug.Log("Force: " + force + ", distance: " + distance);

            if (isPlayerVelocityIgnored)
            {
                playerRB.velocity = new Vector2(0f, 0f);
            }
            else if (GameManager.getInstance().isPlayerJumping())
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, 0f); // Do not add jumping velocity and explosion force
            }
            playerRB.AddForce(force * toPlayer.normalized);
            GameManager.getInstance().setPlayerRagdolls();
        }

        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
