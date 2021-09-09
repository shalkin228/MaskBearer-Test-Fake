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
    [SerializeField] private float speed = 20f;
    [SerializeField] private float jumpMaxFrames = 23;
    [SerializeField] private float jumpFrames = 0;
    [SerializeField] private float runStopFrames=0;
    [SerializeField] private float runStopFramesMax = 7;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float gravityperframe = 0.5f;
    [SerializeField] private float maxFallSpeed = -15f;
    static private Rigidbody2D hitbox;
    float prevDir; //NOT DIR VALUE IN PREVIOUS FRAME
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
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        ctrl = new InputMaster();
        ctrl.Player.Jump.started += ctx => StartJump();
        ctrl.Player.Jump.canceled += ctx => StopJump();
    }
    public void setmypos(Vector3 vec)
    {
        transform.position = vec;
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
        prevDir = transform.localScale.x;
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
        if (transform.localScale.x!=prevDir&&IsOnGround())
        {
            anim.SetTrigger("Turn");
        }
        /*else
        {
            anim.ResetTrigger("Turn");
        }*/
        Debug.Log(dir +" "+ prevDir);
        anim.SetFloat("vertSpeed", hitbox.velocity.y);
        anim.SetBool("isRunning", runStopFrames<runStopFramesMax+1);
        anim.SetBool("isGrounded", IsOnGround());
    }
    private void Move()
    {
        transform.Find("IsOnGroundTrigger").position = transform.position;
        dir = ctrl.Player.Walk.ReadValue<float>();
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
        hitbox.velocity = new Vector3(((dir * speed) + hitbox.velocity.x*0.8f)/2, Mathf.Max(hitbox.velocity.y - 0.6f, maxFallSpeed), 0);
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