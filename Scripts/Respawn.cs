using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Respawn : MonoBehaviour
{

  public GameObject respawnPoint;
  public Rigidbody2D Rigidbody;
  private CameraScript cameraScript;
  public AudioSource deathSound;

  void Start()
  {
    cameraScript = Camera.main.gameObject.GetComponent<CameraScript>();
  }

  public void Respawning()
  {
    transform.position = respawnPoint.transform.position;
    Rigidbody.velocity = Vector2.zero;
  //  Debug.Log("Respawning...");
  }

  public void Update()
  {
    if(Input.GetButtonDown("Retry"))
    {
      Death();
    }
  }

  public void Death()
  {
    Respawning();
    cameraScript.Death();
    deathSound.Play();
  }
}
