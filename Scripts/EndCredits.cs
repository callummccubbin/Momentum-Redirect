using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndCredits : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator transition;
    public GameObject Times;
    public TextMeshProUGUI Level1;
    public TextMeshProUGUI Level2;
    public TextMeshProUGUI Level3;
    public TextMeshProUGUI Level4;
    public TextMeshProUGUI Level5;

    void Start()
    {
      if(PlayerPrefs.GetInt("timerEnabled") == 1)
      {
        Debug.Log("SR=" + PlayerPrefs.GetInt("timerEnabled"));
        Times.SetActive(true);
        Level1.text = PlayerPrefs.GetString("Level 1");
        Level2.text = PlayerPrefs.GetString("Level 2");
        Level3.text = PlayerPrefs.GetString("Level 3");
        Level4.text = PlayerPrefs.GetString("Level 4");
        Level5.text = PlayerPrefs.GetString("Level 5");

      } else {

        Times.SetActive(false);
      }
    }

    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
          StartCoroutine(Exit());
        }
    }

    IEnumerator Exit()
    {
      transition.SetTrigger("Fade");
      yield return new WaitForSeconds(1);
      SceneManager.LoadScene(0);
    }
}
