using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardScene : MonoBehaviour
{
    public Text place1;
    public Text place2;
    public Text place3;
    public Text place4;
    public Text place5;

    public Text nahralsi;

    void Update()
    {
        var score = 0;

        if (PlayerPrefs.HasKey("currentGlobalScore"))
        {
            score = PlayerPrefs.GetInt("currentGlobalScore");
        }

        nahralsi.text = "Nahral si  " + score + " bodov";

        var leaderboard = new Leaderboard();

        place1.text = "1. " + leaderboard.First;
        place2.text = "2. " + leaderboard.Second;
        place3.text = "3. " + leaderboard.Third;
        place4.text = "4. " + leaderboard.Fourth;
        place5.text = "5. " + leaderboard.Fifth;
    }
}