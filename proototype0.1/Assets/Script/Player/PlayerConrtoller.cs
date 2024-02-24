using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
   
    private Rigidbody rb;
    private Animator animatior;

    private float inputX, inputY;
    private float stopX, stopY;

    private Vector3 offset;
   

    private void Start()
    {
        offset = Camera.main.transform.position - transform.position;
        rb = GetComponent<Rigidbody>();
        animatior = GetComponent<Animator>();
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        Vector2 input = new Vector2(inputX, inputY).normalized;
        rb.velocity = input * moveSpeed;

        if(input != Vector2.zero)
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

        Camera.main.transform.position = transform.position + offset;
    }

    
}
