using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlanetLogic : MonoBehaviour
{

    public float amplitude;
    public float speed;
    private float tempVal;
    private Vector3 tempPos;

    void Start()
    {
        tempPos = transform.position;
        tempVal = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, 0, 10 * Time.deltaTime);
        tempPos.y = tempVal + amplitude * Mathf.Sin(speed * Time.time);
        transform.position = tempPos;
    }

}
