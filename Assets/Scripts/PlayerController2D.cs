using System;
using UnityEditor.Rendering;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float smoothTime = .1f;

    private Rigidbody2D rb;

    private Vector2 smoothedInput;
    private Vector2 inputVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }
}