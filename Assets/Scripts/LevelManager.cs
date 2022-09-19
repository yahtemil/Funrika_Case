using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int LevelValue;

    void Awake()
    {
        instance = this;
        LevelValue = PlayerPrefs.GetInt("Level", 1);
    }

    public void LevelCompleted()
    {
        PlayerPrefs.SetInt("Level", LevelValue + 1);
        UIManager.instance.CompletedPanel.SetActive(true);
        UIManager.instance.HelpObject.SetActive(false);
    }
}
