using UnityEngine;
using System.Collections;

public class LoadThisToNextScene : MonoBehaviour
{
    //properties
    private static bool thisThingExists;

    //methods

    // Use this for initialization
    void Start()
    {
        if (!thisThingExists)
        {
            thisThingExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else {
            transform.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
