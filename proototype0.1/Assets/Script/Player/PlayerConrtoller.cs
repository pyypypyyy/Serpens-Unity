using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; 

    private Rigidbody2D rb;

    private Vector3 offset;

    void Start()
    {
        offset = Camera.main.transform.position - transform.position;
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        
        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;

        
        rb.velocity = movement * moveSpeed;

        Camera.main.transform.position = transform.position + offset;
    }
}