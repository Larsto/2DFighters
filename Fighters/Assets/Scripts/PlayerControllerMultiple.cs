using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControllerMultiple : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed, jumpForce;

    private float velocity;
    //private float vertical;

    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    public Animator anim;

    public float timeBetweenAttacks = .25f;
    private float attackCounter;

    public bool selected;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

        theRB.velocity = new Vector2(velocity * moveSpeed, theRB.velocity.y);
        //theRB.velocity = new Vector2(theRB.velocity.x, vertical * moveSpeed);

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("ySpeed", theRB.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(theRB.velocity.x));

        //Change sprite rotation
        if (theRB.velocity.x < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (theRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        //Attack time cant attack in air
        if (attackCounter > 0)
        {
            attackCounter = attackCounter - Time.deltaTime;
            theRB.velocity = new Vector2(0f, theRB.velocity.y);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (selected)
        {
            velocity = context.ReadValue<Vector2>().x;
        }
        else
        {
            velocity = 0;
        }
        //vertical = context.ReadValue<Vector2>().y;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (selected)
        {
            if (context.started && isGrounded)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }

            if (!isGrounded && context.canceled && theRB.velocity.y > 0f)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y * .25f);
            }
        }


    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (selected)
        {
            if (context.started)
            {
                anim.SetTrigger("attack");
                attackCounter = timeBetweenAttacks;
            }
        }

    }
}
