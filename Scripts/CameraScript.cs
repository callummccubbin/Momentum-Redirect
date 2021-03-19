using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraScript : MonoBehaviour
{
    public Rigidbody2D target;
    public BoxCollider2D initialBound;
    public float followSpeed = .2f;
    public float snapSpeed = 0.8f;
    public BoxCollider2D activeBound;
    public BoxCollider2D cameraBox;
    private Camera mainCamera;
    public float minCameraSize = 15f;
    public float cameraSizeFactor = 0.4f;
    public float cameraSizeSpeed = 0.015f;
    public float cameraSizeSnapSpeed = 0.8f;
    private float maxCameraSize;

    private void Start()
    {
        cameraBox = GetComponentInChildren<BoxCollider2D>();
        mainCamera = Camera.main;
        SetActiveBound(initialBound);
        SetCameraBox();
        LerplessMove();
    }

    private void SetCameraBox()
    {

        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize,
                                                 Mathf.Clamp(target.velocity.magnitude * cameraSizeFactor,
                                                             minCameraSize,
                                                             maxCameraSize),
                                                 cameraSizeSpeed);

        cameraBox.size = new Vector2(2f * mainCamera.orthographicSize * mainCamera.aspect, 2f * mainCamera.orthographicSize);
        //Debug.Log("maxCameraSize is " + maxCameraSize);
        //Debug.Log("mainCamera.orthographicSize is " + mainCamera.orthographicSize);
    }

    private void MoveCamera()
    {
      transform.position = Vector3.Lerp(transform.position,
                                        new Vector3(Mathf.Clamp(target.transform.position.x,
                                                                activeBound.bounds.min.x + cameraBox.size.x / 2,
                                                                activeBound.bounds.max.x - cameraBox.size.x / 2),
                                                    Mathf.Clamp(target.transform.position.y,
                                                                activeBound.bounds.min.y + cameraBox.size.y / 2,
                                                                activeBound.bounds.max.y - cameraBox.size.y / 2),
                                                    transform.position.z),
                                        followSpeed);
    }


    public void Death()
    {
      activeBound = target.gameObject.GetComponent<Respawn>().respawnPoint.transform.parent.GetComponent<BoxCollider2D>();
      mainCamera.orthographicSize = minCameraSize;
      LerplessMove();
    }

    public void LerplessMove()
    {
      transform.position = new Vector3(Mathf.Clamp(target.transform.position.x,
                                                   activeBound.bounds.min.x + cameraBox.size.x / 2,
                                                   activeBound.bounds.max.x - cameraBox.size.x / 2),
                                       Mathf.Clamp(target.transform.position.y,
                                                   activeBound.bounds.min.y + cameraBox.size.y / 2,
                                                   activeBound.bounds.max.y - cameraBox.size.y / 2),
                                                   transform.position.z);
    }

    public void SetActiveBound(BoxCollider2D newBound)
    {
        activeBound = newBound;
        transform.position = Vector3.Lerp(transform.position,
                                          new Vector3(Mathf.Clamp(target.transform.position.x,
                                                                  activeBound.bounds.min.x + cameraBox.size.x / 2,
                                                                  activeBound.bounds.max.x - cameraBox.size.x / 2),
                                                      Mathf.Clamp(target.transform.position.y,
                                                                  activeBound.bounds.min.y + cameraBox.size.y / 2,
                                                                  activeBound.bounds.max.y - cameraBox.size.y / 2),
                                                      transform.position.z),
                                          snapSpeed);

        float activeBoundRatio = activeBound.size.x / activeBound.size.y;

        maxCameraSize = Mathf.Min(activeBound.size.x / mainCamera.aspect / 2, activeBound.size.y / 2);

        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize,
                                                 Mathf.Clamp(target.velocity.magnitude * cameraSizeFactor,
                                                             minCameraSize,
                                                             maxCameraSize),
                                                 cameraSizeSnapSpeed);

        cameraBox.size = new Vector2(2f * mainCamera.orthographicSize * mainCamera.aspect, 2f * mainCamera.orthographicSize);
      }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            if (activeBound)
            {
                SetCameraBox();
                MoveCamera();


            }
            else {
                transform.position = Vector3.Lerp(transform.position,
                                                  new Vector3(target.transform.position.x,
                                                              target.transform.position.y,
                                                              transform.position.z),
                                                  followSpeed);

            }
        }
    }
}
