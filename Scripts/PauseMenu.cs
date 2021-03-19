using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.Escape))
      {
        if(GameIsPaused)
        {
          Resume();
        } else {
          Pause();
        }
      }
    }

    public void Resume()
    {
      PauseMenuUI.SetActive(false);
      Time.timeScale = 1f;
      GameIsPaused = false;
      Debug.Log("Game resume");
    }

    void Pause()
    {
      //Debug.Log(SceneManager.GetActiveScene().name);
      PauseMenuUI.SetActive(true);
      Time.timeScale = 0f;
      GameIsPaused = true;
    }

    public void LoadMenu()
    {
      SceneManager.LoadScene(0);
      Time.timeScale = 1f;
      GameIsPaused = false;
    }
}
