using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    //properties
    public AudioSource BGM;
    private static bool audioManagerExists;

    //methods
    public void changeBGM(AudioClip music)
    {
        if (BGM.clip == music)
            return;
        BGM.Stop();
        BGM.clip = music;
        BGM.Play();
    }

    //Use this for initialization
    void Start()
    {
        if (!audioManagerExists)
        {
            audioManagerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            transform.gameObject.SetActive(false);
        }
    }
}
