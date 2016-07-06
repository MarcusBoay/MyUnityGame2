using UnityEngine;
using System.Collections;

public class SwitchMusicTrigger : MonoBehaviour
{
    //properties
    public AudioClip newTrack;
    private AudioManager theAM;

    //methods
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (newTrack != null)
            {
                theAM.changeBGM(newTrack);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        theAM = FindObjectOfType<AudioManager>();
    }
}
