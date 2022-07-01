using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyHurtfull : MonoBehaviour
{
    public float damageCaused = 1f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // Touched player
        {
            GameManager.getInstance().givePlayerDamage(damageCaused);
        }
    }
}
