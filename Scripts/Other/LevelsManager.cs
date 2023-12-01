using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    private string sdfsd;
    void FadeInDeactivate()
    {
        gameObject.SetActive(false);
    }
    void LoadNextLevel()
    {
        if(SceneManager.GetActiveScene().name == "Credits")
        {
            SceneManager.LoadScene("StartMenu");
        }
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
