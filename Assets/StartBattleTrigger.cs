using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleTrigger : MonoBehaviour
{

    [SerializeField]
    private AudioSource music;

    [SerializeField]
    private AudioClip bosstheme;

    [SerializeField]
    private GameObject self;

    [SerializeField]
    private GameObject[] gates;

    private float setTimer = .6f;
    private float time;
    private int i;
    private bool makeGate = false;

    private void Start()
    {
        time = setTimer;
        self = GetComponent<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {

            music.clip = bosstheme;
            music.Play();
            makeGate = true;

        }
    }

    private void Update()
    {

        if(makeGate)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {

                time = setTimer;
                if (i < gates.Length)
                {

                    gates[i].SetActive(true);
                    i++;

                }
                else
                {

                    Invoke("DestroyMe", .2f);


                }

            }
        }

        


    }

    private void DestroyMe()
    {

        Destroy(gameObject);


    }


}
