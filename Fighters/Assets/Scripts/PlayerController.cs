using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed, jumpForce;

    private float velocity;
    //private float vertical;

    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    public Animator anim;

    public bool isKeyBoard2;

    public float timeBetweenAttacks = .25f;
    private float attackCounter;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.AddPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (isKeyBoard2)
        {
            velocity = 0;

            if (Keyboard.current.lKey.isPressed)
            {
                velocity += 1f;
            }

            if(Keyboard.current.jKey.isPressed)
            {
                velocity = -1f;
            }

            if(isGrounded && Keyboard.current.rightShiftKey.wasPressedThisFrame)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }
            if(!isGrounded && Keyboard.current.rightShiftKey.wasReleasedThisFrame && theRB.velocity.y > 0)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y * .25f);
            }
            if(Keyboard.current.enterKey.wasPressedThisFrame)
            {
                anim.SetTrigger("attack");
                attackCounter = timeBetweenAttacks;
            }
        }
            
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

        theRB.velocity = new Vector2(velocity * moveSpeed, theRB.velocity.y);
        //theRB.velocity = new Vector2(theRB.velocity.x, vertical * moveSpeed);

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("ySpeed", theRB.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(theRB.velocity.x));

        if(theRB.velocity.x < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if(theRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }

        if(attackCounter > 0)
        {
            attackCounter = attackCounter - Time.deltaTime;
            theRB.velocity = new Vector2(0f, theRB.velocity.y);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        velocity = context.ReadValue<Vector2>().x;
        //vertical = context.ReadValue<Vector2>().y;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }

        if(!isGrounded && context.canceled && theRB.velocity.y > 0f)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y * .25f);
        }
       
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anim.SetTrigger("attack");
            attackCounter = timeBetweenAttacks;
        }
    }
}
