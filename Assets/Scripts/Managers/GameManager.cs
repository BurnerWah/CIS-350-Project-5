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
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Submarine SubmarineS;
    Stack<string> levelStack = new Stack<string>();

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
}
