using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [Header("Idle Sound Time Settings")]
    [SerializeField] [Range(0.2f, 20)] private float idleSoundMin;
    [SerializeField] [Range(0.1f, 10)] private float idleSoundVariation;

    [Header("Agression Sound Time Settings")]
    [SerializeField] [Range(0.2f, 20)] private float agressiveSoundMin;
    [SerializeField] [Range(0.2f, 10)] private float agressiveSoundVariation;

    [Header("All Audioclips that should be played")]
    [SerializeField] private AudioClip[] enemySounds;
    private EnemyMovement enemyMovement;

    private GameManager gameManager;
    private AudioSource audioSource;
    private float audioCooldownIdle;
    private float audioCooldownAgressive;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<EnemyMovement>().gameManager;

        audioSource = GetComponent<AudioSource>();
        enemyMovement = GetComponent<EnemyMovement>();

        audioCooldownIdle = Random.Range(idleSoundMin, idleSoundMin + idleSoundVariation);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameOver)
        {
            audioSource.volume = 0;
        }

        if (enemyMovement.enemyAgressive)
        {
            if (audioCooldownAgressive <= 0)
            {
                PlaySound();
                audioCooldownAgressive = Random.Range(agressiveSoundMin, agressiveSoundMin + agressiveSoundVariation);
                Debug.Log("Agressive Cooldown");
            }
            else
            {
                audioCooldownAgressive -= Time.deltaTime;
            }
        }
        else
        {
            if (audioCooldownAgressive != 0)
            {
                audioCooldownAgressive = 0;
            }
            if (audioCooldownIdle <= 0)
            {
                PlaySound();
                audioCooldownIdle = Random.Range(idleSoundMin, idleSoundMin + idleSoundVariation);
                Debug.Log("Idle Cooldown");
            }
            else
            {
                audioCooldownIdle -= Time.deltaTime;
            }
        }
    }

    void PlaySound()
    {
        if (gameManager.isGameOver)
        {
            return;
        }
        int randomSound = Random.Range(0, enemySounds.Length);
        Debug.Log("Selected random Sound: " + randomSound + ": " + enemySounds[randomSound].name);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(enemySounds[randomSound]);
        }
    }
}
