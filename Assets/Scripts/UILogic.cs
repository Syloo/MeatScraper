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
    private RectTransform heatProgress;
    [SerializeField]
    private Color heatProgressColor;
    [SerializeField]
    private Color overheatColor;

    public void setHeatTo(float fraction)
    {
        heatProgress.sizeDelta = new Vector2(500f * fraction, heatProgress.sizeDelta.y);
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
        GameManager.getInstance().MainUI = this;
    }

    // Update is called once per frame
    private void Update()
    {
        // Crosshair placement
        crosshair.position = Input.mousePosition;
    }
}
