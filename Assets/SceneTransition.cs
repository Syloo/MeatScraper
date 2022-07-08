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
    private Color color;


   

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {

            Debug.Log("Transition");
                startTransition = true;
           

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        transitionBlackScreen.color = new Color(25, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(startTransition)
        {

            color += new Color(0, 0, 0, transitionSpeed * Time.deltaTime);
            transitionBlackScreen.color = color;
            timeUntilQuit -= Time.deltaTime;
            if(timeUntilQuit <= 0)
            {

                if (isStarting)
                {
                    SceneManager.LoadScene(sceneToLoad);
                }
                else if (isQuitting)
                {
                    Application.Quit();
                }

            }


        }
    }
}
