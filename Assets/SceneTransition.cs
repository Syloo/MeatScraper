using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;

    public bool isQuitting = false, isStarting = false, startTransition = false;
    public float timeUntilQuit = 2f;
    public RawImage transitionBlackScreen;
    public float transitionSpeed;
    

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

            if(isStarting)
            {
                // SceneManager.LoadScene(sceneToLoad);
                startTransition = true;
            }
            else if(isQuitting)
            {
                Application.Quit();
            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        transitionBlackScreen.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(startTransition)
        {



        }
    }
}
