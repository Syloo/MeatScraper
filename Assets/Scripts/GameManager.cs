using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    private static GameManager instance = null;

    private const float PLAYER_INVINCIBILITY_DURATION = 2f;
    private const int PLAYER_MAX_HEALTH = 3;

    private PlayerMovement player;
    private Collider2D playerCollider;
    private int playerHealth;
    private Rigidbody2D playerRB;

    private GameObject[] hearts;
    private float invincibilityEndTime;
    private SoundManager soundManager;

    private GameManager()
    {
        invincibilityEndTime = 0f;
        playerHealth = PLAYER_MAX_HEALTH;
        soundManager = SoundManager.instance;
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

        if (playerHealth + life <= PLAYER_MAX_HEALTH)
        {

            playerHealth += life;
            for (int i = 0; i < playerHealth; i++)
            {

                hearts[i].SetActive(true);

            }


        }

    }

    // Gives player damage, returns if player died
    public bool givePlayerDamage(int amount)
    {
        if (isPlayerInvincible()) return !player.isAlive;

        invincibilityEndTime = Time.time + PLAYER_INVINCIBILITY_DURATION;
        playerHealth -= amount;
        if (playerHealth >= 0 && playerHealth < hearts.Length) hearts[playerHealth].SetActive(false);
        int randomDamageID = Random.Range(1,5);
        soundManager.PlaySound("Damage_" + randomDamageID.ToString());

        if (playerHealth > 0f)
        {
            player.playInvincibleAnimationFor(PLAYER_INVINCIBILITY_DURATION);
            return false;
        }
        else
        {
            Debug.Log("Player is DEAD!");
            invincibilityEndTime += player.dyingTime;
            playerHealth = PLAYER_MAX_HEALTH;
            player.animator.SetBool("isDying", true);
            player.isAlive = false;
            //SceneManager.LoadScene("MainManu");
            return true;
        }
    }
}
