using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    private string checkPointKey = "checkPointKey";

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "SceneKomix1")
        {
            PlayerPrefs.SetString(checkPointKey, "SceneKomix1");
        }
    }


    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadPrevousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
    }

    public void SkipIntroScenes()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("SceneKomix1");             
    }
    public void LoadLastCheckPointScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString(checkPointKey));
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene("SampleScene2");
    }

    public void LoadLevelThree()
    {
        SceneManager.LoadScene("SampleScene3");
    }
    public void LoadLevelFor()
    {
        SceneManager.LoadScene("SampleScene4");
    }
    public void LoadLevelFive()
    {
        SceneManager.LoadScene("SampleScene5");
    }
    public void LoadLevelSix()
    {
        SceneManager.LoadScene("SampleScene6");
    }

    public void LoadWinScene4()
    {
        SceneManager.LoadScene("WinScene4");
    }
    public void LoadScore()
    {
        SceneManager.LoadScene("Score");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
