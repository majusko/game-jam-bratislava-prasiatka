using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SetUpSingleton();
    }

    void Update()
    {
        Scene m_scene;
        m_scene = SceneManager.GetActiveScene();
        if (m_scene.name == "SampleScene")
        {
            Destroy(gameObject);
        }

    }
    private void SetUpSingleton()
    {
       
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
