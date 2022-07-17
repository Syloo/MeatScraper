using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartFillUp : MonoBehaviour
{

    public int fillUpLifes = 1;
    public GameObject heart;

    // Start is called before the first frame update
    void Start()
    {
        heart.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {

            GameManager gm = GameManager.getInstance();
            gm.FillUpLife(fillUpLifes);
            heart.SetActive(false);

        }
    }
}
