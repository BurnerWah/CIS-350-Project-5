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

    public GameObject mainMenuCanvas;

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
        Pause();
        if(win)
        {
            gameObject.transform.Find("Panel").gameObject.transform.Find("GameOverText").gameObject.GetComponent<Text>().text =
                "You Win!\nYou Killed Big Covid!";
        }
        else
        {
            gameObject.transform.Find("Panel").gameObject.transform.Find("GameOverText").gameObject.GetComponent<Text>().text =
                "You Lose!\nRemember to Dodge!";
        }
    }

    public void LevelOneGameOver(int kills)
    {
        gameOverUI.SetActive(true);
        levelOneButton.SetActive(false);
        levelTwoButton.SetActive(true);
        Pause();
        gameObject.transform.Find("Panel").gameObject.transform.Find("GameOverText").gameObject.GetComponent<Text>().text =
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
