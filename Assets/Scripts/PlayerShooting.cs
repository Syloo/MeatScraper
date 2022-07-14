using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float cooldown;

    public Transform gun;
    public Transform projectileSpawn;
    public GameObject projectilePrefab;

    private Vector2 aimDirection;
    private float cooldownRemaining;
    private Camera mainCamera;

    // Start is called before the first frame update
    private void Start()
    {
        aimDirection = new Vector2(1f, 0f);
        cooldownRemaining = 0f;
        mainCamera = Camera.main;

        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // Shooting
        cooldownRemaining -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1"))
        {
            Cursor.visible = false;
            if (cooldownRemaining <= 0f)
            {
                cooldownRemaining = cooldown;
                Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
            }
        }

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
