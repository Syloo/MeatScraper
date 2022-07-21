using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stages
{
    [Range(0f, 1f)]
    public float fractionHealthTrigger;
    public Spawn[] spawns;
}

public class EnemyBoss : MonoBehaviour
{
    public int maxHealth;
    public float platformDisappearDuration;
    public Stages[] stages;

    public Animator animator;
    public GameObject[] platforms;
    public GameObject healthBar;
    public RectTransform healthProgress;
    public GameObject victoryScreen;

    private int currentStage;
    private int health;
    private float healthBarWidth;
    private bool hasFightStarted;
    private bool isDead;
    private SoundManager sM;
    private PropertySpawner spawner;

    public AudioSource music;

    public void startFight()
    {
        healthBar.SetActive(true);
        GetComponent<PropertyFollowsPath>().enabled = true;
        spawner.enabled = true;
        hasFightStarted = true;
        Invoke("resumeFight", platformDisappearDuration);
    }

    // Start is called before the first frame update
    private void Start()
    {
        currentStage = -1;
        health = maxHealth;
        healthBarWidth = healthBar.GetComponent<RectTransform>().rect.width - 24f;
        hasFightStarted = false;
        isDead = false;
        sM = SoundManager.instance;
        spawner = GetComponent<PropertySpawner>();

        updateHealthBar();
        updateStageBasedOnHealth();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!hasFightStarted || isDead) return;

        // TODO Remove update() and hasFightStarted if not needed
    }

    // Boss touched something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile" && !isDead) // Got hit by projectile
        {
            health -= 1;
            updateHealthBar();
            updateStageBasedOnHealth();

            if (health <= 0f)
            {
                music.Stop();
                isDead = true;
                animator.SetBool("isDead", true);
                int randomID = Random.Range(1, 3);
                sM.PlaySound("EnemyDie_" + randomID);
                victoryScreen.SetActive(true);

                GetComponent<PropertyFollowsPath>().enabled = false;
                GetComponent<PropertyHurtfull>().enabled = false;
                spawner.enabled = false;
                spawner.destroyAllSpawnedObjects();
            }
        }
    }

    private void resumeFight()
    {
        foreach (GameObject p in platforms)
        {
            p.SetActive(true);
        }
        GetComponent<Collider2D>().enabled = true;
    }

    private void updateHealthBar()
    {
        float progressWidth = Mathf.Max(0f, healthBarWidth * ((float)health / maxHealth));
        healthProgress.sizeDelta = new Vector2(progressWidth, healthProgress.sizeDelta.y);
    }

    private void updateStageBasedOnHealth()
    {
        float healthFraction = (float)health / maxHealth;
        float minStageHealth = 2f;
        int newStage = 0;

        // Select triggered stage with smallest health trigger
        for (int i = 0; i < stages.Length; i++)
        {
            if (stages[i].fractionHealthTrigger >= healthFraction && stages[i].fractionHealthTrigger < minStageHealth)
            {
                minStageHealth = stages[i].fractionHealthTrigger;
                newStage = i;
            }
        }

        if (newStage != currentStage)
        {
            if (currentStage != -1)
            {
                foreach (GameObject p in platforms)
                {
                    p.SetActive(false);
                }
                Invoke("resumeFight", platformDisappearDuration);
            }

            currentStage = newStage;
            GetComponent<Collider2D>().enabled = false;
            spawner.spawns = stages[currentStage].spawns;
        }
    }
}
