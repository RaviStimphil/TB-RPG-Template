using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerControls playerControls;
    public Rigidbody rb;
    public Camera cam;
    public Vector2 moveDirection = Vector2.zero;
    public Vector3 mousePosition = Vector2.zero;
    public float moveSpeed = 5f; 
    // Start is called before the first frame update
    void Start()
    {
        //boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate(){
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        
        rb.velocity = moveDirection * moveSpeed;

        Vector3 lookDir = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = new Quaternion(angle, 0f, 0f, 0f);;
    }
    
    public void OnMovement(InputValue value){
        
            moveDirection = value.Get<Vector2>();
        
        
    }
}
