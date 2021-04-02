using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MenuButtons : MonoBehaviour
{
   public GameObject pausePanel;
   public bool isGameIsPause;

   public static UI_MenuButtons _instance;

   private void Start()
   {
      _instance = this;
   }

   public void Restart()
   {
      Time.timeScale = 1f;
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

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Escape) && pausePanel)
      {
         if (isGameIsPause)
         {
            Resume();
         }
         else
         {
            Pause();
         }
      }
   }

   void Pause()
   {
      pausePanel.SetActive(true);
      Time.timeScale = 0f;
      isGameIsPause = true;
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
   }

   public void Resume()
   {
      pausePanel.SetActive(false);
      Time.timeScale = 1f;
      isGameIsPause = false;
      Cursor.visible = false;
      Cursor.lockState = CursorLockMode.Locked;
   }

   public void LoadMenu()
   {
      Time.timeScale = 1f;
      SceneManager.LoadScene(0, LoadSceneMode.Single);
   }
}
