using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyHurtfull : MonoBehaviour
{
    public int damageCaused = 1;

    private void OnCollisionStay2D(Collision2D collision)
    {
        handleTouch(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        handleTouch(collision.gameObject);
    }

    private void handleTouch(GameObject other)
    {
        if (other.tag == "Player") // Touched player
        {
            GameManager.getInstance().givePlayerDamage(damageCaused);
        }
    }
}
