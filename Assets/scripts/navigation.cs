using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class navigation : MonoBehaviour {
    public static Stack<string> scenes = new Stack<string>();

    void Start() {
        if (scenes.Count == 0) {
            scenes.Push(SceneManager.GetActiveScene().name);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !scenes.Peek().Equals("GamePage")) {
            previousScene();
        }

    }

    public void previousScene() {

        if (scenes.Count == 1) {
            Application.Quit();
        } else {
            scenes.Pop();
            string sceneToBuild = scenes.Pop();
            loadScene(sceneToBuild);
        }

    }

    public void loadScene(string newScene) {
        scenes.Push(newScene);
        SceneManager.LoadScene(newScene);
    }
}
