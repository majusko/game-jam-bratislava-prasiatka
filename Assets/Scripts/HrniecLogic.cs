using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HrniecLogic : MonoBehaviour
{

    public float amplitude;
    public float speed;
    public float rotateSpeed;
    private Vector3 tempVal;
    private Vector3 tempPos;


    void Start()
    {
        tempPos = transform.position;
        tempVal = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        tempPos.y = tempVal.y + amplitude * Mathf.Sin(speed * Time.time);
        tempPos.x = tempVal.x + amplitude * Mathf.Sin(speed * Time.time);
        transform.position = tempPos;
    }

}
