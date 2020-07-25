using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class navigation : MonoBehaviour
{
    public static Stack<string> scenes = new Stack<string>();

    void Start()
    {
        if (scenes.Count == 0)
        {
            scenes.Push(SceneManager.GetActiveScene().name);
        }
    }

    void Update()
    {
        previousScene();
    }

    public void previousScene()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (scenes.Count == 1)
            {
                Application.Quit();
            }
            else
            {
                scenes.Pop();
                string sceneToBuild = scenes.Pop();
                loadScene (sceneToBuild);
            }
        }
    }

    public void loadScene(string newScene)
    {
        scenes.Push (newScene);
        SceneManager.LoadScene (newScene);
    }
}
