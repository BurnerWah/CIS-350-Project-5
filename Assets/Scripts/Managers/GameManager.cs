/*
 * Jaden Pleasants, Gerard Lamoureux
 * Project 5
 * Handles Scene management, scores
 */
using System;
using System.Collections;
using System.Collections.Generic;
using SubmarineBigfish;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    Stack<string> levelStack = new Stack<string>();

    public int humanHealth = 5;

    public int score = 0;

    public int level = 0;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject levelOneButton;
    [SerializeField] private GameObject levelTwoButton;
    [SerializeField] private GameObject levelThreeButton;

    [SerializeField] private GameObject mainMenuLevelTwoButton;
    [SerializeField] private GameObject mainMenuLevelThreeButton;

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

    public void UnloadCurrentLevel()
    {
        SceneManager.UnloadSceneAsync(levelStack.Pop());
        DeleteClones();
    }

    public string GetCurrentLevel() => levelStack.Peek();

    public void BossLevelGameOver(bool win)
    {
        gameOverUI.SetActive(true);
        levelOneButton.SetActive(true);
        levelTwoButton.SetActive(false);
        levelThreeButton.SetActive(false);
        humanHealth = 5;
        score = 0;
        Pause();
        var gameovertext = gameOverUI.transform.Find("Panel").gameObject.transform.Find("GameOverText").gameObject.GetComponent<Text>();
        if (win)
        {
            gameovertext.text = "You Win!\nYou Killed Big Covid!";
        }
        else
        {
            gameovertext.text = "You Lose!\nRemember to Shoot the Covid Cells!";
        }
    }

    public void LevelOneGameOver()
    {
        gameOverUI.SetActive(true);
        levelOneButton.SetActive(false);
        if (level == 1)
        {
            levelTwoButton.SetActive(true);
            levelThreeButton.SetActive(false);
            mainMenuLevelTwoButton.SetActive(true);
        }
        else
        {
            levelTwoButton.SetActive(false);
            levelThreeButton.SetActive(true);
            mainMenuLevelThreeButton.SetActive(true);
        }
        humanHealth = 5;
        Pause();
        var gameovertext = gameOverUI.transform.Find("Panel").gameObject.transform.Find("GameOverText").gameObject.GetComponent<Text>();
        if (score < 30)
        {
            gameovertext.text = $"Game Over!\nYou killed {score} Cells!\nTry to kill more for Next Level!";
            if (level == 1)
            {
                levelTwoButton.SetActive(false);
                levelThreeButton.SetActive(false);
            }
            else
            {
                levelTwoButton.SetActive(false);
                levelThreeButton.SetActive(false);
            }
        }
        else
        {
            gameovertext.text = $"Game Over!\nYou killed {score} Cells!";
            if (level == 1)
            {
                levelTwoButton.SetActive(true);
                levelThreeButton.SetActive(false);
                mainMenuLevelTwoButton.SetActive(true);
            }
            else
            {
                levelTwoButton.SetActive(false);
                levelThreeButton.SetActive(true);
                mainMenuLevelThreeButton.SetActive(true);
            }
        }
        score = 0;
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
        DeleteClones();
        LoadLevel(levelStack.Peek());
    }

    public void DeleteClones()
    {
        var clones = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var clone in clones)
        {
            if (clone.name == "Covid cell(Clone)")
                Destroy(clone);
        }
        clones = GameObject.FindGameObjectsWithTag("Friend");
        foreach (var clone in clones)
        {
            if (clone.name == "Red blood cell(Clone)")
                Destroy(clone);
        }
        clones = GameObject.FindGameObjectsWithTag("Missile");
        foreach (var clone in clones)
        {
            if (clone.name == "Missile(Clone)")
                Destroy(clone);
        }
    }

    public void SetLevel(int i)
    {
        level = i;
    }

}
