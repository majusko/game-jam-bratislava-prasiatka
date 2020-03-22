using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public GameObject planet;
    public List<GameObject> planetObjects;
    public List<GameObject> spawningPigs;
    public List<GameObject> spawningOctopus;
    public float spawnSomethingEvery;
    public float chanceForPig;
    public float waitTimeForNext;
    public int pointsZaChobotnica;
    public int pointsCoTreba;
    public int lifes;
    public float timeForLevelInSec;
    public Text score;
    public Text timer;

    public GameObject zivot1;
    public GameObject zivot2;
    public GameObject zivot3;

    private float nextActionTime = 0.0f;
    private Dictionary<string, float> closedHrnce = new Dictionary<string, float>();
    private int currentScore = 0;
    private float levelWillEnd = 0;
    private float timeStarted = 0;

    void Start()
    {
        timeStarted = Time.time;
        levelWillEnd = Time.time + timeForLevelInSec;
        PlayerGlobalState.Instance.lifes = lifes;
        PlayerGlobalState.Instance.levelPoints = 0;
    }

    void Update()
    {

        score.text = PlayerGlobalState.Instance.levelPoints + "/" + pointsCoTreba;
        timer.text = Mathf.RoundToInt(levelWillEnd - Time.time) + "s";

        if (Time.time > nextActionTime)
        {
            nextActionTime += spawnSomethingEvery;
            SpawnSomething();
        }

        var removals = new List<string>();

        foreach (KeyValuePair<string, float> entry in closedHrnce)
        {
            if (entry.Value < Time.time)
            {
                removals.Add(entry.Key);
            }
        }

        foreach (string removal in removals)
        {
            closedHrnce.Remove(removal);
        }


        if (PlayerGlobalState.Instance.lifes <= 0 || (Time.time > levelWillEnd && PlayerGlobalState.Instance.levelPoints < pointsCoTreba))
        {
            Debug.Log("ZEMRI");
            SceneManager.LoadScene("FailScene");
        }

        if (PlayerGlobalState.Instance.lifes > 0 && Time.time > levelWillEnd && PlayerGlobalState.Instance.levelPoints > pointsCoTreba)
        {
            Debug.Log("DALSI LEVEL");

            SceneManager.LoadScene("WinScene1");
        }

        Zivoty();
    }

    private void SpawnSomething()
    {
        GameObject spawnBehind = NextPlaceToPick();

        GameObject spawningObject;

        if(Random.Range(0f, 1f) < chanceForPig)
        {
            spawningObject = spawningPigs[Random.Range(0, spawningPigs.Count)];
        } else
        {
            spawningObject = spawningOctopus[Random.Range(0, spawningOctopus.Count)];
        }

        GameObject intantiated = Instantiate(spawningObject, spawnBehind.transform.position, spawnBehind.transform.rotation);

        intantiated.transform.SetParent(spawnBehind.transform, true);
    }

    private GameObject NextPlaceToPick()
    {
        GameObject spawnBehind = planetObjects[Random.Range(0, planetObjects.Count)];

        if (closedHrnce.ContainsKey(spawnBehind.name))
        {
            return NextPlaceToPick();
        }

        closedHrnce[spawnBehind.name] = Time.time + waitTimeForNext;

        return spawnBehind;
    }


    private void SaveScore(int score)
    {
        var player = new Player();

        if(score > player.First)
        {
            player.First = score;
        } else if(score > player.Second) {
            player.Second = score;
        }
        else if (score > player.Third)
        {
            player.Third = score;
        }
        else if (score > player.Fourth)
        {
            player.Fourth = score;
        }
        else if (score > player.Fifth)
        {
            player.Fifth = score;
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
}


public class Player
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