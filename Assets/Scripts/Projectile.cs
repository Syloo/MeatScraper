using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject explosionEffect;
    public float explosionForce = 10f;
    public float speed = 20f;

    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = speed * transform.right;
    }

    // Projectile did hit something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 3) // Ignore collisions with player
        {
            Rigidbody2D playerRB = GameManager.getInstance().getPlayerRB();
            Vector2 toPlayer = playerRB.position - new Vector2(transform.position.x, transform.position.y);
            if (toPlayer.magnitude < 10f)
            {
                float force = explosionForce / toPlayer.magnitude; // Magnitude is greater zero because player collider size is greater zero
                playerRB.AddForce(force * toPlayer.normalized);
                GameManager.getInstance().setPlayerRagdolls();
            }

            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
