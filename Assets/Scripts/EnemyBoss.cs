using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public int maxHealth;

    public Animator animator;
    public GameObject healthBar;
    public RectTransform healthProgress;

    private int health;
    private float healthBarWidth;
    private bool hasFightStarted;
    private bool isDead;
    private SoundManager sM;

    public void startFight()
    {
        healthBar.SetActive(true);
        GetComponent<PropertyFollowsPath>().enabled = true;
        hasFightStarted = true;
    }

    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
        healthBarWidth = healthBar.GetComponent<RectTransform>().rect.width - 24f;
        hasFightStarted = false;
        isDead = false;
        sM = SoundManager.instance;

        updateHealthBar();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!hasFightStarted || isDead) return;

        // TODO
    }

    // Boss touched something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile" && !isDead) // Got hit by projectile
        {
            health -= 1;
            updateHealthBar();

            if (health <= 0f)
            {
                isDead = true;
                animator.SetBool("isDead", true);
                int randomID = Random.Range(1, 3);
                sM.PlaySound("EnemyDie_" + randomID);

                GetComponent<PropertyFollowsPath>().enabled = false;
                GetComponent<PropertyHurtfull>().enabled = false;
            }
        }
    }

    private void updateHealthBar()
    {
        float progressWidth = (healthBarWidth / maxHealth) * health;
        healthProgress.sizeDelta = new Vector2(progressWidth, healthProgress.sizeDelta.y);
    }
}
