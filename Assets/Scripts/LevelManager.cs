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

    private List<GameObject> currentlySpawned;
    private float nextActionTime = 0.0f;
    private Dictionary<string, float> closedHrnce = new Dictionary<string, float>();

    void Start()
    {
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

}
