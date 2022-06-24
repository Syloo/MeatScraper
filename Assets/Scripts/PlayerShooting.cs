using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float cooldown;

    public RectTransform crosshair;
    public Transform gun;
    public Camera mainCamera;
    public Transform projectileSpawn;
    public GameObject projectilePrefab;

    private Vector2 aimDirection;
    private float cooldownRemaining;

    // Start is called before the first frame update
    private void Start()
    {
        aimDirection = new Vector2(1f, 0f);
        cooldownRemaining = 0f;

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

        // Crosshair placement
        float mouseXFromCenter = Input.mousePosition.x - Screen.width / 2f;
        float mouseYFromCenter = Input.mousePosition.y - Screen.height / 2f;
        crosshair.localPosition = new Vector3(Mathf.Round(mouseXFromCenter), Mathf.Round(mouseYFromCenter), 0f);

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
