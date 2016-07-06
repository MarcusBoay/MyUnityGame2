using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    //properties
    Animator anim;
    bool isFading = false;
    private static bool screenFaderExists;

    //methods
    public IEnumerator FadeToClear()
    {
        PlayerMovement.shouldUpdate = false;
        isFading = true;
        anim.SetTrigger("out");
        while (isFading)
        {
            yield return null;
        }
    }

    public IEnumerator FadeToBlack()
    {
        PlayerMovement.shouldUpdate = false;
        isFading = true;
        anim.SetTrigger("in");
        while (isFading)
        {
            yield return null;
        }
    }

    void AnimationComplete()
    {
        isFading = false;
        anim.SetTrigger("isFadingTrigger");
        Debug.Log("duhh");
        PlayerMovement.shouldUpdate = true;
    }

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        if (!screenFaderExists)
        {
            screenFaderExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else {
            transform.gameObject.SetActive(false);
        }
    }

}
