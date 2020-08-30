using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DirectionControl : MonoBehaviour
{

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
    public Button buttonRight;
    public Button buttonLeft;
    public  int direction;
    public bool buttonPressed;
    // 0 right, 1 left

    //navigation navigationObject;

    void Awake() {
        normalDisplay();
        //navigationObject = quitMenu.GetComponent<navigation>();
    }

    void Start() {
        buttonLeft.gameObject.GetComponent<Button>().onClick.AddListener(() => onClickDirection(buttonLeft.gameObject));
        buttonRight.gameObject.GetComponent<Button>().onClick.AddListener(() => onClickDirection(buttonRight.gameObject));
        
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
        buttonPressed = false;
        quitMenu.alpha = 1;
        quitMenu.interactable = true;
        quitMenu.blocksRaycasts = true;

        mainPage.alpha = .5f;
        mainPage.interactable = false;
        mainPage.blocksRaycasts = false;
    }
    
    public void onClickDirection(GameObject button) {
        buttonPressed = true;
        if (button.name.Equals(buttonRight.name)) {
            direction = 1;
        } else {
            direction = 2;
        }
        normalDisplay();
    }



}