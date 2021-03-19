using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float moveSpeed = 10f;
    private Vector2 direction;
    private bool facingRight = true;
    public GameObject charHolder;
    public bool xReverse = false;
    private bool changingDirections = false;

    [Header("Vertical Movement")]
    public float jumpFloor = 10f;
    public float jumpPower = 1f;
    public float jumpFactor = .8f;
    public float jumpDelay = .25f;
    private float jumpTimer;
    private float airVelocity = 0;
    public bool MJumpEnabled = false;
    private bool canMJump = false;
    public float MJumpThreshold = 20f;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public LayerMask groundLayer;

    [Header("Physics")]
    public float maxSpeed = 100f;
    public float linearDrag = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;

    [Header("Collision")]
    public bool onGround = false;
    public bool onWall = false;
    public bool onWallRight = false;
    public bool onWallLeft = false;
    public float groundLength = 0.6f;
    public float wallLength = 0.3f;
    public Vector3 colliderOffset;
    public float wallSlideVelocity = 5f;
    public float wallJumpFactor = 1.2f;
    public Vector3 wallColliderOffset;

    [Header("SFX")]
    public AudioSource wallJumpSound;
    public AudioSource jumpSound;
    public AudioSource midairJumpSound;
    public float jumpPitchFactor = 0.03f;
    public float PitchLowerLimit = 0.6f;
    public float PitchUpperLimit = 3f;

    void Start()
    {
      if (SceneManager.GetActiveScene().buildIndex > 5)
      {
        MJumpEnabled = true;
      }
    }

    // Update is called once per frame
    void Update()
    {

      if(!xReverse)
      {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
      } else {
        if (!changingDirections)
        {
          direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
          xReverse = false;
        } else {
          direction = Vector2.zero;
        }
      }

      animator.SetFloat("vertical", direction.y);

      onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) ||
                 Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);

      onWallRight = Physics2D.Raycast(transform.position + wallColliderOffset, Vector2.right, wallLength, groundLayer) ||
                    Physics2D.Raycast(transform.position - wallColliderOffset, Vector2.right, wallLength, groundLayer);

      onWallLeft = Physics2D.Raycast(transform.position + wallColliderOffset, Vector2.left, wallLength, groundLayer) ||
                   Physics2D.Raycast(transform.position - wallColliderOffset, Vector2.left, wallLength, groundLayer);

      onWall = onWallLeft || onWallRight;

      if(onWall || onGround)
      {
        if(MJumpEnabled)
        {
          canMJump = true;
        }
      }

      if(Input.GetButtonDown("Jump"))
      {
          jumpTimer = Time.time + jumpDelay;
      }

      if (!onGround && onWall)
      {
        animator.SetBool("wall", true);
      } else {
        animator.SetBool("wall", false);
      }
    }


    void FixedUpdate()
    {

        changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);
        MoveCharacter(direction.x);
        ModifyPhysics();

        if(onGround)
        {
          airVelocity = 0;
        }

        if ((Mathf.Abs(airVelocity) < Mathf.Abs(rb.velocity.x)) && !onGround)
        {
          airVelocity = rb.velocity.x;
        }

        if(jumpTimer > Time.time)
        {
          if(onGround)
          {
            Jump();
          } else if (onWall) {
            WallJump();
          } else if (canMJump) {
            if(rb.velocity.magnitude > MJumpThreshold && direction != Vector2.zero)
            {
              MidairJump();
            }
          }
        } else if (onWall && rb.velocity.x == 0) {
          rb.velocity = new Vector2(rb.velocity.x, -wallSlideVelocity);
        }
    }

    void MoveCharacter(float horizontal)
    {
      rb.AddForce(Vector2.right * horizontal * moveSpeed);

      animator.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));
      if (!xReverse)
      {
        if((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
          Flip();
        } else if ((onWallRight && !facingRight) || (onWallLeft && facingRight)) {
          Flip();
        }
        if(Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
          rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
      }
    }

    void Jump()
    {
      //Debug.Log(rb.velocity.x);
      jumpSound.pitch = Mathf.Clamp(Mathf.Abs(rb.velocity.x) * jumpPitchFactor, PitchLowerLimit, PitchUpperLimit);
      rb.velocity = new Vector2(rb.velocity.x - (rb.velocity.x * jumpFactor), Mathf.Max(((Mathf.Abs(rb.velocity.x) * jumpFactor) + jumpPower), jumpFloor));
      jumpTimer = 0;
      jumpSound.Play();
    }

    void WallJump()
    {
      //Debug.Log(airVelocity);
      wallJumpSound.pitch = Mathf.Clamp(Mathf.Abs(airVelocity) * jumpPitchFactor, PitchLowerLimit, PitchUpperLimit);
      rb.velocity = new Vector2(((onWallLeft) ? 1 : -1) * Mathf.Abs(airVelocity) * wallJumpFactor, Mathf.Abs(airVelocity));
      xReverse = true;
      //Debug.Log((onWallLeft) ? 1 : -1);
      //Debug.Log(airVelocity);
      //Debug.Log(((onWallLeft) ? 1 : -1) * Mathf.Abs(airVelocity) * wallJumpFactor);

      jumpTimer = 0;
      wallJumpSound.Play();
    }

    void MidairJump()
    {
      midairJumpSound.pitch = Mathf.Clamp(rb.velocity.magnitude * jumpPitchFactor, PitchLowerLimit, PitchUpperLimit);
      //Debug.Log(rb.velocity.magnitude);
      rb.velocity = direction.normalized * rb.velocity.magnitude;
      canMJump = false;
      midairJumpSound.Play();
      jumpTimer = 0;
    }


    void ModifyPhysics()
    {

      if (onGround)
      {
        if (Mathf.Abs(direction.x) < 0.4 || changingDirections)
        {
          rb.drag = linearDrag;
        } else {
          rb.drag = 0f;
        }
        rb.gravityScale = 0;
      } else {
        rb.gravityScale = gravity;
        rb.drag = linearDrag * 0.15f;
        if(rb.velocity.y < 0)
        {
          rb.gravityScale = gravity * fallMultiplier;
        } else if(rb.velocity.y > 0 && !Input.GetButtonDown("Jump")) {
          rb.gravityScale = gravity * (fallMultiplier / 2);
        }
      }
    }

    void Flip()
    {
        facingRight = !facingRight;
        charHolder.transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    private void OnDrawGizmos()
    {
      Gizmos.color = Color.red;
      Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
      Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);

      Gizmos.color = Color.blue;
      Gizmos.DrawLine(transform.position, transform.position + 0.5f * (Vector3.right * rb.velocity.x + Vector3.up * rb.velocity.y));

      Gizmos.color = Color.green;
      Gizmos.DrawLine(transform.position + wallColliderOffset, (transform.position + wallColliderOffset) + Mathf.Sign(airVelocity) * Vector3.right * wallLength);
      Gizmos.DrawLine(transform.position - wallColliderOffset, (transform.position - wallColliderOffset) + Mathf.Sign(airVelocity) * Vector3.right * wallLength);
    }
}
