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

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard(); //Running Animation
        PlayerJump();
        anim.SetFloat(FALLING_ANIMATION, myBody.velocity.y); // Falling Animation
        
    }

    void PlayerMoveKeyboard()
    {
        inputX = Input.GetAxis("Horizontal");
        transform.position += new Vector3(inputX, 0f, 0f) * Time.deltaTime * moveForce;

        // Running Animation
        if (inputX > 0) //right
        {
            transform.localScale = new Vector3(1.422106f, 1.422106f, 1.422106f);
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
            moveForce = 10f;
            anim.SetBool(JUMP_UP_ANIMATION, false);
        }
    }
}
