using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    public int playerHealth = 3;
    public float playerInvincibilityDuration = 1.5f;

    private static GameManager instance = null;

    private float playerLastHitTime;
    private PlayerMovement player;
    private Collider2D playerCollider;
    private int playerMaxHealth;
    private Rigidbody2D playerRB;

    private GameObject[] hearts;


    private GameManager()
    {
        playerLastHitTime = 0f;
        playerMaxHealth = playerHealth;
    }

    public static GameManager getInstance()
    {
        if (instance == null)
        {
            instance = new GameManager();
        }
        return instance;
    }

    public UILogic MainUI { get; set; }

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
        hearts = GameObject.FindGameObjectsWithTag("Heart");
        //Debug.Log(hearts.Length);
    }

    public void setPlayerRagdolls()
    {
        player.isRagdolling = 1f;
    }

    public void givePlayerDamage(int amount)
    {
        if (Time.time - playerLastHitTime < playerInvincibilityDuration) return;

        playerLastHitTime = Time.time;
        playerHealth -= amount;
        hearts[playerHealth].SetActive(false);

        if (playerHealth <= 0f)
        {
            Debug.Log("Player is DEAD!");
            playerHealth = playerMaxHealth;
            player.animator.SetBool("isDying", true);
            player.isAlive = false;
            //SceneManager.LoadScene("MainManu");
        }
    }
}
