using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public string bestPlayer;//highest score player name
    public string playerName;//current player name
    public int bestScore;//highest score
    public int playerScore;//current player score

    public GameObject inputTextField;
    public GameObject playerNameTextField;

    private void Start()
    {
        string startPrompt = ScoreManager.Instance.bestPlayer;
        Debug.Log("ScoreManager.Instance.bestPlayer = " + ScoreManager.Instance.bestPlayer);
        ShowBestScoreUIHandler();
    }

    public void ShowBestScoreUIHandler()
    {
        if (string.IsNullOrEmpty(ScoreManager.Instance.bestPlayer))
        {
            playerNameTextField.GetComponent<Text>().text = "Hello!";
        }
        else
        {
            playerNameTextField.GetComponent<Text>().text = $"Best score : { ScoreManager.Instance.bestPlayer} : {ScoreManager.Instance.bestScore}";
        }
            
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SaveScoreUIHandler()
    {
        ScoreManager.Instance.bestPlayer = playerName;//delete after complete test
        ScoreManager.Instance.bestScore = 3;//delete after complete test
        ScoreManager.Instance.SaveScore();
    }

    public void ClearScoreUIHandler()
    {
        ScoreManager.Instance.ClearScore();
    }

    public void LoadScoreUIHandler()
    {
        ScoreManager.Instance.LoadScore();
    }

    public void ReadStringInput()
    {
        string inputPlayerName = inputTextField.GetComponent<Text>().text;
        if (string.IsNullOrEmpty(inputPlayerName))
        {
            playerNameTextField.GetComponent<Text>().text = "Please, write your name!";
        }
        else
        {
            playerName = inputPlayerName;
            ScoreManager.Instance.playerName = playerName;
            StartNew();
        }

    }

    
}

