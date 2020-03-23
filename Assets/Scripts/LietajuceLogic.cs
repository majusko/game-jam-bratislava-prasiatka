using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LietajuceLogic : MonoBehaviour
{
    public bool somPrasa;
    private GameObject levelManager;
    private LevelManager levelManagerScript;
    public GameObject explotion;

    // Start is called before the first frame update
    void Start()
    {
        levelManagerScript = levelManager.GetComponent<LevelManager>();
    }

    // Update is called once per frame
  
    void OnMouseDown()
    {
        if (somPrasa)
        {
            PlayerGlobalState.Instance.lifes -= 1;
        }
        else
        {
            levelManagerScript.KillChobotnica();
        }

        GameObject particles = Instantiate(explotion, transform.position, Quaternion.identity) as GameObject;

        Destroy(particles, 2);

        Destroy(gameObject);
    }
}
