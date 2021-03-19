using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Animator animator;
    private AudioSource sound;
    private bool activated;
    private Respawn playerRespawn;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        playerRespawn = GameObject.FindWithTag("Player").GetComponent<Respawn>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
      if(other.gameObject.tag == "Player")
      {
        animator.SetBool("Activated", true);
        playerRespawn.respawnPoint = this.gameObject;

        if(!activated)
        {
          sound.Play();
          activated = true;
        }
  //      Debug.Log("Player has made it to this checkpoint");
      }
    }

}
