using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Teleporter destination;
    private AudioSource sound;
    public bool facingRight;

    void Start()
    {
      sound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
        col.transform.position = new Vector2(destination.transform.position.x, destination.transform.position.y + col.transform.position.y - transform.position.y + 0.05f);
        if (destination.facingRight == facingRight)
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            col.GetComponent<player>().xReverse = true;
        }
        sound.Play();
    }

}
