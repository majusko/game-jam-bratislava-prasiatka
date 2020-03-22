using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int lifes;
    public float timeForLevelInSec;

    public GameObject zivot1;
    public GameObject zivot2;
    public GameObject zivot3;

    private float nextActionTime = 0.0f;
    private Dictionary<string, float> closedHrnce = new Dictionary<string, float>();
    private int currentScore;
    private float levelWillEnd;

    void Start()
    {
        levelWillEnd = Time.time + timeForLevelInSec;
    }

    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += spawnSomethingEvery;
            SpawnSomething();
        }

        var removals = new List<string>();

        foreach (KeyValuePair<string, float> entry in closedHrnce) {
            if(entry.Value < Time.time)
            {
                removals.Add(entry.Key);
            }
        }

        foreach(string removal in removals)
        {
            closedHrnce.Remove(removal);
        }


        if(lifes <= 0)
        {
            Debug.Log("ZEMRI");
            //TODO end screen
        }

        if(Time.time > levelWillEnd)
        {
            Debug.Log("DALSI LEVEL");
            //TODO end level
        }
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

    private void KillPrasa()
    {
        lifes--;

        if(lifes == 2)
        {
            Destroy(zivot1);
        }

        if (lifes == 1)
        {
            Destroy(zivot2);
        }

        if(lifes == 0)
        {
            Destroy(zivot3);
        }
    }

    private void KillChobotnica()
    {
        currentScore += pointsZaChobotnica;
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