using UnityEngine;

public class pauseControl : MonoBehaviour
{
   public GameObject pauseMenu;

   public void PauseGame(){
       pauseMenu.SetActive(true);
   }

   public void ResumeGame(){
       pauseMenu.SetActive(false);
   }
}
