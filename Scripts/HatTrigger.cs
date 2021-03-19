using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatTrigger : MonoBehaviour
{

    public int hat;
    public Animator animator;
    public AudioSource sound;

    // Update is called once per frame
    public void OnTriggerEnter2D()
    {
      PlayerPrefs.SetInt("Hat" + (hat - 1), 1);
      Debug.Log("Hat" + (hat - 1));
      StartCoroutine(NewHat());
    }

    IEnumerator NewHat()
    {
      animator.SetTrigger("activate");
      sound.Play();
      yield return new WaitForSeconds(1);

      Destroy(this.gameObject);
    }

}
