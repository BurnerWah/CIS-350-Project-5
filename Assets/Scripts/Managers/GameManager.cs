/*
 * Jaden Pleasants
 * Project 5
 * Handles Scene management
 */
using System;
using System.Collections;
using System.Collections.Generic;
using SubmarineBigfish;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    Stack<string> levelStack = new Stack<string>();

    public int humanHealth = 5;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject levelOneButton;
    [SerializeField] private GameObject levelTwoButton;

    public void LoadLevel(string name)
    {
        var ao = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError($"[GameManager] Unable to load level {name}");
            return;
        }
        levelStack.Push(name);
    }
    
    public void UnloadCurrentLevel() => SceneManager.UnloadSceneAsync(levelStack.Pop());

    public void BossLevelGameOver(bool win)
    {
        gameOverUI.SetActive(true);
        levelOneButton.SetActive(true);
        levelTwoButton.SetActive(false);
        humanHealth = 5;
        Pause();
        if(win)
        {
            gameOverUI.transform.Find("Panel").gameObject.transform.Find("GameOverText").gameObject.GetComponent<Text>().text =
                "You Win!\nYou Killed Big Covid!";
        }
        else
        {
            gameOverUI.transform.Find("Panel").gameObject.transform.Find("GameOverText").gameObject.GetComponent<Text>().text =
                "You Lose!\nRemember to Shoot the Covid Cells!";
        }
    }

    public void LevelOneGameOver(int kills)
    {
        gameOverUI.SetActive(true);
        levelOneButton.SetActive(false);
        levelTwoButton.SetActive(true);
        humanHealth = 5;
        Pause();
        gameOverUI.transform.Find("Panel").gameObject.transform.Find("GameOverText").gameObject.GetComponent<Text>().text =
                "Game Over!\nYou killed " + kills + " Cells!";
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
    }

    public void Retry()
    {
        SceneManager.UnloadSceneAsync(levelStack.Peek());
        LoadLevel(levelStack.Peek());
    }
}
