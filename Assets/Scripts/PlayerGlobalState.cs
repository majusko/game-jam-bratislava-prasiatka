using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobalState : MonoBehaviour
{

    public int lifes;
    public int levelPoints;

    private static PlayerGlobalState _instance;

    public static PlayerGlobalState Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
