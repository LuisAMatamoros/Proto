using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float JumpSpeed = 12;
    float jumpMult = 1;
    public float raycastOffset = 0.4f;
    public float raycastDistance = 1f;
    private int jumpCount;
    [Range (1,4)]
    public int playerindex; 
    protected Rigidbody2D _RB;    //RIGID BODY OF THE CHARACTER
    protected SpriteRenderer _SR;

    [SerializeField]
    private KeyCode _ShootKey;




    private void Awake()
    {
        _RB = GetComponent<Rigidbody2D>();     //GETTING THE COMPONENTS FROM THE GAME OBJECT 
        _SR = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        bool isGrounded = IsGrounded(raycastOffset) || IsGrounded(-raycastOffset); //CHECK IF THE CHARACTER IS GROUNDED
        float horizontalInput  = Input.GetAxis("Horizontal_" + (playerindex));
        if (horizontalInput < 0) _SR.flipX = true;                                                                  
        else if (horizontalInput > 0) _SR.flipX = false;
        _RB.velocity = new Vector2(moveSpeed * horizontalInput, _RB.velocity.y);  //CHARACTER MOVING

        if ((isGrounded || jumpCount < 2) && Input.GetKeyDown(_ShootKey))   //JUMPING 
        {
            jumpCount++; 
            _RB.velocity = new Vector2(_RB.velocity.x, JumpSpeed * jumpMult);
        }
    }


    protected bool IsGrounded(float offsetX)     //FUNCTION THAT CHECKS IF ITS GROUNDED 
    {
        Vector2 origin = transform.position; //the pivot point //vector 2 is 2 floats    //MAYBE ITS NOT WORKING BECAUSE THE PIVOT ITS NOT ON THE BOTTOM OF THE CHARACTER 
        origin.x += offsetX;
        RaycastHit2D hitInfo = Physics2D.Raycast(origin, Vector2.down, raycastDistance); //(from origin cast a raydown, a given distance)
        Debug.DrawRay(origin, Vector2.down*raycastDistance, Color.red);
        if (hitInfo == true)         //ASK WILLY HOW TO DEBUG RAYCASTERS
        {
            print("yuuuuuuuuuus");
            jumpCount = 0;
        }
        else
        {
            print("NOOOOOOOES");
        }
        return hitInfo; //if it has something returns ture      //NOT RETURNING TRUE MAYBE

    }



}
