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
    public GameObject explotion;

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
    }

    void OnMouseDown()
    {
        if(somPrasa)
        {
            PlayerGlobalState.Instance.lifes -= 1;
        } else
        {
            levelManagerScript.KillChobotnica();
        }

		GameObject particles = Instantiate(explotion, transform.position, Quaternion.identity) as GameObject;

        Destroy(particles, 2);

        Destroy(gameObject);
    }

    private void GoDown()
    {
        goDown = true;
    }
}
