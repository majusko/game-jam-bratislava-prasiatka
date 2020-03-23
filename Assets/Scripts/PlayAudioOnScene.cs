using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnScene : MonoBehaviour
{
    public AudioClip AudioFile;
    [SerializeField] [Range(0, 1)] float SoundVolume = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(AudioFile, Camera.main.transform.position, SoundVolume);
    }


}
