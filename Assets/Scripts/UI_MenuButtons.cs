using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MenuButtons : MonoBehaviour
{
   public void Restart()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
   }

   public void Play()
   {
      SceneManager.LoadScene(1, LoadSceneMode.Single);
   }

   public void Quit()
   {
      Application.Quit();
   }
}
