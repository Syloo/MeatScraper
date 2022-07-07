using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public float playerHealth = 3f;
    public float playerInvincibilityDuration = 2f;

    private static GameManager instance = null;

    private float playerLastHitTime;
    private PlayerMovement player;
    private Collider2D playerCollider;
    private Rigidbody2D playerRB;

    private GameManager()
    {
        playerLastHitTime = 0f;
    }

    public static GameManager getInstance()
    {
        if (instance == null)
        {
            instance = new GameManager();
        }
        return instance;
    }

    public Collider2D getPlayerCollider()
    {
        return playerCollider;
    }

    public Rigidbody2D getPlayerRB()
    {
        return playerRB;
    }

    public bool isPlayerJumping()
    {
        return player.isJumping > 0f;
    }

    public void setPlayer(PlayerMovement player)
    {
        this.player = player;
        playerCollider = player.GetComponent<Collider2D>();
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    public void setPlayerRagdolls()
    {
        player.isRagdolling = 1f;
    }

    public void givePlayerDamage(float amount)
    {
        if (Time.time - playerLastHitTime < playerInvincibilityDuration) return;

        playerLastHitTime = Time.time;
        playerHealth -= amount;

        Debug.Log("DMG");

        if (playerHealth <= 0f)
        {
            Debug.Log("Player is DEAD!");
        }
    }
}
