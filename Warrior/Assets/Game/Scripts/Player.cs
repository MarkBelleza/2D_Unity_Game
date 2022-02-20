using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float moveForce = 10f;
    private float jumpForce = 7.5f;
    private float inputX;
    public Rigidbody2D myBody;
    private SpriteRenderer sr;
    private Animator anim;
    private string RUN_ANIMATION = "Run";
    private string JUMP_UP_ANIMATION = "JumpUp";
    private string GROUND_TAG = "Ground";
    private string FALLING_ANIMATION = "Falling";
    private bool isGround = true;

    //Dash Limiters Variables
    private float nextDashTime = 0f;   
    private float dashRate = 1f;
    private bool dashedInAir = false;

    private float dashTime;
    private float startDashTime = 0.2f;
    private bool dashed = false;


    public void setMoveForce(float val)
    {
        moveForce = val;
    }

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        dashTime = startDashTime;
    }

  


    // Update is called once per frame
    void Update()
    {
        if ((Time.time >= nextDashTime) && (Input.GetKeyDown(KeyCode.Mouse1)))
        {
            PlayerDash();
        }
        checkDash();
        PlayerMoveKeyboard(); //Running Animation
        PlayerJump();
        anim.SetFloat(FALLING_ANIMATION, myBody.velocity.y); // Falling Animation
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            moveForce = 5f; 
        }
        
    }

    void checkDash()
    {
        if (dashed)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                inputX = 0;
                dashTime = startDashTime;
                myBody.velocity = new Vector2(0f, 0f);
                anim.SetBool("outDash", true);
                dashed = false;
            }
        }
    }

    void PlayerDash()
    {
            if ((isGround == false) && (dashedInAir == false))
            {
                myBody.velocity = new Vector2(transform.localScale.x * 10, myBody.velocity.y) ;
                anim.SetTrigger("InDash");
                dashedInAir = true;
            }
            else if (isGround)
            {
                myBody.velocity = new Vector2(transform.localScale.x * 10, myBody.velocity.y);
                anim.SetTrigger("InDash");
            }
            nextDashTime = Time.time + 1f / dashRate;
        dashed = true;
    }

    void PlayerMoveKeyboard()
    {
        inputX = Input.GetAxis("Horizontal");
        transform.position += new Vector3(inputX, 0f, 0f) * Time.deltaTime * moveForce;
        // Running Animation
        if (inputX > 0) //right
        {
            transform.localScale = new Vector3(1.422106f, 1.422106f, 1.422106f); //Shouldve just been 1, but I messed with the scaling when I spawned the Player sprite T^T
            anim.SetBool(RUN_ANIMATION, true);
            //sr.flipX = false;
        }
        else if (inputX < 0) //left 
        {
            transform.localScale = new Vector3(-1.422106f, 1.422106f, 1.422106f);
            anim.SetBool(RUN_ANIMATION, true);
            //sr.flipX = true;
        }
        else anim.SetBool(RUN_ANIMATION, false);


    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGround)
        {
            isGround = false;
            moveForce = 6.5f;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            anim.SetBool(JUMP_UP_ANIMATION, true); // JumpingUp Animation
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG) || collision.gameObject.CompareTag("Player"))
        {
            isGround = true;
            dashedInAir = false;
            moveForce = 10f;
            anim.SetBool(JUMP_UP_ANIMATION, false);
        }
    }
}
