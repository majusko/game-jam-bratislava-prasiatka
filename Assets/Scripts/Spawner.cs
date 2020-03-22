using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class Spawner : MonoBehaviour
{


    [SerializeField] GameObject chobotnicaPrefab;
    [SerializeField] GameObject prasaPrefab;

    [SerializeField] float destroyTime = 1;   
    [SerializeField] int sancaNaPrasa = 30;
    [SerializeField] int sancaNaCobotnicu = 70;
    [SerializeField] int intervalMin = 2;
    [SerializeField] int intervalMax = 10;
    [SerializeField] float speed = 2;
    [SerializeField] AudioClip chobotnicaSound;
    [SerializeField] [Range(0, 1)] float chobotnicaSoundVolume = 0.25f;
    private Random rngesus = new Random();
    
    // Use this for initialization
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
