using UnityEngine;
using System.Collections;

public class SwitchMusicOnLoad : MonoBehaviour
{
    //properties
    public AudioClip newTrack;
    private AudioManager theAM;

    //methods

    // Use this for initialization
    void Start()
    {
        theAM = FindObjectOfType<AudioManager>();
        if (newTrack != null)
            theAM.changeBGM(newTrack);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
