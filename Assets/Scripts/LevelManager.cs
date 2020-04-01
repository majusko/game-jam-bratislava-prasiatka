using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public bool firstScene;
    public bool lastScene;
    public bool hasBuckets;
    public List<GameObject> planetObjects;
    public List<GameObject> spawningPigs;
    public List<GameObject> spawningOctopus;
    public float spawnSomethingEvery;
    public float spawnFlyingChance;
    public float chanceForPig;
    public float waitTimeForNext;
    public int pointsZaChobotnica;
    public int pointsCoTreba;
    public int lifes;
    public float timeForLevelInSec;
    public Text score;
    public Text timer;
    public string failScene;
    public string winScene;
   
    public GameObject zivot1;
    public GameObject zivot2;
    public GameObject zivot3;

    public List<GameObject> flyPlaceToStart;
    public List<GameObject> flyPigs;
    public List<GameObject> flyOctopus;
    public List<GameObject> flyToPlaces;

    private float nextActionTime = 1.5f;
    private Dictionary<string, float> closedHrnce = new Dictionary<string, float>();
    private string currentScoreKey = "currentGlobalScore";
    private float levelWillEnd = 0;
    private float timeStarted = 0;
    private bool started = false;

    void Start()
    {
        timeStarted = Time.timeSinceLevelLoad;
        levelWillEnd = Time.timeSinceLevelLoad + timeForLevelInSec;
        PlayerGlobalState.Instance.lifes = lifes;
        PlayerGlobalState.Instance.levelPoints = 0;
        started = true;

        if(firstScene)
        {
            PlayerPrefs.SetInt(currentScoreKey, 0);
        }
    }

    void Update()
    {

        score.text = PlayerGlobalState.Instance.levelPoints + "/" + pointsCoTreba;
        timer.text = Mathf.RoundToInt(levelWillEnd - Time.timeSinceLevelLoad) + "s";

        if (started && Time.timeSinceLevelLoad > nextActionTime)
        {
            nextActionTime += spawnSomethingEvery;

            if (Random.Range(0f, 1f) < spawnFlyingChance)
            {
                SpawnSomethingFlying();
            }
            else
            {
                if(hasBuckets)
                SpawnSomething();
            }

        }


        var removals = new List<string>();

        foreach (KeyValuePair<string, float> entry in closedHrnce)
        {
            if (entry.Value < Time.timeSinceLevelLoad)
            {
                removals.Add(entry.Key);
            }
        }

        foreach (string removal in removals)
        {
            closedHrnce.Remove(removal);
        }


        if (PlayerGlobalState.Instance.lifes <= 0 || (Time.timeSinceLevelLoad > levelWillEnd && PlayerGlobalState.Instance.levelPoints < pointsCoTreba))
        {
            PlayerPrefs.SetInt(currentScoreKey, PlayerPrefs.GetInt(currentScoreKey) + PlayerGlobalState.Instance.levelPoints);
            SceneManager.LoadScene(failScene);
        }

        if (PlayerGlobalState.Instance.lifes > 0 && Time.timeSinceLevelLoad > levelWillEnd && PlayerGlobalState.Instance.levelPoints >= pointsCoTreba)
        {
            PlayerPrefs.SetInt(currentScoreKey, PlayerPrefs.GetInt(currentScoreKey) + PlayerGlobalState.Instance.levelPoints);

            if (lastScene)
            {
                SaveScore();
            }

            SceneManager.LoadScene(winScene);
        }

        Zivoty();
    }

    private void SpawnSomething()
    {
        GameObject spawnBehind = NextPlaceToPick();

        if(spawnBehind != null)
        {

            GameObject spawningObject;

            if (Random.Range(0f, 1f) < chanceForPig)
            {
                spawningObject = spawningPigs[Random.Range(0, spawningPigs.Count)];
            }
            else
            {
                spawningObject = spawningOctopus[Random.Range(0, spawningOctopus.Count)];
            }

            GameObject intantiated = Instantiate(spawningObject, spawnBehind.transform.position, spawnBehind.transform.rotation);

            intantiated.transform.SetParent(spawnBehind.transform, true);
        }

    }

    private void SpawnSomethingFlying()
    {
        GameObject spawningObject;

        if (Random.Range(0f, 1f) < chanceForPig)
        {
            spawningObject = flyPigs[Random.Range(0, flyPigs.Count)];
        }
        else
        {
            spawningObject = flyOctopus[Random.Range(0, flyOctopus.Count)];
        }

        GameObject intantiated = Instantiate(spawningObject, flyPlaceToStart[Random.Range(0, flyPlaceToStart.Count)].transform.position, Quaternion.identity);

    }


    private GameObject NextPlaceToPick(){
        GameObject spawnBehind = planetObjects[Random.Range(0, planetObjects.Count)];

        if (!closedHrnce.ContainsKey(spawnBehind.name))
        {
            closedHrnce[spawnBehind.name] = Time.timeSinceLevelLoad + waitTimeForNext;

            return spawnBehind;
        }

        return null;

    }


    public void SaveScore()
    {
        var score = PlayerPrefs.GetInt(currentScoreKey);
        var leaderboard = new Leaderboard();

        if(score > leaderboard.First)
        {
            leaderboard.First = score;
        } else if(score > leaderboard.Second) {
            leaderboard.Second = score;
        }
        else if (score > leaderboard.Third)
        {
            leaderboard.Third = score;
        }
        else if (score > leaderboard.Fourth)
        {
            leaderboard.Fourth = score;
        }
        else if (score > leaderboard.Fifth)
        {
            leaderboard.Fifth = score;
        }
    }

    private void Zivoty()
    {
        if (PlayerGlobalState.Instance.lifes == 3)
        {
            zivot1.SetActive(true);
            zivot2.SetActive(true);
            zivot3.SetActive(true);
        }
        if (PlayerGlobalState.Instance.lifes == 2)
        {
            zivot1.SetActive(false);
            zivot2.SetActive(true);
            zivot3.SetActive(true);
        }
        if (PlayerGlobalState.Instance.lifes == 1)
        {
            zivot1.SetActive(false);
            zivot2.SetActive(false);
            zivot3.SetActive(true);
        }
        if (PlayerGlobalState.Instance.lifes == 0)
        {
            zivot1.SetActive(false);
            zivot2.SetActive(false);
            zivot3.SetActive(false);
        }
    }

    public void KillChobotnica()
    {
        PlayerGlobalState.Instance.levelPoints += pointsZaChobotnica;
        
    }

    public List<GameObject> getFlyTo()
    {
        return flyToPlaces;
    }
}

public class Leaderboard
{
    private const string first = "first";
    public int First
    {
        get { return PlayerPrefs.GetInt(first); }
        set { PlayerPrefs.SetInt(first, value); }
    }

    private const string second = "second";
    public int Second
    {
        get { return PlayerPrefs.GetInt(second); }
        set { PlayerPrefs.SetInt(second, value); }
    }

    private const string third = "third";
    public int Third
    {
        get { return PlayerPrefs.GetInt(third); }
        set { PlayerPrefs.SetInt(third, value); }
    }

    private const string fourth = "fourth";
    public int Fourth
    {
        get { return PlayerPrefs.GetInt(fourth); }
        set { PlayerPrefs.SetInt(fourth, value); }
    }

    private const string fifth = "fifth";
    public int Fifth
    {
        get { return PlayerPrefs.GetInt(fifth); }
        set { PlayerPrefs.SetInt(fifth, value); }
    }
}