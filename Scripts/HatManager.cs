using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatManager : MonoBehaviour
{

    public GameObject[] Hats;

    void Start()
    {
      foreach(GameObject Hat in Hats)
      {
        Hat.SetActive(false);
      }

      if(PlayerPrefs.GetInt("CurrentHat") > 0)
      {
        Hats[PlayerPrefs.GetInt("CurrentHat") - 1].SetActive(true);
      }
    }

}
