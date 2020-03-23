using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   

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

    public void QuitGame()
    {
        Application.Quit();
    }
}
