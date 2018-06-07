using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbis.Data;

public class PlayerMove : MonoBehaviour
{
    Animator anim;
    public float speed = 5f;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Mathf.Round(Input.GetAxis("Horizontal"));
        float yMove = Mathf.Round(Input.GetAxis("Vertical"));
        Vector2 movement = new Vector2(xMove, yMove);

        transform.Translate(movement * Time.deltaTime * speed);

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