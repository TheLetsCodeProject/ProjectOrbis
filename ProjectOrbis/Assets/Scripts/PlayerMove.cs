using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb2d;
    public float speed = 5f;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Mathf.Round(Input.GetAxis("Horizontal"));
        float yMove = Mathf.Round(Input.GetAxis("Vertical"));
        Vector2 movement = new Vector2(xMove, yMove);

        // transform.Translate(movement * Time.deltaTime * speed);
        rb2d.velocity = movement * speed;

        if (xMove != 0f || yMove != 0f)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        anim.SetFloat("MoveX", xMove);
        anim.SetFloat("MoveY", yMove);
    }
}