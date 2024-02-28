using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    //private Animator animatior;

    private float inputX, inputY;
    private float stopX, stopY;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        //animatior = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        Vector2 input = new Vector2(inputX, inputY).normalized;
        rb.velocity = input * moveSpeed;
        /*
                if (input != Vector2.zero)
                {
                    animatior.SetBool("isMoving", true);
                    stopX = inputX;
                    stopY = inputY;
                }
                else
                {
                    animatior.SetBool("isMoving", false);
                }
                animatior.SetFloat("InputX", stopX);
                animatior.SetFloat("InputY", stopY);

        */
    }


}