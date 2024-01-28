using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    [SerializeField] private GameObject gameManagerObject;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float smoothTime = .1f;
    [SerializeField] private AudioClip[] footstepSounds;

    private GameInput gameInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private Rigidbody2D rb;
    private GameManager gameManager;

    private Vector2 smoothedInput;
    private Vector2 inputVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameInput = gameManagerObject.GetComponent<GameInput>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameManager.isGameOver)
        {
            audioSource.volume = 0;
        }
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
            StepSound();
        }
        else if (inputVector.x > 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("Walking", true);
            StepSound();
        }
        else if (inputVector.y != 0)
        {
            animator.SetBool("Walking", true);
            StepSound();
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void StepSound()
    {
        if (!audioSource.isPlaying)
        {
            int randomSound = Random.Range(0, footstepSounds.Length);
            audioSource.PlayOneShot(footstepSounds[randomSound]);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Exit"))
        {
            gameManagerObject.GetComponent<GameManager>().GameWon();
        }
    }
}