using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILogic : MonoBehaviour
{
    [SerializeField]
    private RectTransform crosshair;
    [SerializeField]
    private Image heatImage;
    [SerializeField]
    private GameObject heatMeter;
    [SerializeField]
    private RectTransform heatProgress;
    [SerializeField]
    private Color heatProgressColor;
    [SerializeField]
    private Color overheatColor;
    [SerializeField]
    private GameObject menuUI;
    [SerializeField]
    private PlayerShooting playerShooting;

    public float moveSpeed = 5f;
    public GameObject eye;

    private bool menuActive = false;

    public void setHeatTo(float fraction)
    {
        float oldProgress = heatProgress.sizeDelta.x;
        heatProgress.sizeDelta = new Vector2(466f * fraction, heatProgress.sizeDelta.y);

        if (oldProgress > 0f && fraction == 0f)
        {
            heatMeter.SetActive(false);
        }
        else if (oldProgress == 0f && fraction > 0f)
        {
            heatMeter.SetActive(true);
        }
    }

    public void setOverheated(bool hasOverheated)
    {
        if (hasOverheated)
        {
            heatImage.color = overheatColor;
        }
        else
        {
            heatImage.color = heatProgressColor;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        setHeatTo(0f);
        heatMeter.SetActive(false);
        GameManager.getInstance().MainUI = this;
        menuUI.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        // Crosshair placement
        crosshair.position = Input.mousePosition;

        if(Input.GetKeyDown(KeyCode.Escape))
        {

            if(!menuActive)
            {
                AppearMenu();

            }
            else if(menuActive)
            {
                DisAppearMenu();
            }


        }

        
    }

    public void ReloadCurrentScene()
    {
        Cursor.visible = false;
        SceneManager.LoadScene("MainManu");
        menuUI.SetActive(false);
        menuActive = false;
        Time.timeScale = 1f;

    }

    public void Continue()
    {
        Cursor.visible = false;
        menuUI.SetActive(false);
        menuActive = false;
        playerShooting.shootPossible = true;
        Time.timeScale = 1f;
    }

    private void AppearMenu()
    {
        Cursor.visible = true;
        playerShooting.shootPossible = false;
        menuUI.SetActive(true);
        menuActive = true;
        Time.timeScale = 0f;
        
    }
    private void DisAppearMenu()
    {
        Cursor.visible = false;
        menuUI.SetActive(false);
        menuActive = false;
        Time.timeScale = 1f;
        playerShooting.shootPossible = true;

    }
}
