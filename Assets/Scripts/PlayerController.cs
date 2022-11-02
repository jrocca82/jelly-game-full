using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D jellyRB;

    //Constants
    public float speed = 5.0f;

    public float jumpForce = 8.0f;

    public float jumpsAllowed = 2.0f;

    //Inputs
    private float moveInput;

    private float jumpInput;

    private float prevJumpInput;

    private float timesJumped;

    void Start()
    {
        jellyRB = GetComponent<Rigidbody2D>();
        timesJumped = 0.0f;
        prevJumpInput = 0.0f;
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        jellyRB.velocity = new Vector2(moveInput * speed, jellyRB.velocity.y);

        jumpInput = Input.GetAxis("Vertical");
        if (jumpInput > 0 && timesJumped < jumpsAllowed && prevJumpInput == 0)
        {
            jellyRB.velocity = new Vector2(jellyRB.velocity.x, jumpForce);
            timesJumped++;
        }

        prevJumpInput = jumpInput;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            timesJumped = 0.0f;
        }
        if (other.gameObject.CompareTag("coin"))
        {
            Destroy(other.gameObject);
            TextManager.instance.IncreaseScore();
        }
    }
}