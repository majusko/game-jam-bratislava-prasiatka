using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class Spawner : MonoBehaviour
{
    public GameObject chobotnicaPrefab;
    public GameObject prasaPrefab;
    public List<GameObject> flyToPlaces;

    public float destroyTime = 1;
    public int sancaNaPrasa = 30;
    public int sancaNaCobotnicu = 70;
    public int intervalMin = 2;
    public int intervalMax = 10;
    public float speed = 2;
    public AudioClip chobotnicaSound;
    [Range(0, 1)] public float chobotnicaSoundVolume = 0.25f;
    private Random rngesus = new Random();
    
    void Start()
    {
        Invoke("Spawn", UnityEngine.Random.Range(intervalMin, intervalMax));

    }

    public void Spawn()
    {
        GameObject aktualnySpawn = new GameObject();

        Random random = new Random();
        int x = random.Next(0, 100);
        if ((x -= sancaNaPrasa) < 0) 
        {
            aktualnySpawn = prasaPrefab;
        }
        else if ((x -= sancaNaCobotnicu) < 0)
        {
            aktualnySpawn = chobotnicaPrefab;
        }
        else
        {
            //tato vetva netreba 
          //  aktualnySpawn = chobotnicaPrefab;
        }

        var tmpTransform = transform;
        var tmp = (float)(rngesus.NextDouble() * tmpTransform.localScale.x);
        GameObject g = Instantiate(aktualnySpawn, tmpTransform.position + Vector3.right * tmp + Vector3.left * tmpTransform.localScale.x / 2, Quaternion.identity) as GameObject;
        g.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        AudioSource.PlayClipAtPoint(chobotnicaSound, Camera.main.transform.position, chobotnicaSoundVolume);
        Destroy(g, destroyTime);
        Invoke("Spawn", UnityEngine.Random.Range(intervalMin, intervalMax));
    }
}
