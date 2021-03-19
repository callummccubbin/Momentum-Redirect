using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goo : MonoBehaviour
{

  private Respawn player;

  void Start()
  {
    player = FindObjectOfType<Respawn>();
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    if(other.gameObject.tag == "Player")
    {
      player.Death();
    }
  }
}
