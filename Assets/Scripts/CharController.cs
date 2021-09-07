using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CharController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float toFloor;
    private Vector2 acceleration = new Vector2(100.0f,0.1f);
    private float speed = 5f;
    private float jumpMaxFrames = 23;
    private float jumpFrames = 0;
    private float runStopFrames=0;
    private float runStopFramesMax = 7;
    private float jumpForce = 8f;
    private float gravityperframe = 0.6f;
    private float maxFallSpeed = -15f;
    static private Rigidbody2D hitbox;
    private BoxCollider2D cld;
    bool needJump=false;
    float dir;
    private Vector2 stick;
    InputMaster ctrl;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        hitbox = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        ctrl.Player.Enable();
    }
    private void OnDisable()
    {
        ctrl.Player.Disable();
    }

    private void Awake()
    {
        ctrl = new InputMaster();
        ctrl.Player.Jump.started += ctx => StartJump();
        ctrl.Player.Jump.canceled += ctx => StopJump();
    }
    void rotate(Vector2 vec)
    {
        vec = ctrl.Player.DebugCamera.ReadValue<Vector2>();
        transform.rotation = Quaternion.Euler(vec.x, vec.y, 0);
    }
    bool IsOnGround()
    {
        return transform.Find("IsOnGroundTrigger").GetComponent<CheckIsOnGround>().isGrounded;
    }
    void FixedUpdate()
    {
        hitbox.velocity = new Vector2(0, Mathf.Max(hitbox.velocity.y - gravityperframe, maxFallSpeed));
        Move();
        ProcessAnims();
        if(IsOnGround())
        {
            jumpFrames = 0;
        }
        if (needJump)
        {
            if (IsOnGround())
            {
                ContinueJump();
            }
            else
            {
                if (jumpFrames < jumpMaxFrames&&jumpFrames!=0)
                {
                    ContinueJump();
                }
                else
                {
                    StopJump();
                }
            }
        }
    }
    public void StartJump()
    {
        needJump = true;
    }
    void ContinueJump()
    {
        hitbox.velocity = new Vector2(hitbox.velocity.x,jumpForce);
        jumpFrames++;
    }
    void StopJump()
    {
        needJump = false;
        jumpFrames = jumpMaxFrames;
    }
    void ProcessAnims()
    {
        anim.SetFloat("vertSpeed", hitbox.velocity.y);
        anim.SetBool("isRunning", runStopFrames<runStopFramesMax+1);
        anim.SetBool("isGrounded", IsOnGround());
        Debug.Log(dir);
    }
    float prevdir=0;
    private void Move()
    {
        transform.FindChild("IsOnGroundTrigger").position = transform.position;
        dir = ctrl.Player.Walk.ReadValue<float>();

        if (Mathf.Abs(prevdir) < Mathf.Pow(0.7f, 5))
            prevdir = 0;
        if(dir>0)
            dir = Mathf.Abs(Mathf.Max(prevdir * 0.5f, dir));
        if (dir<0)
            dir = -Mathf.Abs(Mathf.Max(prevdir * 0.5f, dir));
        if (dir>0)
        {
            runStopFrames = 0;
            transform.localScale = new Vector3(1, 1, 1);
        }
        if(dir<0)
        {
            runStopFrames = 0;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if(dir==0)
        {
            runStopFrames++;
        }
        //Debug.Log(dir+" "+Time.time);
        hitbox.velocity = new Vector3(dir * speed, Mathf.Max(hitbox.velocity.y - 0.6f, maxFallSpeed), 0);
        prevdir = dir;
    }
    /*private void Move(float dir)//for level transitions
    {
        transform.FindChild("IsOnGroundTrigger").position = transform.position;
        if (dir > 0)
        {
            runStopFrames = 0;
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (dir < 0)
        {
            runStopFrames = 0;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (dir == 0)
        {
            runStopFrames++;
        }
        //Debug.Log(dir+" "+Time.time);
        hitbox.velocity = new Vector3(dir * speed, Mathf.Max(hitbox.velocity.y - 0.6f, maxFallSpeed), 0);
    }*/
}