using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{

    public BoxCollider2D bound;
    public CameraScript cam;
    public Camera main;

    private void Start()
    {
        cam = main.GetComponent<CameraScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            cam.SetActiveBound(bound);
        }
    }

}
