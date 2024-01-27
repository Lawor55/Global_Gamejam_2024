using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isGameOver;

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

        FreezeTime(true);
        Debug.Log("Game Over");
        isGameOver = true;
    }
}
