using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour
{
    //methods
    public string levelToLoad;
    PlayerMovement pm = new PlayerMovement();

    //properties
    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            ScreenFader sf = GameObject.FindGameObjectWithTag("fader").GetComponent<ScreenFader>();
            pm.updateCase(false);
            yield return StartCoroutine(sf.FadeToBlack());
            //something is wrong ---vv
            Debug.Log("1");
            SceneManager.LoadScene(levelToLoad);
            Debug.Log("2");
            yield return StartCoroutine(sf.FadeToClear());
            Debug.Log("3");
            pm.updateCase(true);
            Debug.Log("4");
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
