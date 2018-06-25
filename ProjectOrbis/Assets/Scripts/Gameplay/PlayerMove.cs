using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb2d;

    [Range(0f, 25f)] //Creates a slider in the inspector
    public float speed = 5f;
    
    // Use this for initialization
    void Start()
    {
        //Get components and set them to our variables
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement Engine

        //Use Input class to calculate movement vectors, Round these floats for snappy control
        float xMove = Mathf.Round(Input.GetAxis("Horizontal"));
        float yMove = Mathf.Round(Input.GetAxis("Vertical"));
        Vector2 movement = new Vector2(xMove, yMove);

        // Applies movement, no need for Time.deltaTime (physics engine frame smooths for us)
        rb2d.velocity = movement * speed;

        //If the player is moving, set animator's isMoving to true
        if (xMove != 0f || yMove != 0f)
        {
            anim.SetBool("isMoving", true);
        }
        //If we are not moving, set isMoving to false
        else
        {
            anim.SetBool("isMoving", false);
        }
        //Set our players sprite direction
        anim.SetFloat("MoveX", xMove);
        anim.SetFloat("MoveY", yMove);
        #endregion 

    }
}