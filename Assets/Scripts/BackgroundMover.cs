using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{

    [SerializeField]
    private Vector3 startScale = new Vector3(3,3,3), endScale, curScale;
    [SerializeField]
    private Transform backgroundSprite;
    private float fraction = 0;
    private bool changeDir;
    public float moveSpeed = 1f;


    // Start is called before the first frame update
    void Start()
    {
        changeDir = false;
        fraction = 0;
        backgroundSprite.transform.localScale = startScale;
    }

    // Update is called once per frame
    void Update()
    {

        if (fraction < 1 && !changeDir)
        {
            fraction += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startScale, endScale, fraction);

        }
        else if (fraction < 1 && changeDir)
        {

            fraction += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(endScale, startScale, fraction);

        }
        else if (fraction >= 1)
        {

            fraction = 0;
            if (!changeDir)
            { changeDir = true; }
            else if (changeDir)
            { changeDir = false; }

        }

    }
}
