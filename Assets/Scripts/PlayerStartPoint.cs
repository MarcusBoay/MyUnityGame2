using UnityEngine;
using System.Collections;

public class PlayerStartPoint : MonoBehaviour
{
    //properties
    private PlayerMovement thePlayer;
    private CameraFollow theCamera;

    //methods

    // Use this for initialization
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMovement>();
        thePlayer.transform.position = transform.position;

        theCamera = FindObjectOfType<CameraFollow>();
        theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
        /*
		ScreenFader sf = GameObject.FindGameObjectWithTag ("fader").GetComponent<ScreenFader> ();
		yield return StartCoroutine (sf.FadeToClear ());
		PlayerMovement.shouldUpdate = true;
*/
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
	IEnumerator OnTriggerEnter2D (){
		ScreenFader sf2 = GameObject.FindGameObjectWithTag ("fader").GetComponent<ScreenFader> ();
		yield return StartCoroutine (sf2.FadeToClear ());
		PlayerMovement.shouldUpdate = true;
	}
	*/
}
