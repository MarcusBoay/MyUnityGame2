using UnityEngine;
using System.Collections;

public class Warps : MonoBehaviour
{

    public Transform warpTarget;
    PlayerMovement pm = new PlayerMovement();

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        ScreenFader sf = GameObject.FindGameObjectWithTag("fader").GetComponent<ScreenFader>();
        pm.updateCase(false);
        yield return StartCoroutine(sf.FadeToBlack());
        other.gameObject.transform.position = warpTarget.position;
        Camera.main.transform.position = warpTarget.position;
        yield return StartCoroutine(sf.FadeToClear());
        pm.updateCase(true);
    }

}