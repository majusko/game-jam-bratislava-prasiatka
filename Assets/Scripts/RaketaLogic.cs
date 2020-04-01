using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RaketaLogic : MonoBehaviour
{
    public GameObject explotion;
    public Text zachraneni;
    public AudioClip zasahSound;
    [SerializeField] [Range(0, 1)] float zasahSoundVolume = 0.25f;
    public AudioClip zaschraneniSound;
    [SerializeField] [Range(0, 1)] float zachraneniSoundVolume = 0.25f;
    public int pointsCoTreba;
    public string failScene;
    public string winScene;
    public bool lastScene;


    private int aktualneScore = 0;
    private string currentScoreKey = "currentGlobalScore";
    public LevelManager levelManager;


    private void Start()
    {
        PlayerPrefs.SetInt(currentScoreKey, 0);
     
         zachraneni.text = PlayerGlobalState.Instance.levelPoints + "/" + pointsCoTreba;
     }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Chobotnica"))
        {
            PlayerGlobalState.Instance.lifes -= 1;
            GameObject particles = Instantiate(explotion, transform.position, Quaternion.identity) as GameObject;
            AudioSource.PlayClipAtPoint(zasahSound, Camera.main.transform.position, zasahSoundVolume);


        }
        if (collision.name.Contains("Prasa"))
        {
            aktualneScore++;
            AudioSource.PlayClipAtPoint(zaschraneniSound, Camera.main.transform.position, zachraneniSoundVolume);
            zachraneni.text = aktualneScore + "/" + pointsCoTreba;

            if (aktualneScore >= pointsCoTreba)
            {
                PlayerPrefs.SetInt(currentScoreKey, PlayerPrefs.GetInt(currentScoreKey) + PlayerGlobalState.Instance.levelPoints);
                if (lastScene)
                {
                    levelManager.SaveScore();
                }

                SceneManager.LoadScene(winScene);
            }

        }
    }
}
