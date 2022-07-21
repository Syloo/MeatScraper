using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    // Update is called once per frame
    private void Update()
    {
        // Crosshair placement
        crosshair.position = Input.mousePosition;
    }
}
