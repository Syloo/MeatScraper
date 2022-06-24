using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager instance = null;

    private PlayerMovement player;
    private Rigidbody2D playerRB;

    private GameManager()
    {
        // Nothing
    }

    public static GameManager getInstance()
    {
        if (instance == null)
        {
            instance = new GameManager();
        }
        return instance;
    }

    public Rigidbody2D getPlayerRB()
    {
        return playerRB;
    }

    public void setPlayer(PlayerMovement player)
    {
        this.player = player;
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    public void setPlayerRagdolls()
    {
        player.isRagdolling = 1f;
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}