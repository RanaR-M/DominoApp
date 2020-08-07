using UnityEngine;
using UnityEngine.SceneManagement;

public class menuControl : MonoBehaviour {
    public CanvasGroup quitMenu;
    public CanvasGroup mainPage;
    //navigation navigationObject;

    void Awake() {
        normalDisplay();
        //navigationObject = quitMenu.GetComponent<navigation>();
    }

    public void normalDisplay() {
        mainPage.alpha = 1;
        mainPage.interactable = true;
        mainPage.blocksRaycasts = true;

        quitMenu.alpha = 0;
        quitMenu.interactable = false;
        quitMenu.blocksRaycasts = false;
    }

    public void QuitMenuDisplay() {
        quitMenu.alpha = 1;
        quitMenu.interactable = true;
        quitMenu.blocksRaycasts = true;

        mainPage.alpha = .5f;
        mainPage.interactable = false;
        mainPage.blocksRaycasts = false;
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name.Equals("GamePage")) {
            QuitMenuDisplay();
        }
    }

    


}
