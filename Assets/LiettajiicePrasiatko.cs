using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiettajiicePrasiatko : MonoBehaviour
{
    public bool somPrasa;
    public float flySpeed;
    public GameObject explotion;
    public AudioClip prasaKillSound;
    [SerializeField] [Range(0, 1)] float prasaSoundVolume = 0.25f;
    public AudioClip chobotnicaKillSound;
    [SerializeField] [Range(0, 1)] float chobotnicaSoundVolume = 0.25f;

    private GameObject levelManager;
    private LevelManager levelManagerScript;
    private List<GameObject> flyToPlaces;
    private Vector3 flyTo;
    private bool directionrotate;

    void Start()
    {
        levelManager = GameObject.Find("LevelManager");
        levelManagerScript = levelManager.GetComponent<LevelManager>();
        flyToPlaces = levelManagerScript.getFlyTo();
        flyTo = flyToPlaces[Random.Range(0, flyToPlaces.Count)].transform.position;
        directionrotate = (Random.value > 0.5f);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, flyTo, flySpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, flyTo) < 0.1f)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        if(directionrotate)
        {
            transform.Rotate(new Vector3(0, 0, 1));
        } else
        {
            transform.Rotate(new Vector3(0, 0, -1));
        }


    }

    void OnMouseDown()
    {
        if (somPrasa)
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
}
