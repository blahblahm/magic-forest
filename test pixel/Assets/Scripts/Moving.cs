using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moving : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private Rigidbody2D rb;
    private float moveInput;

    public Transform groundPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    private bool doubleJump;
    private bool isActiveDoubleJump;
    private float doubleJumpTimer = 0f;
    private int doublecounter = 1;

    private Animator anim;

    private float boostTimer = 0f;
    private potionAppearBlue potionBlue;
    private potionAppearGreen potionGreen;
    private potionAppearRed potionRed;
    private bool boosting;

    public Rigidbody2D crate1;
    public Rigidbody2D crate2;
    public Rigidbody2D crate3;
    private float strenghtTimer = 0f;
    private bool strenght;

    [SerializeField] private AudioSource drinking;

    void Awake()
    {
       potionBlue = GameObject.Find("potion_blue").GetComponent<potionAppearBlue>(); 
       potionGreen = GameObject.Find("potion_green").GetComponent<potionAppearGreen>();
       potionRed = GameObject.Find("potion_red").GetComponent<potionAppearRed>(); 
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boosting = false;
        isActiveDoubleJump = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveInput = Input. GetAxisRaw("Horizontal");
        rb .velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    void Update()
    {
        
        WalkingAnimation();
        Jumping();  
        DoubleJump();
        JumpingAnimation();
        Boosting();
        Strenght();
        Flip();
    }

    private void WalkingAnimation(){
        if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
    }

    private void Jumping(){
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (isJumping && Input.GetKey(KeyCode.Space))
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
                doubleJump = false;
            }
        }
        if (!doubleJump && isActiveDoubleJump && !IsGrounded() && Input.GetKeyDown(KeyCode.Space) && doublecounter > 0)
        {
            doublecounter--;
            doubleJump = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && doubleJump && isJumping)
        {
            if (jumpTimeCounter > 0 && doublecounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
                doubleJump = false;
            }

        }
        if (IsGrounded())
        {
            doublecounter = 1;
        }
        // Debug.Log(" " + isActiveDoubleJump + doubleJump + isJumping);
        // Debug.Log(doublecounter);

    }

    private void JumpingAnimation(){
        if (rb.velocity.y == 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }

        if (rb.velocity.y > 0)
        {
            anim.SetBool("isJumping", true);
        }

        if (rb.velocity.y < 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            doubleJump = false;
        }

        if (IsGrounded())
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }
    }

    private void DoubleJump(){
       
        if (potionGreen.disappear > 0 && Input.GetKey(KeyCode.B))
        {
            drinking.Play();
            isActiveDoubleJump = true;
            potionGreen.Retreat();
            potionBlue.disappear--;
        }  
    }

    void OnTriggerEnter2D(Collider2D collision)
        {
            if (boosting && collision.tag == "potionEnd")
            {
                speed = 5f;
                boostTimer = 0;
                boosting = false;
            }

            if (strenght && collision.tag == "potionEnd")
            {
                crate1.bodyType = RigidbodyType2D.Static;
                crate2.bodyType = RigidbodyType2D.Static;
                crate3.bodyType = RigidbodyType2D.Static;
                strenghtTimer = 0;
                strenght = false;
            }
        
            if (isActiveDoubleJump && collision.tag == "potionEnd")
            {
                isActiveDoubleJump = false;
                doubleJumpTimer = 0f;
            }
        }
        
    


    private void Boosting(){
        if (potionBlue.disappear > 0 && Input.GetKey(KeyCode.B))
        {
            boosting = true;
            speed = 8f;
            potionBlue.Retreat();
            drinking.Play();
            potionBlue.disappear--;
        }
    }

    private void Strenght(){

        if (potionRed.disappear > 0 && Input.GetKey(KeyCode.B))
        {
            strenght = true;
            crate1.bodyType = RigidbodyType2D.Dynamic;
            crate2.bodyType = RigidbodyType2D.Dynamic;
            crate3.bodyType = RigidbodyType2D.Dynamic;
            drinking.Play();
            potionBlue.disappear--;
        }
    }

    private void Flip(){
        if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0,180, 0);
        }
        else if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }


    private bool IsGrounded(){
        return Physics2D.OverlapCircle(groundPos.position, checkRadius, whatIsGround);
    }
}

   