using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DobrePrasiatko : MonoBehaviour
{

    public bool somPrasa;
    public float amplitude;
    public float speed;
    public float rotateSpeed;
    public float lifetime;
    public float timeSpentUp;
    public float speedOfSlideUpAndDown;
    public GameObject levelManager;

    private LevelManager levelManagerScript;
    private bool goDown = false;
    private bool imUp = false;


    void Start()
    {
        levelManagerScript = levelManager.GetComponent<LevelManager>();
    }

    void Update()
    {
        Transform animalUp = transform.parent.Find("AnimalUp");
        Transform animalDown = transform.parent.Find("AnimalDown");


        if(goDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, animalDown.position, speedOfSlideUpAndDown * Time.deltaTime);

            if (Vector3.Distance(transform.position, animalDown.position) < 0.1f)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, animalUp.position, speedOfSlideUpAndDown * Time.deltaTime);
            if (!imUp && transform.position.Equals(animalUp.position))
            {
                imUp = true;
                Invoke("GoDown", timeSpentUp);
            }
        }

        if (Input.GetMouseButton(0))
            Debug.Log("down");

        //trasenie - ---
        //transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        //float AngleAmount = (Mathf.Cos(Time.time * rotateSpeed) * 180) / Mathf.PI * 0.5f;
        //AngleAmount = Mathf.Clamp(AngleAmount, -15, 15);
        //transform.localRotation = Quaternion.Euler(0, 0, AngleAmount);
        //trasenie - ---

    }

    void OnMouseDown()
    {

        if(somPrasa)
        {
            levelManagerScript.KillPrasa();
        } else
        {
            levelManagerScript.KillChobotnica();
        }

        //TODO particle

        Destroy(gameObject);
    }

    private void GoDown()
    {
        goDown = true;
    }
}
