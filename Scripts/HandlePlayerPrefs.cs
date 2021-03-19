using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandlePlayerPrefs : MonoBehaviour
{

    public GameObject SpeedrunCheckbox;
    public Button[] HatButtons;

    void Start()
    {

      if(PlayerPrefs.GetInt("timerEnabled") == 1)
      {
        SpeedrunCheckbox.SetActive(true);
      } else {
        SpeedrunCheckbox.SetActive(false);
      }
      HatCheck();
    }

    void HatCheck()
    {
      for(int i = 0; i < HatButtons.Length; i++)
      {
        if(PlayerPrefs.GetInt("Hat" + i) == 1)
        {
          HatButtons[i].interactable = true;
          if(PlayerPrefs.GetInt("CurrentHat") - 1 != i)
          {
            HatButtons[i].gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
          }
        } else {
          HatButtons[i].interactable = false;
          HatButtons[i].gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
        }
      }
    }

    public void ResetProgress() {
      PlayerPrefs.DeleteAll();
      HatCheck();
      PlayerPrefs.SetInt("timerEnabled", 0);
      SpeedrunCheckbox.SetActive(false);
    }

    public void EnableSpeedrunTimer()
    {
      if(PlayerPrefs.GetInt("timerEnabled") == 1)
      {
        PlayerPrefs.SetInt("timerEnabled", 0);
        SpeedrunCheckbox.SetActive(false);
      } else {
        PlayerPrefs.SetInt("timerEnabled", 1);
        SpeedrunCheckbox.SetActive(true);
      }
      //Debug.Log("Speedrun: " + PlayerPrefs.GetInt("timerEnabled"));
    }

    public void SetHat(int hat)
    {
      if(PlayerPrefs.GetInt("CurrentHat") != 0)
      {
        HatButtons[PlayerPrefs.GetInt("CurrentHat") - 1].gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
      }

      if(PlayerPrefs.GetInt("CurrentHat") == hat)
      {
        PlayerPrefs.SetInt("CurrentHat", 0);
      } else {
        PlayerPrefs.SetInt("CurrentHat", hat);
        HatButtons[hat - 1].gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.white;
      }

    }

}
