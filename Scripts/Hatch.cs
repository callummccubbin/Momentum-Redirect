using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hatch : MonoBehaviour
{

    public GameObject player;
    public Animator animator;
    public Animator transition;
    private Animator playerAnim;
    public Camera mainCamera;
    public AudioSource sound;
    public float cameraZoomSpeed;
    public float cameraMinimumSize;
    public float playerSpinSize;
    private int nextSceneLoad;
    public SpeedrunTimer SpeedrunTimer;

    void Start()
    {
      mainCamera = Camera.main;
      nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D other)
    {
      player = other.gameObject;
      playerAnim = player.GetComponent<player>().animator;
      player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
      StartCoroutine(Open());
    }

    IEnumerator Open()
    {
      SpeedrunTimer.RecordTime();
      Destroy(player.GetComponent<player>());
      mainCamera.gameObject.GetComponent<CameraScript>().LerplessMove();
      Destroy(mainCamera.gameObject.GetComponent<CameraScript>());
      playerAnim.SetFloat("horizontal", 0f);
      playerAnim.SetFloat("vertical", 0f);
      animator.SetTrigger("Open");
      sound.Play();

      while(mainCamera.orthographicSize > cameraMinimumSize)
      {
        mainCamera.orthographicSize -= cameraZoomSpeed * Time.deltaTime;
        yield return new WaitForEndOfFrame();

        if(mainCamera.orthographicSize < playerSpinSize)
        {
          playerAnim.SetTrigger("spin");
        }

      }

      playerAnim.SetTrigger("spin");

      transition.SetTrigger("Fade");
      yield return new WaitForSeconds(1);
      SceneManager.LoadScene(nextSceneLoad);

    }


}
