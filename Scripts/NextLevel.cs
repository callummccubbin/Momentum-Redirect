using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public Animator transition;
    private int nextSceneLoad;
    public SpeedrunTimer SpeedrunTimer;
    // Start is called before the first frame update
    void Start()
    {
      nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
      if(other.gameObject.tag == "Player")
      {
        if(nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
        {
          PlayerPrefs.SetInt("levelAt", nextSceneLoad);
        }
        StartCoroutine(LoadLevel(nextSceneLoad));

      }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Debug.Log("LoadLevel called");
        transition.SetTrigger("Fade");
        SpeedrunTimer.RecordTime();

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);
    }


}
