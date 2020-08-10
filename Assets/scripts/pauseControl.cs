using UnityEngine;

public class pauseControl : MonoBehaviour
{
    /* to use this code you need two objects: the control object that you attach the script too and the menu you want to control 
     * the menu should be an empty object that has the background only and it's children are the components of the menu eg: buttons
     * you should have at least two buttons to use these functions
     * the PauseGame function activates the menu you chose, while the ResumeGame function deactivates it.
     * for example you should have a pause button in the regual screen that it's onclick activates the PauseGame function
     * and a exit button that it's onclick function uses the ResumeGame in the menu that would go back to the normal screen.
     */
    public GameObject pauseMenu;

    public void PauseGame() {
        pauseMenu.SetActive(true);
    }

    public void ResumeGameWithArgument(GameObject pauseMenu) {
        this.pauseMenu = pauseMenu;
        ResumeGame();
    }

    public void ResumeGame() {
        pauseMenu.SetActive(false);
    }
}
