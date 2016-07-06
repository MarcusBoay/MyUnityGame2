using UnityEngine;
using System.Collections;

public class AudioManagerCheck : MonoBehaviour
{
    //properties
    public GameObject audioMan;

    //methods

    // Use this for initialization
    void Start()
    {
        if (FindObjectOfType<AudioManager>())
            return;
        else
            Instantiate(audioMan, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
