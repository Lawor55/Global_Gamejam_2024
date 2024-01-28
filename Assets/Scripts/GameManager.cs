using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioClip deathSound;
    [HideInInspector] public bool isGameOver;
    [HideInInspector] public bool died = false;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject restartButton;
    
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
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
        restartButton.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Debug.Log("Game Over");
        isGameOver = true;
    }

    public void GameWon()
    {
        winScreen.SetActive(true);
        GameOver();
    }

    public void Restart()
    {
        restartButton.SetActive(false);
        FreezeTime(false);
        isGameOver = false;
        SceneManager.LoadScene(0);
    }
}
