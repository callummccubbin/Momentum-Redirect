using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    void Start()
    {
      //Debug.Log(PlayerPrefs.GetInt("levelAt"));
      if(PlayerPrefs.GetInt("levelAt") == 0)
      {
        PlayerPrefs.SetInt("levelAt", 2);
      }
    }

    public void PlayGame()
    {
      Debug.Log(PlayerPrefs.GetInt("levelAt"));
      SceneManager.LoadScene(PlayerPrefs.GetInt("levelAt"));
    }

    public void CheatCode()
    {
      if(Input.GetKey(KeyCode.X))
      {
        if(Input.GetKey(KeyCode.D))
        {
          PlayerPrefs.SetInt("levelAt", 6);
        }
      }
    }

    public void toLevelSelection()
    {
      SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
      Application.Quit();
    }
}
