using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathMenu : MonoBehaviour
{

   public void RetryGame()
   {
       SceneManager.LoadScene("lvl1");
   }

   public void QuitGame() {
       SceneManager.LoadScene("MainMenu");
   }
}
