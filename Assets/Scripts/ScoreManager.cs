using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public string bestPlayer;//highest score player name
    public string playerName;//current player name
    public int bestScore;//highest score

    public string filePath;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        filePath = Application.persistentDataPath + "/savefile.json";
        DontDestroyOnLoad(gameObject);
        LoadScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string bestPlayer;
        public int bestScore;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();

        data.bestPlayer = bestPlayer;
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestPlayer = data.bestPlayer;
            bestScore = data.bestScore;
            Debug.Log("bestPlayer = " + bestPlayer + ", bestScore = " + bestScore);
        }
    }

    public void CheckScore(int playerScore)
    {
        if (playerScore > bestScore)
        {
            GameObject.Find("MainManager").GetComponent<MainManager>().m_newHighScore = true;
            bestScore = playerScore;
            bestPlayer = playerName;
            SaveScore();
        }
        else
        {
            GameObject.Find("MainManager").GetComponent<MainManager>().m_newHighScore = false;
        }
        Debug.Log($"playerScore : {playerScore}; bestScore : {bestScore}; playerName : {playerName}");
    }

    public void ClearScore()
    {
        SaveData data = new SaveData();
        data.bestPlayer = "";
        data.bestScore = 0;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }


}
