using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStarter : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement player;

    
    private bool canonSet = false;
    public float timeUntilCanonSet = 2f;
    public GameObject canonModel;

    // Start is called before the first frame update
    void Start()
    {
        canonModel.SetActive(false);

        player.animator.SetBool("isBorn", true);
        Invoke("SetFalseAgain", .1f);


    }
    private void Update()
    {

        timeUntilCanonSet -= Time.deltaTime;

        if (timeUntilCanonSet <= 0 && !canonSet)
        {
            canonModel.SetActive(true);
            canonSet = true;
            Debug.Log("setcanon");

        }
    }

    private void SetFalseAgain()
    {

        player.animator.SetBool("isBorn", false);


    }

}
