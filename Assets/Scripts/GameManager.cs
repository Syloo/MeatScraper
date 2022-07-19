using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    public int playerHealth = 3;
    public float playerInvincibilityDuration = 1.5f;

    private static GameManager instance = null;

    private float invincibilityEndTime;
    private PlayerMovement player;
    private Collider2D playerCollider;
    private int playerMaxHealth;
    private Rigidbody2D playerRB;

    private SoundManager soundManager = SoundManager.instance;

    private GameObject[] hearts;

  

    private GameManager()
    {

        invincibilityEndTime = 0f;
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

    public Vector2 getPlayerLastVelocity()
    {
        return player.getLastVelocity();
    }

    public Rigidbody2D getPlayerRB()
    {
        return playerRB;
    }

    public bool isPlayerInvincible()
    {
        return Time.time < invincibilityEndTime;
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

    public void FillUpLife(int life)
    {

        if(playerHealth + life <= 3)
        {

            playerHealth += life;
            for(int i = 0; i < playerHealth; i++)
            {

                hearts[i].SetActive(true);

            }


        }

    }

    // Gives player damage, returns if player died
    public bool givePlayerDamage(int amount)
    {
        if (isPlayerInvincible()) return !player.isAlive;

        invincibilityEndTime = Time.time + playerInvincibilityDuration;
        playerHealth -= amount;
        if (hearts.Length > playerHealth) hearts[playerHealth].SetActive(false);
        Debug.Log("Got 1 damage");
        int randomDamageID = Random.Range(1,5);
        soundManager.PlaySound("Damage_" + randomDamageID.ToString());

        if (playerHealth <= 0f)
        {
            Debug.Log("Player is DEAD!");
            invincibilityEndTime += player.dyingTime;
            playerHealth = playerMaxHealth;
            player.animator.SetBool("isDying", true);
            player.isAlive = false;
            //SceneManager.LoadScene("MainManu");
            return true;
        }

        return false;
    }
}
