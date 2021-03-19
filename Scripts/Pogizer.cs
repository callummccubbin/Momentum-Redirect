using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pogizer : MonoBehaviour
{

    public Animator animPog;
    public Animator animJar;
    public AudioSource sfx;
    public GameObject JarColliders;
    private bool activated = false;
    public AudioSource MainTheme;

    public void OnTriggerEnter2D()
    {
      if(!activated)
      {
        StartCoroutine(Pogize());
      }
    }

    IEnumerator Pogize()
    {
      JarColliders.SetActive(true);
      animJar.SetBool("Open", true);

      yield return new WaitForSeconds(0.5f);

      sfx.Play();
      animPog.SetTrigger("Activate");

      yield return new WaitForSeconds(6f);

      animJar.SetBool("Open", false);

      yield return new WaitForSeconds(0.5f);

      JarColliders.SetActive(false);
      activated = true;

      GameObject.FindWithTag("Player").GetComponent<player>().MJumpEnabled = true;
      MainTheme.Play();
    }

}
