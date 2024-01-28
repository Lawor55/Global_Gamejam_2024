using System;
using UnityEditor.Rendering;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float smoothTime = .1f;

    private GameInput gameInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private Vector2 smoothedInput;
    private Vector2 inputVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameInput = gameManager.GetComponent<GameInput>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        smoothedInput = Vector2.SmoothDamp(
            smoothedInput,
            inputVector,
            ref inputVelocity,
            smoothTime
        );

        rb.velocity = smoothedInput * playerSpeed;
        //Debug.Log(smoothedInput * (playerSpeed * Time.deltaTime));

        if (inputVector.x < 0)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("Walking", true);
        }
        else if (inputVector.x > 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("Walking", true);
        }
        else if (inputVector.y != 0)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Exit"))
        {
            gameManager.GetComponent<GameManager>().GameWon();
        }
    }
}