using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastScore : MonoBehaviour
{
    public Text score;
    // Start is called before the first frame update
    void Update()
    {      
        if (PlayerPrefs.HasKey("lastGlobalScore"))
        {         
            score.GetComponent<Text>().text = "nahral si : " + PlayerPrefs.GetInt("lastGlobalScore") + " bodoov."; 
        }
    }

    
}
