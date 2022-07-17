using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float cooldownAfterOverheat;
    public float cooldownBetweenShots;
    public float heatPerShot;

    public Animator animator;
    public Transform gun;
    public Transform projectileSpawn;
    public GameObject projectilePrefab;

    private Vector2 aimDirection;
    private float heat;
    private Camera mainCamera;
    private float nextShotReadyTime;

    public ParticleSystem reloadingSmoke;

    // Start is called before the first frame update
    private void Start()
    {
        reloadingSmoke.Stop();
        aimDirection = new Vector2(1f, 0f);
        heat = 0f;
        mainCamera = Camera.main;
        nextShotReadyTime = 0f;

        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        UILogic ui = GameManager.getInstance().MainUI;

        // Shooting
        heat = Mathf.Max(0f, heat - Time.deltaTime / cooldownAfterOverheat);
        if (Input.GetButtonDown("Fire1") && Time.time - nextShotReadyTime >= 0f)
        {
            Cursor.visible = false;
            heat += heatPerShot;
            Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);

            if (heat > 1f)
            {
                nextShotReadyTime = Time.time + cooldownAfterOverheat;
                animator.SetBool("hasOverheated", true);
                heat = 1f;
                reloadingSmoke.Play();
                ui.setOverheated(true);

                GameManager.getInstance().givePlayerDamage(1);
            }
            else
            {
                nextShotReadyTime = Time.time + cooldownBetweenShots;
                animator.SetBool("hasOverheated", false);
                ui.setOverheated(false);
            }
        }
        ui.setHeatTo(heat);

        // Gun placement
        Vector3 newAimDirection = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (newAimDirection.x != 0f || newAimDirection.y != 0f)
        {
            aimDirection = new Vector2(newAimDirection.x, newAimDirection.y).normalized;
            float angle = (180f / Mathf.PI) * Mathf.Atan2(aimDirection.y, aimDirection.x);
            gun.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
