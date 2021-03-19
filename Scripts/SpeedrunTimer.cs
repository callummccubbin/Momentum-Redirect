using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SpeedrunTimer : MonoBehaviour
{
    public float timeStart;
    public TextMeshProUGUI textBox;
    public TimeSpan timeSpan;

    void Start()
    {
      timeStart = PlayerPrefs.GetFloat("TimePlayed");

      if(PlayerPrefs.GetInt("timerEnabled") == 1)
      {
        textBox.transform.parent.gameObject.SetActive(true);
        textBox.text = "00:00.00";
      } else {
        textBox.transform.parent.gameObject.SetActive(false);
      }
    }

    void Update()
    {
      timeStart += Time.deltaTime;
      timeSpan = TimeSpan.FromSeconds(timeStart);
      textBox.text = timeSpan.ToString("mm':'ss'.'ff");
    }

    public void RecordTime ()
    {
        PlayerPrefs.SetString(SceneManager.GetActiveScene().name, timeSpan.ToString("mm':'ss'.'ff"));
        Debug.Log(SceneManager.GetActiveScene().name + timeSpan.ToString("mm':'ss'.'ff"));
    }
}
