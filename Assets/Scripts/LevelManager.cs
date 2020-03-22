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
    private int currentScore = 0;
    private float levelWillEnd;
    private int internalLives;

    void Start()
    {
        levelWillEnd = Time.time + timeForLevelInSec;
        internalLives = lifes;
    }

    void Update()
    {
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


        if (internalLives <= 0)
        {
            Debug.Log("ZEMRI");
            //TODO end screen
        }

        if (Time.time > levelWillEnd)
        {
            Debug.Log("DALSI LEVEL");
            //TODO end level
        }


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "MyObjectName")
                { print("My object is clicked by mouse"); }
            }

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

    public void KillPrasa()
    {
        Debug.Log("killed prasa");

        internalLives--;


        Debug.Log(internalLives);
        if (internalLives == 2)
        {
            zivot1.SetActive(false);
            Destroy(zivot1);
        }

        if (internalLives == 1)
        {
            zivot2.SetActive(false);
            Destroy(zivot2);
        }

        if(internalLives == 0)
        {
            zivot3.SetActive(false);
            Destroy(zivot3);
        }
    }

    public void KillChobotnica()
    {
        Debug.Log("killed chbot");
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