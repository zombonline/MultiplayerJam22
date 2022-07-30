using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    Rigidbody2D rb;

    [SerializeField] float speed;

    [Header("Jump Variables")]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpVelocityFallOff, fallMultiplier;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var moveX = playerInput.actions.FindAction("Move").ReadValue<Vector2>().x;
        rb.position += Vector2.right * (moveX * speed * Time.deltaTime);


        if (rb.velocity.y < jumpVelocityFallOff)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }

    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }


}
