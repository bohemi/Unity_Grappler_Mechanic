using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // player Movement settings...
    [Header("Movement Settings")]
    [SerializeField] private float speed = 0.0f;
    [SerializeField] private float jump_Height = 0.0f;
    [SerializeField] private float air_control = 0.0f;
    [SerializeField] private float drag_force = 0.0f;

    [Header("Ground Settings")]
    [SerializeField] private bool bIs_grounded = true;
    [SerializeField] private float ground_ray_length = 0.6f;
    [SerializeField] private LayerMask ground_mask;

    private Vector2 move_direction = Vector2.zero;
    private Rigidbody2D rigid_body = null;

    private void Start()
    {
        rigid_body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bIs_grounded = Physics2D.Raycast(transform.position, Vector2.down, ground_ray_length, ground_mask);
        Debug.DrawRay(transform.position, Vector2.down * ground_ray_length, Color.black);

        move_direction = new Vector2(Input.GetAxis("Horizontal"), 0);
        DragCheck();
    }

    private void FixedUpdate()
    {
        MovePlayer(bIs_grounded, move_direction);
    }

    private void MovePlayer(bool grounded, Vector2 direction)
    {
        if (grounded)
        {
            rigid_body.AddForce(move_direction.normalized * speed, ForceMode2D.Force);
        }
        else if (!grounded)
        {
            rigid_body.AddForce(move_direction.normalized * speed * air_control, ForceMode2D.Force);
        }
        // jumping
        if (Input.GetButton("Jump") && grounded)
        {
            //print("jumping");
            rigid_body.AddForce(Vector2.up * jump_Height, ForceMode2D.Impulse);
        }
    }

    private void DragCheck()
    {
        if (bIs_grounded)
        {
            rigid_body.drag = drag_force;
        }
        else
        {
            rigid_body.drag = 0.0f;
            //print("llllllllllllll");
        }
    }
}
