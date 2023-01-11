using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Animator animator;
    public ParticleSystem dustEffect;
    private Rigidbody2D rb2D;
    private bool landed;
    private float moveSpeed;
    private float jumpForce;
    private bool isJumping;
    private bool canDoubleJump;
    private float moveHorizontal;
    private float moveVertical;
    private bool boolean1;
    private float doubleJumpForce;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        moveSpeed = 1.25f;
        jumpForce = 30f;
        doubleJumpForce = 20f;
        isJumping = false;

    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

    }


    private void FixedUpdate()
    {
        if (moveHorizontal > 0f || moveHorizontal < 0f)
        {
            
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }

        if (moveVertical == 0f)
        {
            boolean1 = true;
        }


        if (!isJumping && moveVertical > 0f)
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
            boolean1 = false;
        }

        else if (isJumping && moveVertical > 0f && canDoubleJump && boolean1)
        {
            canDoubleJump = false;
            rb2D.AddForce(new Vector2(0f, moveVertical * doubleJumpForce), ForceMode2D.Impulse);
        } 

        else if (isJumping)
        { 
            rb2D.drag = 5.5f;
        }

        else
        {
            rb2D.drag = 3.1f;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            if (landed)
            {
                dustEffect.Play();
                landed = false;
            }
            animator.SetBool("Jump", false);
            isJumping = false;
            canDoubleJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            landed = true;
            animator.SetBool("Jump", true);
            isJumping = true;
        }
    }



}
