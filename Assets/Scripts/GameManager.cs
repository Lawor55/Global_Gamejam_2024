using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioClip deathSound;
    [HideInInspector] public bool isGameOver;
    [HideInInspector] public bool died = false;
    public GameObject deathScreen;
    public GameObject winScreen;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void FreezeTime(bool freeze)
    {
        Time.timeScale = freeze ? 0 : 1;
    }

    public void GameOver()
    {
        if (isGameOver)
        {
            return;
        }
        if (died)
        {
            audioSource.PlayOneShot(deathSound);
        }

        FreezeTime(true);
        deathScreen.SetActive(true);
        Debug.Log("Game Over");
        isGameOver = true;
    }

    public void GameWon()
    {
        winScreen.SetActive(true);
        GameOver();
    }
}
