using UnityEngine;
using UnityEngine.SceneManagement;

public class menuControl : MonoBehaviour {

    /* to use this code you need two Canvases one is the normal screen the other is menu you want to display
     * add to these canvases the CanvasGroup component then make an object and attach this script to it 
     * in the script put the canvases as the names suggest
     * you need two buttons to use this script one to access the menu and the other to close it
     * to the button that access the menu attach the QuitMenuDisplay method and to the button the closes it use the normalDisplay
     * the update method is used to access the quitMenu with the press of the return button in android and the ESC button on pc
     * 
     */
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
