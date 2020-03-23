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
    public GameObject explotion;
    public AudioClip prasaKillSound;
    [SerializeField] [Range(0, 1)] float prasaSoundVolume = 0.25f;
    public AudioClip chobotnicaKillSound;
    [SerializeField] [Range(0, 1)] float chobotnicaSoundVolume = 0.25f;

    private GameObject levelManager;
    private LevelManager levelManagerScript;
    private bool goDown = false;
    private bool imUp = false;


    void Start()
    {
        levelManager = GameObject.Find("LevelManager");
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
            AudioSource.PlayClipAtPoint(prasaKillSound, Camera.main.transform.position, prasaSoundVolume);
            PlayerGlobalState.Instance.lifes -= 1;
           

        }
        else
        {
           AudioSource.PlayClipAtPoint(chobotnicaKillSound, Camera.main.transform.position, chobotnicaSoundVolume);
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
