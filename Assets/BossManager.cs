using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{

    public Vector3 startPos, endPos;
    public bool startFight = false;
    private float fraction = 0;
    public float speed = .5f;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(startFight)
        {

            if (fraction < 1)
            {
                fraction += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, fraction);
            }

        }

    }



}
